using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    public int health;

    private void Start()
    {
        health = 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponentInParent<WeaponAttack>() != null)
        {
            print("HA GOT IT");
            gameObject.GetComponent<DrawCollider>().hurtboxCollision();

            gameObject.GetComponentInParent<DamageAnim>().take_damage();
            health -= other.gameObject.GetComponentInParent<WeaponAttack>().damage;
        }
    }

    private void FixedUpdate()
    {
        if (health <= 0)
            Destroy(gameObject.transform.parent.gameObject);
    }
}
