using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public Transform target;

    public float speed = 10;

    Rigidbody rb;

    public GameObject dieParticleSystemPrefab;

    public EnemySpawner spawner;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        rb.AddForce((target.position - transform.position).normalized * speed);

        rb.AddForce(Vector3.down * 10);

        if (Vector3.Distance(transform.position, target.position) < 2.5f)
        {
            IDamageable d = target.GetComponent<IDamageable>();

            if (d != null)
                d.Damage(1, transform.forward);
        }

        if (rb.velocity.magnitude < 0.3f)
        {
            rb.AddTorque(new Vector3(10, 10, 10));
        }
    }

    public void Damage(int amount, Vector3 damageDirection)
    {
        GameObject go = Instantiate(dieParticleSystemPrefab, transform.position, transform.rotation);

        go.transform.LookAt(go.transform.position + damageDirection);
        spawner.RemoveEnemy(gameObject);
        Destroy(go, 4);
        Destroy(gameObject);
    }
}
