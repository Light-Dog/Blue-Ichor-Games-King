using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class for all weapon actions
//children: attack, block, combo, dodge

public class WeaponAction : MonoBehaviour
{
    public enum typeOfAction { Attack, Combo, Block, Dash, Counter, none };

    //frames for animation
    public Sprite[] frames;
    public int[] activeFrames;

    int currentFrame = 0;
    int maxFrames = 0;
    int activeFrameIndex = 0;

    //2d box collider
    public List<BoxCollider2D> actionColliders;

    public float energyCost = 0.0f;
    public float repositionX = 0.0f;
    public bool lerp = false;
    public typeOfAction actionType = typeOfAction.none;
    public AudioSource sfx = null;

    public WeaponController parent;
    GameObject player;
    bool active = false;

    SpriteRenderer[] hitboxs;
    public Color hitboxColor;
    Color savedColor;

    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        parent = gameObject.GetComponentInParent<WeaponController>();

        hitboxs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        if(hitboxs.Length != 0)
            savedColor = hitboxs[0].color;
        foreach(SpriteRenderer childHitbox in hitboxs)
        {
            childHitbox.enabled = false;
        }


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

        if (sfx != null)
        {
            if (!sfx.isPlaying)
                sfx.Play();
        }
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

    public GameObject GetPlayer()
    {
        return player;
    }

    public void UpdateFrame()
    {
        player.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        if (player.GetComponent<PlayerController>().drawCollider)
            DrawCollider();

        if(ActiveFrameCheck())
            actionColliders[activeFrameIndex].enabled = true;

        if(currentFrame < maxFrames)
        {
            if(parent.TimerUpdate())
            {
                if (ActiveFrameCheck())
                    activeFrameIndex++;

                currentFrame++;

                if (lerp)
                    Move();

                foreach (BoxCollider2D collider in actionColliders)
                {
                    //print("Reset Hitbox");
                    collider.enabled = false;
                }
            }
        }
    }

    public void UpdateHoldFrame(bool held, int holdFrame)
    {
        player.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        if (ActiveFrameCheck())
            actionColliders[activeFrameIndex].enabled = true;
        

        if (currentFrame < maxFrames)
        {
            if(holdFrame == currentFrame && held)
            {

            }
            else if (parent.TimerUpdate())
            {
                if (ActiveFrameCheck())
                    activeFrameIndex++;

                currentFrame++;

                foreach (BoxCollider2D collider in actionColliders)
                    collider.enabled = false;
            }
            
        }
    }

    public void ResetData()
    {
        currentFrame = 0;
        activeFrameIndex = 0;
        active = false;

        player.GetComponent<AnimationCycle>().PauseAnimation(false);
    }

    public bool ActiveFrameCheck()
    {
        if (activeFrameIndex < activeFrames.Length)
            return (currentFrame == activeFrames[activeFrameIndex]);

        return false;
    }

    private void Move()
    {
        if (player.GetComponent<PlayerController>().m_FacingRight)
            player.transform.position = new Vector3(player.transform.position.x + repositionX, player.transform.position.y, player.transform.position.z);
        else
            player.transform.position = new Vector3(player.transform.position.x - repositionX, player.transform.position.y, player.transform.position.z);
    }

    public void DrawCollider(bool drawAll = false)
    {
        if(ActiveFrameCheck())
        {
            hitboxs[activeFrameIndex].enabled = true;
            hitboxs[activeFrameIndex].color = hitboxColor;
        }
        else if(drawAll)
        {
            hitboxs[activeFrameIndex].enabled = true;
            hitboxs[activeFrameIndex].color = savedColor;
        }
        else
        {
            hitboxs[activeFrameIndex].enabled = false;
        }
    }

}
