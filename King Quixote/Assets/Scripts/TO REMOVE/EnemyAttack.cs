using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int enemyDamage = 2;
    public AnimationCycle attack;
    public BoxCollider2D attackCollider;
    public int frame = 0;

    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<AttackController>() != null || collision.gameObject.GetComponent<ShieldBlock>() != null)
        {
            print("Player Damaged!");

            if(player.GetComponentInChildren<ShieldBlock>().EnemyBlockCheck())
                player.GetComponentInChildren<DamageTaken>().shieldOuch();
            else
                player.GetComponentInChildren<DamageTaken>().ouch();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attackCollider.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        frame = attack.currentFrame;
        //print(frame)

        if (attack.currentFrame == 4 || attack.currentFrame == 5)
        {
            attackCollider.enabled = true;
        }
        else
            attackCollider.enabled = false;
    }
}
