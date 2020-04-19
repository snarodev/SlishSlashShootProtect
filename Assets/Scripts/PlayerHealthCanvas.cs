using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthCanvas : MonoBehaviour
{
    Transform player;

    Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        offset = transform.position - player.position;
    }


    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }

}
