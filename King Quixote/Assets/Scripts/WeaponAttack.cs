using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    //frames for attack animation
    public Sprite[] attackFrames;
    int currentFrame = 0;
    int maxSpriteSize = 0;
    public int[] activeFrames;
    int activeFrameIndex = 0;

    public float animationSpeed = 0.2f;
    float timer = 0.0f;

    //2d box collider
    public List<BoxCollider2D> attackColliders;
    public int damage = 2;
    //int colliderCounter = 0;

    //player access
    GameObject player;
    bool attack = false;
    bool attacking = false;

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
            activeFrames[i] -= 1;
        }

        maxSpriteSize = attackFrames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Weapon Update");

        //check for button press, and if so set attack to true
        if (Input.GetKeyUp(KeyCode.F) == true)
        {
            attack = true;

            //Pause the idle animation
            player.GetComponent<AnimationCycle>().PauseAnimation(true);

            print("Input Recieved, attacking now");
        }

        //if the player attacks...
        if(attack == true)
        {
            //print("Attacking....");

            //update sprite
            player.GetComponent<SpriteRenderer>().sprite = attackFrames[currentFrame];

            //if the frame is the next attack frame, enable the collider
            if (ActiveFrameCheck())
            {
                //print("Collider Enabled");
                attackColliders[activeFrameIndex].enabled = true;
                attacking = true;
                activeFrameIndex++;
            }
                

            if (currentFrame < maxSpriteSize)
            {
                if(timer < animationSpeed)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    //Change Frame
                    FrameChange();
                    currentFrame++;
                    timer = 0.0f;
                    attacking = false;
                }
            }

            //disabel lance collider after animation finishes
            if (currentFrame == maxSpriteSize)
            {
                currentFrame = 0;
                activeFrameIndex = 0;

                timer = 0.0f;
                attack = false;
                player.GetComponent<AnimationCycle>().PauseAnimation(false);
            }

        }
    }

    public bool ActiveFrameCheck()
    {
        if(activeFrameIndex < activeFrames.Length)
            return currentFrame == activeFrames[activeFrameIndex];

        return false;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    void FrameChange()
    {
        //on frame change disable colliders
        foreach (BoxCollider2D collider in attackColliders)
        {
            //print("disableing collider: " + colliderCounter++);
            collider.enabled = false;
        }
        //colliderCounter = 0;
    }
}
