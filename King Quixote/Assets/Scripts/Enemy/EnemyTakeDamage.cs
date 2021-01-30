using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    EnemyController parent;
    PlayerController player;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        foreach (SpriteRenderer hitbox in gameObject.GetComponentsInChildren<SpriteRenderer>())
            hitbox.enabled = false;
    }


    //take damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponentInParent<AttackAction>() || other.GetComponentInParent<ComboAction>())
        {
            print("in Trigger");

            parent.health -= player.DealDamage();
            parent.GetComponent<DamageAnim>().take_damage();
        }
    }

}
