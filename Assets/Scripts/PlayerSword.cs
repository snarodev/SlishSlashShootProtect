using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float attackAngle = 40;

    public float attackDuration = 0.1f;

    public Transform sword;

    float attackTime;

    public ParticleSystem swordParticleSystem;
    public GameObject swordDamageObj;

    public float cooldownFactor = 3;

    public AnimationCurve attackCurve;

    public Animator anim;

    FlowController flowController;
    GameStateController gameStateController;

    private void Start()
    {
        flowController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlowController>();
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
    }

    private void Update()
    {
        if (gameStateController.currentGameState != GameStateController.GameState.InGame)
            return;

        if (flowController.flow)
            return;

        if (Input.GetMouseButtonDown(1))
            anim.SetBool("Attack", true);
        else if (Input.GetMouseButtonUp(1))
            anim.SetBool("Attack", false);
    }
}
