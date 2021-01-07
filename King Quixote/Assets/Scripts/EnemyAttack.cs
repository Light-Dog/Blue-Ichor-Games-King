﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int enemyDamage = 2;
    public AnimationCycle attack;
    public BoxCollider2D attackCollider;
    public int frame = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<AttackController>() != null)
        {
            print("Player Damaged!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attackCollider.enabled = false;
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
