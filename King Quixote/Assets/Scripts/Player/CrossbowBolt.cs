using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowBolt : MonoBehaviour
{
    WeaponController weapon;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        weapon = player.GetComponent<PlayerController>().weapons[player.GetComponent<PlayerController>().equipedWeapon];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyController>())
        {
            other.GetComponent<EnemyController>().health -= weapon.damage;

            print("Dealing damage :" + weapon.damage);

            other.GetComponent<DamageAnim>().take_damage();

            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Land"))
        {
            Destroy(gameObject);
        }
    }
}
