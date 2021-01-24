using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class for all weapon actions
//children: attack, block, combo, dodge

public class WeaponAction : MonoBehaviour
{
    //frames for animation
    public Sprite[] frames;
    public int[] activeFrames;

    int currentFrame = 0;
    int maxFrames = 0;
    int activeFrameIndex = 0;

    //2d box collider
    List<BoxCollider2D> actionColliders;

    public float energyCost = 0.0f;

    public float repositionX = 0.0f;

    WeaponController parent;
    GameObject player;
    bool active = false;
    bool playSound = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        BoxCollider2D[] childBoxes = gameObject.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in childBoxes)
        {
            collider.enabled = false;
            actionColliders.Add(collider);
        }

        maxFrames = frames.Length;
        energyCost = energyCost / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancelAction()
    {
        currentFrame = 0;
        activeFrameIndex = 0;
        active = false;

        foreach (BoxCollider2D collider in actionColliders)
            collider.enabled = false;
    }

    public void ActivateAction()
    {
        player.GetComponent<AnimationCycle>().PauseAnimation(true);
        active = true;
    }

    public bool CheckActive()
    {
        return active;
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }

    public int GetMaxFrames()
    {
        return maxFrames;
    }

    public void UpdateFrame()
    {
        player.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        if(ActiveFrameCheck())
            actionColliders[activeFrameIndex].enabled = true;

        if(currentFrame < maxFrames)
        {
            if(parent.TimerUpdate())
            {
                currentFrame++;

                if (ActiveFrameCheck())
                    activeFrameIndex++;

                foreach (BoxCollider2D collider in actionColliders)
                    collider.enabled = false;
            }
        }
    }

    public void UpdateHoldFrame(bool held)
    {
        player.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        if (ActiveFrameCheck())
            actionColliders[activeFrameIndex].enabled = true;
        

        if (currentFrame < maxFrames)
        {
            if(ActiveFrameCheck() && held)
            {
                //dont update timer
            }
            else
            {
                if (parent.TimerUpdate())
                {
                    currentFrame++;

                    if (ActiveFrameCheck())
                        activeFrameIndex++;

                    foreach (BoxCollider2D collider in actionColliders)
                        collider.enabled = false;
                }
            }
        }
    }

    public void ResetData()
    {
        currentFrame = 0;
        activeFrameIndex = 0;
        active = false;

        if(player.GetComponent<PlayerController>().m_FacingRight)
            player.transform.position = new Vector3(player.transform.position.x + repositionX, player.transform.position.y, player.transform.position.z);
        else
            player.transform.position = new Vector3(player.transform.position.x - repositionX, player.transform.position.y, player.transform.position.z);

        player.GetComponent<AnimationCycle>().PauseAnimation(false);
    }

    private bool ActiveFrameCheck()
    {
        if (activeFrameIndex < activeFrames.Length)
            return (currentFrame == activeFrames[activeFrameIndex]);

        return false;
    }
}
