using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    //frames for animation
    public Sprite[] frames;
    public int[] activeFrames;

    int currentFrame = 0;
    int maxFrames = 0;
    int activeFrameIndex = 0;

    //2d box collider
    public List<BoxCollider2D> actionColliders;

    public float repositionX = 0.0f;
    public bool lerp = false;

    public bool reverse = false;
    bool forward = true;

    EnemyController parent;
    bool active = false;

    public int damage = 0;


    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<EnemyController>();

        BoxCollider2D[] childBoxes = gameObject.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in childBoxes)
        {
            collider.enabled = false;
            actionColliders.Add(collider);
        }

        maxFrames = frames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if(!reverse)
            {
                UpdateFrame();

                if (currentFrame == maxFrames)
                    ResetData();
            }
            else
            {
                if (forward)
                    UpdateFrame();
                else
                    ReverseFrame();

                if (currentFrame == maxFrames)
                {
                    currentFrame--;
                    forward = false;
                }

                if (currentFrame < 0)
                    ResetData();
            }
        }
    }

    public void UpdateFrame()
    {
        parent.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        if (ActiveFrameCheck())
            actionColliders[activeFrameIndex].enabled = true;

        if (currentFrame < maxFrames)
        {
            if (parent.TimerUpdate())
            {
                if (ActiveFrameCheck())
                    activeFrameIndex++;

                currentFrame++;

                if(lerp)
                    parent.transform.position = new Vector3(parent.transform.position.x + repositionX, parent.transform.position.y, parent.transform.position.z);

                foreach (BoxCollider2D collider in actionColliders)
                {
                    //print("Reset Hitbox");
                    collider.enabled = false;
                }
            }
        }
    }

    public void ReverseFrame()
    {
        parent.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        if (ActiveFrameCheck())
            actionColliders[activeFrameIndex].enabled = true;

        if (currentFrame >= 0)
        {
            if (parent.TimerUpdate())
            {
                if (ActiveFrameCheck())
                    activeFrameIndex++;

                currentFrame--;

                if (lerp)
                    parent.transform.position = new Vector3(parent.transform.position.x - repositionX, parent.transform.position.y, parent.transform.position.z);

                foreach (BoxCollider2D collider in actionColliders)
                {
                    //print("Reset Hitbox");
                    collider.enabled = false;
                }
            }
        }
    }

    public bool ActiveFrameCheck()
    {
        if (activeFrameIndex < activeFrames.Length)
            return (currentFrame == activeFrames[activeFrameIndex]);

        return false;
    }

    public void ResetData()
    {
        currentFrame = 0;
        activeFrameIndex = 0;
        active = false;
        forward = true;

        parent.GetComponent<AnimationCycle>().PauseAnimation(false);
    }

    public void Activate()
    {
        active = true;
        parent.GetComponent<AnimationCycle>().PauseAnimation(true);
    }

    //deal damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player)
            player.TakeDamage(damage);
    }
}
