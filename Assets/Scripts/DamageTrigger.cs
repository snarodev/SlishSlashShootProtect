using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public bool destroyOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable s = other.GetComponent<IDamageable>();

        if (s != null)
        {
            s.Damage(1, transform.forward);

            if (destroyOnTrigger)
                Destroy(gameObject);
        }
    }
}
