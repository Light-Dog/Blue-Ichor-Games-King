using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{

    //frames for block animation
    public Sprite[] blockFrames;

    //out of 3 blocking frames the 2nd is the block
    int activeFrame = 1;

    int currentFrame = 0;
    int maxFrameCount = 0;

    public BoxCollider2D blockCollider = null;

    //Input
    public KeyCode blockButton;
    public float energyCost = 0.0f;

    GameObject player;
    bool blocking = false;
    bool holdBlock = false;

    public float animationSpeed = 0.2f;
    float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Check for single attack collider
        if (blockCollider == null)
        {
            blockCollider = gameObject.GetComponent<BoxCollider2D>();
        }

        maxFrameCount = blockFrames.Length;
        blockCollider.enabled = false;
        energyCost = energyCost / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(blockButton))
        {
            blocking = true;
            player.GetComponent<AnimationCycle>().PauseAnimation(true);
        }

        //while key is down, play to the block animation and hold
        if(blocking == true)
        {
            print("Blocking!");

            player.GetComponent<SpriteRenderer>().sprite = blockFrames[currentFrame];

            if (Input.GetKey(blockButton))
                holdBlock = true;
            else
            {
                holdBlock = false;
            }

            if(currentFrame == activeFrame)
            {
                print("HA blocked");
                blockCollider.enabled = true;

                //if an attack hits in this frame => flash to indicate counter
            }

            if(currentFrame < maxFrameCount)
            {
                timerUpdate();
            }

            if (currentFrame == maxFrameCount)
            {
                print("Block Complete");
                currentFrame = 0;

                blocking = false;
                holdBlock = false;
                player.GetComponent<AnimationCycle>().PauseAnimation(false);
            }
        }

        //on release finish the animation & disable hitbox
    }

    public float blockCheck()
    {
        float cost = 0.0f;

        if(Input.GetKeyDown(blockButton))
        {
            print("Block Activated");

            blocking = true;
            player.GetComponent<AnimationCycle>().PauseAnimation(true);

            cost = energyCost;
        }

        return cost;
    }

    void timerUpdate()
    {
        if (timer < animationSpeed)
            timer += Time.deltaTime;
        else
        {
            timer = 0.0f;

            if (currentFrame == activeFrame && holdBlock)
            {
                //do nothing
            }
            else
            {
                currentFrame++;
                blockCollider.enabled = false;
            }
        }
    }

    public bool blockComplete()
    {
        return blocking; 
    }
}
