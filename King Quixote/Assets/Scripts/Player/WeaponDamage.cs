using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    WeaponAction weapon;

    private void Start()
    {
        weapon = gameObject.GetComponentInParent<WeaponAction>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyController>())
        {
            other.GetComponent<EnemyController>().health -= weapon.parent.damage;

            other.GetComponent<DamageAnim>().take_damage();
        }

        if(other.GetComponent<Breakable>())
        {
            other.GetComponent<Breakable>().health -= weapon.parent.damage;
        }
    }
}
