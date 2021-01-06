using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    //frames for attack animation
    public Sprite[] attackFrames;
    public int[] activeFrames;

    int currentFrame = 0;
    int maxSpriteSize = 0;
    int activeFrameIndex = 0;

    //animation Update & frame control
    public WeaponController parentContoller;
    public float animationMultiplier = 1.0f;

    //2d box collider
    public List<BoxCollider2D> attackColliders;

    //Input
    public KeyCode attackButton;
    public float energyCost = 0.0f;

    //player access
    GameObject player;
    bool attack = false;

    //----------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        print("Weapon Start");

        //Get Player object
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Check for single attack collider
        if(gameObject.GetComponent<BoxCollider2D>() != null)
        {
            attackColliders.Add(gameObject.GetComponent<BoxCollider2D>());
        }
        else
        {
            //Loops through childrens BoxColliders
            BoxCollider2D[] temp = gameObject.GetComponentsInChildren<BoxCollider2D>();
            for(int i = 0; i < temp.Length; i++)
            {
                attackColliders.Add(temp[i]);
            }
        }

        attack = false;

        //Disable colliders
        for(int i = 0; i < attackColliders.Count; i++)
        {
            attackColliders[i].enabled = false;
            //activeFrames[i] -= 1;
        }

        maxSpriteSize = attackFrames.Length;
        energyCost = energyCost / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Weapon Update");

        //if the player attacks...
        if(attack == true)
        {
            //update sprite
            player.GetComponent<SpriteRenderer>().sprite = attackFrames[currentFrame];

            //if the frame is the next attack frame, enable the collider
            if (ActiveFrameCheck())
            {
                //print("Collider Enabled");
                attackColliders[activeFrameIndex].enabled = true;
                activeFrameIndex++;
            }


            //Frame Update
            if (currentFrame < maxSpriteSize)
            {
                if (parentContoller.timerUpdate())
                {
                    //Change Frame
                    currentFrame++;

                    //on frame change disable colliders
                    foreach (BoxCollider2D collider in attackColliders)
                        collider.enabled = false;

                }
            }

            //resets frame and index date for next attack
            if (currentFrame == maxSpriteSize)
            {
                //print("End of Attack");
                currentFrame = 0;
                activeFrameIndex = 0;

                attack = false;
                player.GetComponent<AnimationCycle>().PauseAnimation(false);
            }
        }

    }

    //----------------------------------------------------------------------------------------------------

    public void CancelAttack()
    {
        currentFrame = 0;
        activeFrameIndex = 0;
        attack = false;

        foreach (BoxCollider2D collider in attackColliders)
            collider.enabled = false;
    }

    public bool AttackTrigger()
    {
        return attack;
    }

    //checks if this frame is an active frame
    public bool ActiveFrameCheck()
    {
        if(activeFrameIndex < activeFrames.Length)
            return currentFrame == activeFrames[activeFrameIndex];

        return false;
    }

    //returns current frame
    public int GetCurrentFrame()
    {
        return currentFrame;
    }

    //triggers attack
    public void AttackWithWeapon()
    {
        //check for button press, and if so set attack to true
        attack = true;

        //Pause the idle animation
        player.GetComponent<AnimationCycle>().PauseAnimation(true);

        print("Input Recieved, attacking now");
    }

}
