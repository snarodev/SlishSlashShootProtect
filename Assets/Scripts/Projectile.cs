using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;     
    }
}
