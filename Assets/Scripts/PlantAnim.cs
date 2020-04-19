using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnim : MonoBehaviour
{
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time), 0);

        Vector3 rot = new Vector3(10, 9, 3) * Time.deltaTime;

        transform.Rotate(rot);
    }
}
