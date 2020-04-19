using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    Camera cam;

    public GameObject projectilePrefab;

    public float shootInterval = 0.2f;
    float nextShootTime;

    FlowController flowController;
    GameStateController gameStateController;

    private void Start()
    {
        cam = Camera.main;

        flowController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlowController>();
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
    }

    private void Update()
    {
        if (flowController.flow)
            return;

        if (gameStateController.currentGameState != GameStateController.GameState.InGame)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookAtTarget = hit.point;
            lookAtTarget.y = transform.position.y;
            transform.LookAt(lookAtTarget);
        }

        if (Time.time > nextShootTime)
        {
            nextShootTime = Time.time + shootInterval;

            if (Input.GetMouseButton(0))
            {
                GameObject go = Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
        }
    }
}
