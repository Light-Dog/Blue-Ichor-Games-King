using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    public string combo;
    public List<KeyCode> buttons;
    public List<int> keyframes;  //make into a window of frames to recieve input
    public bool comboEnabled = false;
    public int comboIndex = 0;

    //combo sprite data
    public Sprite[] comboFrames;
    public int[] activeFrames;

    bool comboAttack = false;
    public int currentFrame = 0;
    public int maxSpriteSize = 0;
    int activeFrameIndex = 0;
    int damage = 4;

    //animation Update & frame control
    public WeaponController parentContoller;
    public float animationMultiplier = 1.0f;
    public float repositionX = 0.0f;

    //2d box collider
    public List<BoxCollider2D> comboColliders;

    public GameObject player;

    public bool playedSound = false;
    bool directionFacing = true;

    //----------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //repositionX *= player.transform.localScale.x;

        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            comboColliders.Add(gameObject.GetComponent<BoxCollider2D>());
        }
        else
        {
            //Loops through childrens BoxColliders
            BoxCollider2D[] temp = gameObject.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < temp.Length; i++)
            {
                comboColliders.Add(temp[i]);
            }
        }

        comboAttack = false;
        currentFrame = 0;

        //Disable colliders
        for (int i = 0; i < comboColliders.Count; i++)
        {
            comboColliders[i].enabled = false;
            //activeFrames[i] -= 1;
        }

        maxSpriteSize = comboFrames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //repositionX *= player.transform.localScale.x;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.GetComponent<AnimationCycle>().PauseAnimation(true);
            comboAttack = true;
        }

        if(comboAttack == true)
        {
            //audio fx
            if (playedSound == false)
            {
                gameObject.GetComponentInChildren<AudioSource>().Play();
                playedSound = true;
            }

            player.GetComponent<SpriteRenderer>().sprite = comboFrames[currentFrame];

            if (ActiveFrameCheck())
            {
                //print("Collider Enabled");
                comboColliders[activeFrameIndex].enabled = true;
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
                    foreach (BoxCollider2D collider in comboColliders)
                        collider.enabled = false;

                }
            }

            //resets frame and index date for next attack
            if (currentFrame == maxSpriteSize)
            {
                print("End of Attack");
                currentFrame = 0;
                activeFrameIndex = 0;

                directionFacing = player.GetComponent<PlayerController>().m_FacingRight;

                if(directionFacing)
                    player.transform.position = new Vector3(player.transform.position.x + repositionX, player.transform.position.y, player.transform.position.z);
                else
                    player.transform.position = new Vector3(player.transform.position.x - repositionX, player.transform.position.y, player.transform.position.z);

                comboAttack = false;
                player.GetComponent<AnimationCycle>().PauseAnimation(false);

                playedSound = false;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------

    public bool ContinueCombo(KeyCode buttonPress, int currentFrame)
    {
        if (currentFrame == keyframes[comboIndex])
        {
            if (buttonPress == buttons[comboIndex])
                comboEnabled = true;

            comboIndex++;
        }
        else
            comboEnabled = false;

        return comboEnabled;
    }

    //checks if this frame is an active frame
    public bool ActiveFrameCheck()
    {
        if (activeFrameIndex < activeFrames.Length)
            return currentFrame == activeFrames[activeFrameIndex];

        return false;
    }

    public void ComboAttack()
    {
        print("Combo Finisher Start");
        comboAttack = true;
        player.GetComponent<AnimationCycle>().PauseAnimation(true);
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }

    public bool ComboTrigger()
    {
        return comboAttack;
    }
}
