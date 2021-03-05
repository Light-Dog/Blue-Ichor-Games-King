using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPather : MonoBehaviour
{
    public float moveSpeed;
    private float moveSmoothing = .05f;
    private Rigidbody2D body;
    private Vector3 vecRef = Vector3.zero;

    public float moveRange = 15.0f;
    public float attackRange = 6.0f;
    public float direction = 1.0f;
    public float yDistanceCheck = 5.0f;
    public bool inMoveRange = false;
    public bool yCheck;
    public bool inAttackRange = false;
    public bool moving = false;

    private Transform player;

    //consider a function taking the enemy current action or next planned action to update the attack range to adjust for different attacks

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>().gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the enemy is withing attack range -> attack
        RangeCheck();
        CheckSide();
        //check if the player is in the move range 
        if (inMoveRange && !inAttackRange)
            moving = true;
        else
        {
            moving = false;
            if(body)
                body.velocity = Vector3.zero;
        }
        //check the direction to move
    }

    private void CheckSide()
    {
        if (player.localPosition.x < gameObject.GetComponent<Transform>().localPosition.x)
            direction = -1.0f;
        else
            direction = 1.0f;
    }

    private void RangeCheck()
    {
        float x_distance = Mathf.Abs((player.localPosition.x - gameObject.transform.localPosition.x));
        //print("Distance: " + distance);
        float y_distance = Mathf.Abs((player.localPosition.y - gameObject.transform.localPosition.y));
        yCheck = true;

        if (y_distance > yDistanceCheck || y_distance < -yDistanceCheck)
            yCheck = false;

        if (x_distance <= moveRange && yCheck)
            inMoveRange = true;
        else
            inMoveRange = false;

        if (x_distance <= attackRange)
            inAttackRange = true;
        else
            inAttackRange = false;

    }

    public void Move()
    {
        if (moving && body)
        {
            Vector3 targetVelocity = new Vector2(moveSpeed * direction, body.velocity.y);
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref vecRef, moveSmoothing);
        }
        else
        {
            if (body)
                body.velocity = new Vector3(0.0f, body.velocity.y);
        }
    }

    public bool InAttackRange() { return inAttackRange;  }

}
