using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnInterval = 2;
    float nextSpawnTime;

    public Transform plant;

    Transform player;

    List<GameObject> enemys = new List<GameObject>();

    GameStateController gameStateController;

    int wave = -1;

    public int currentlySpawnedEnemys;

    float targetEnemyAmount = 30;

    public Text waveText;

    public Image flow;
    public GameObject flowText;

    public float currentFlowAmount = 0;
    public float maxFlowAmount = 100;
    public float flowDecrease = 10;
    public float flowReplenishment = 10;
    public float flowRequired = 80;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
    }

    private void Update()
    {
        if (gameStateController.currentGameState == GameStateController.GameState.InGame)
        {
            if (wave == -1)
            {
                wave = 0;
                StartCoroutine("NextWave");
            }

            if (currentlySpawnedEnemys < targetEnemyAmount && currentlySpawnedEnemys != -1)
            {
                if (Time.time > nextSpawnTime)
                {
                    nextSpawnTime = Time.time + spawnInterval;

                    GameObject go = Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * 50, Quaternion.identity);

                    go.GetComponent<Enemy>().target = Random.Range(0, 2) < 1 ? plant : player;
                    go.GetComponent<Enemy>().spawner = this;

                    enemys.Add(go);

                    currentlySpawnedEnemys++;
                }


                currentFlowAmount -= flowDecrease * Time.deltaTime;
                currentFlowAmount = Mathf.Max(currentFlowAmount, 0);
            }
            else
            {
                if (enemys.Count == 0 && currentlySpawnedEnemys != -1)
                {
                    currentlySpawnedEnemys = -1;
                    StartCoroutine("NextWave");
                }
            }
        }

        flow.fillAmount = currentFlowAmount / flowRequired;

        flowText.SetActive(CanFlow());
    }

    public bool CanFlow()
    {
        return currentFlowAmount > flowRequired;
    }

    IEnumerator NextWave()
    {
        wave++;
        waveText.text = "Wave " + wave.ToString();

        currentlySpawnedEnemys = -1;

        yield return new WaitForSecondsRealtime(3);

        currentlySpawnedEnemys = 0;

        targetEnemyAmount *= 1.3f;

        waveText.text = "";
    }

    public GameObject FindNearestEnemy(Vector3 pos)
    {
        float best = float.MaxValue;
        int bestId = -1;
        for (int i = 0; i < enemys.Count; i++)
        {
            if (Vector3.Distance(enemys[i].transform.position, pos) < best)
            {
                best = Vector3.Distance(enemys[i].transform.position, pos);
                bestId = i;
            }
        }

        if (bestId == -1)
            return null;

        return enemys[bestId];
    }

    public void RemoveEnemy(GameObject go)
    {
        currentFlowAmount += flowReplenishment;
        currentFlowAmount = Mathf.Min(currentFlowAmount, maxFlowAmount);

        enemys.Remove(go);
    }
}
