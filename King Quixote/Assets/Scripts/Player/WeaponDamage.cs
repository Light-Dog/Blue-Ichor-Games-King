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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<EnemyController>())
        {
            if(other.GetComponent<EnemyController>().damaged != true)
            {
                other.GetComponent<EnemyController>().health -= weapon.parent.damage;
                other.GetComponent<EnemyController>().Bleed();

                other.GetComponent<DamageAnim>().take_damage();
            }

        }

        if(other.GetComponent<Breakable>())
        {
            if(other.GetComponent<Breakable>().safe)
            {
                other.GetComponent<Breakable>().health -= weapon.parent.damage;
                other.GetComponent<Breakable>().Crack();

            }
        }
    }
}
