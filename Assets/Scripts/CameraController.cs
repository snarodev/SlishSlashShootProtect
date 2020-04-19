using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothTime = 4;

    Transform player;
    Rigidbody playerRb;

    Vector3 offset;

    Vector3 velocity;

    Camera cam;

    Vector3 distance = new Vector3(0,0,0);

    FlowController flowController;

    Quaternion normalRot;

    Vector3 lastFlowTargetPos;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        flowController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlowController>();
        playerRb = player.GetComponent<Rigidbody>();
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
        normalRot = transform.rotation;
    }

    void Update()
    {
        Vector3 target = player.position;
        if (flowController.flow)
        {
            if (flowController.currentFlowTarget != null)
                lastFlowTargetPos = flowController.currentFlowTarget.transform.position;

            target = Vector3.Lerp(player.position, lastFlowTargetPos, 0.5f) + -transform.forward * 6;

            target.y = Mathf.Max(target.y, 0.5f);

            transform.LookAt(Vector3.Lerp(player.position, lastFlowTargetPos, 0.5f));

            transform.position = Vector3.Lerp(transform.position, target, 0.25f);
        }
        else
        {
            //distance = Vector3.Lerp(distance, -transform.forward * playerRb.velocity.magnitude * 0.4f, 0.01f);
            distance = -transform.forward * 20;

            target = player.position + offset + distance;

            

            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, target.x, ref velocity.x, smoothTime),
                Mathf.SmoothDamp(transform.position.y, target.y, ref velocity.y, smoothTime),
                Mathf.SmoothDamp(transform.position.z, target.z, ref velocity.z, smoothTime));

            transform.rotation = Quaternion.Lerp(transform.rotation, normalRot, 0.1f);

            //transform.LookAt(player);
        }

        
    }
}
