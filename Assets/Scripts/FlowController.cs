using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FlowController : MonoBehaviour
{
    public Transform[] testArray;

    [HideInInspector]
    public bool flow = false;
    [HideInInspector]
    public GameObject currentFlowTarget;

    EnemySpawner spawner;
    GameStateController gameStateController;

    public Animator anim;
    public Transform playerRot;

    public GameObject flowCollider;

    public GameObject keyPrefab;
    public Transform keyParent;

    public GameObject inFlowMeter;

    public Image inFlowMeterImage;

    public float currentFlowAmount = 100;
    public float maxFlowAmount = 100;
    public float flowDecrease = 150;
    public float flowReplenishment = 10;

    public ComboText comboText;

    public GameObject playerHealthBar;

    public AudioMixerSnapshot normalAudioMixerSnapshot;
    public AudioMixerSnapshot flowAudioMixerSnapshot;

    public AudioSource flowSound;

    public AudioSource swordAttackSound;

    public AudioSource missKeyAudioSource;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
        flowCollider.SetActive(false);
        comboText.gameObject.SetActive(false);
        inFlowMeter.SetActive(false);
    }

    void Update()
    {
        if (gameStateController.currentGameState != GameStateController.GameState.InGame)
        {
            if (flow)
                ExitFlow(true);

            return;
        }

        if (spawner.currentlySpawnedEnemys == -1)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !flow && spawner.CanFlow())
        {
            flow = true;

            currentFlowTarget = spawner.FindNearestEnemy(transform.position);

            if (currentFlowTarget == null)
                flow = false;

            Time.timeScale = 0.1f;
            anim.SetTrigger("FlowAttackRight");

            flowCollider.SetActive(true);

            currentFlowAmount = maxFlowAmount;

            for (int i = 0; i < 6; i++)
            {
                GameObject go = Instantiate(keyPrefab, keyParent);
                go.GetComponent<FlowKey>().flowController = this;

                go.transform.SetAsLastSibling();
            }
            inFlowMeter.SetActive(true);
            comboText.gameObject.SetActive(true);
            playerHealthBar.SetActive(false);
            comboText.ResetCombo();
            flowAudioMixerSnapshot.TransitionTo(0);
            flowSound.Play();
            spawner.currentFlowAmount = 0;
        }

        if (flow)
        {
            spawner.currentFlowAmount = 0;

            if (currentFlowTarget != null)
                playerRot.LookAt(currentFlowTarget.transform);


            inFlowMeterImage.fillAmount = currentFlowAmount / maxFlowAmount;

            currentFlowAmount -= (flowDecrease + (comboText.currentCombo + 1)) * Time.deltaTime;

            if (currentFlowAmount < 0)
                ExitFlow(true);
        }
    }

    public void NextFlowKey()
    {
        Shift();
    }
    bool right;

    void Shift()
    {
        currentFlowTarget = spawner.FindNearestEnemy(transform.position);

        if (currentFlowTarget == null)
        {
            ExitFlow(true);
            return;
        }

        Destroy(keyParent.GetChild(0).gameObject);

        currentFlowAmount += flowReplenishment;

        currentFlowAmount = Mathf.Min(currentFlowAmount, maxFlowAmount);

        GameObject go = Instantiate(keyPrefab, keyParent);
        go.GetComponent<FlowKey>().flowController = this;

        go.transform.SetAsLastSibling();

        comboText.NewCombo();

        swordAttackSound.Play();

        if (right)
            anim.SetTrigger("FlowAttackRight");
        else
            anim.SetTrigger("FlowAttackRight");


        right = !right;
    }

    public void ExitFlow(bool lastEnemy)
    {
        if (!lastEnemy)
        {
            //Missed a key
            missKeyAudioSource.Play();
        }

        spawner.currentFlowAmount = 0;
        flow = false;
        flowCollider.SetActive(false);

        for (int i = 0; i < keyParent.childCount; i++)
        {
            Destroy(keyParent.GetChild(i).gameObject);
        }
        inFlowMeter.SetActive(false);
        playerHealthBar.SetActive(true);
        StartCoroutine("ExitFlowAnim");
        flowSound.Stop();
        normalAudioMixerSnapshot.TransitionTo(0.5f);
    }

    IEnumerator ExitFlowAnim()
    {
        yield return new WaitForSecondsRealtime(1);
        for (float i = 0; i < 1; i+= 0.01f)
        {
            Time.timeScale = i;
            yield return 0;
        }
        Time.timeScale = 1;
        comboText.gameObject.SetActive(false);
        spawner.currentFlowAmount = 0;
    }
}
