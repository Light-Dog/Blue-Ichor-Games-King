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
            UpdateFrame();

            if (currentFrame == maxFrames)
                ResetData();
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
