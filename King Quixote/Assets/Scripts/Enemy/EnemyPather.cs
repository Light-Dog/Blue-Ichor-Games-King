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
    public float direction = 1.0f;
    public bool inMoveRange = false;
    private bool moving = false;

    private Transform player;
    private AnimationCycle animator;

    //consider a function taking the enemy current action or next planned action to update the attack range to adjust for different attacks

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>().gameObject.GetComponent<Transform>();
        animator = gameObject.GetComponent<AnimationCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the enemy is withing attack range -> attack
        RangeCheck();
        CheckSide();
        //check if the player is in the move range 
        if (inMoveRange)
        {
            animator.isMoving = true;
            moving = true;
        }
        else
        {
            animator.isMoving = false;
            moving = false;
        }
        //check the direction to move
        //set move
        Move();
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
        float distance = Mathf.Abs((player.localPosition.x - gameObject.GetComponent<Transform>().localPosition.x));
        //print("Distance: " + distance);

        if (distance <= moveRange)
        {
            animator.PauseAnimation(true);
            inMoveRange = true;
        }
        else
        {
            animator.PauseAnimation(false);
            inMoveRange = false;
        }

    }

    private void Move()
    {
        if (moving && body)
        {
            Vector3 targetVelocity = new Vector2(moveSpeed * direction, body.velocity.y);
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref vecRef, moveSmoothing);
        }
        else
        {
            if (body)
                body.velocity = Vector3.zero;
        }
    }
}
