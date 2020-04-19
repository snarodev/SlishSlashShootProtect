using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageable
{
    public float speed = 10;

    public float maxSpeed;

    public Transform displayPlayer;

    Rigidbody rb;

    public int maxHealth = 100;

    public Image healthImage;

    int health;

    FlowController flowController;

    Vector3 lastFlowTargetPos;

    GameStateController gameStateController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;

        flowController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlowController>();

        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
    }

    void FixedUpdate()
    {
        if (gameStateController.currentGameState != GameStateController.GameState.InGame)
            return;

        if (flowController.flow)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector3 velocity = new Vector3(h, 0, v);
        velocity.Normalize();

        if (Input.GetMouseButton(0))
            velocity *= speed * 0.8f;
        else  
            velocity *= speed;

        rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, velocity.x, 0.5f), rb.velocity.y, Mathf.Lerp(rb.velocity.z, velocity.z, 0.5f));

        rb.AddForce(Vector3.down * 10);

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 3, 0);
        }
    }

    private void Update()
    {
        if (gameStateController.currentGameState != GameStateController.GameState.InGame)
            return;

        if (flowController.flow)
        {
            if (flowController.currentFlowTarget != null)
                lastFlowTargetPos = flowController.currentFlowTarget.transform.position;

            transform.position = Vector3.Lerp(transform.position, Vector3.MoveTowards(lastFlowTargetPos, transform.position, 3), 100f * Time.deltaTime);
        }

        float scale = 1 + Mathf.Sin(Time.time * 10) * Mathf.Clamp01(rb.velocity.magnitude) * 0.1f;

        Vector3 newScale = new Vector3(scale, 1, scale);

        displayPlayer.localScale = newScale;

        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>().GameOver();
        }
    }

    public void Damage(int amount, Vector3 damageDirection)
    {
        health -= amount;

        healthImage.fillAmount = (float)health / (float)maxHealth;
    }
}

