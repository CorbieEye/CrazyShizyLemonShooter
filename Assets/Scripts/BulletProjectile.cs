using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public DamageData DamageData = new DamageData();
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = 50f;
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        IDamageReceiver damageReceiver = other.transform.root.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.ReceiveDamage(DamageData);
        }
        Destroy(gameObject);
    }
}
