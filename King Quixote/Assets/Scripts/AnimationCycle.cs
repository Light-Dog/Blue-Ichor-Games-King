using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCycle : MonoBehaviour
{
    enum UnitType { player, enemy, none };

    public Sprite[] idleFrames;
    public Sprite[] moveFrames;
    public Sprite[] deathFrames;
    public float animationSpeed = .2f;
    public bool reverseIdle = false;

    public bool lerp = false;
    public float repositionX = 0.0f;

    public int currentFrame = 0;
    public int maxFrame;
    int maxMoveFrame = 0;
    int maxDeathFrame = 0;
    float timer = 0.0f;

    public GameObject unitController = null;
    PlayerController player = null;
    //EnemyController enemy = null;
    UnitType type = UnitType.none;

    bool forwardPlay = true;
    bool pause = false;
    bool moveUnit = false;
    bool isMoving = false;
    bool killUnit = false;

    // Start is called before the first frame update
    void Start()
    {
        //Checks for max size of the spirte list
        maxFrame = idleFrames.Length;
        maxMoveFrame = moveFrames.Length;
        maxDeathFrame = deathFrames.Length;

        if(unitController != null && unitController.GetComponent<PlayerController>() != null)
        {
            player = unitController.GetComponent<PlayerController>();
            type = UnitType.player;
        }
        else if(unitController != null)// && unitController.GetComponent<EnemyController>()
        {
            //enemy = unitController.GetComponent<EnemyController>();
            type = UnitType.enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (killUnit)
            DeathUpdate();
        else if (pause == false)
        {
            MoveCheck();
            if (isMoving)
                MoveUpdate();
            else
                IdleUpdate();
        }
    }

    bool timerUpdate(float multiplier = 1.0f)
    {
        if(timer < animationSpeed * multiplier)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
            return true;
        }

        return false;
    }

    void IdleUpdate()
    {
        if (currentFrame >= 0 && currentFrame < maxFrame)
            gameObject.GetComponent<SpriteRenderer>().sprite = idleFrames[currentFrame];

        if (forwardPlay)
        {
            if (timerUpdate())
            {
                if (currentFrame < maxFrame)
                    currentFrame++;
                else
                {
                    forwardPlay = false;
                    moveUnit = true;
                }
            }
        }
        else
        {
            if (reverseIdle)
            {
                if (timerUpdate())
                {
                    if (currentFrame == 0)
                        forwardPlay = true;
                    else
                        currentFrame--;
                }
            }
            else
            {
                currentFrame = 0;
                forwardPlay = true;

                MoveAnimation();
            }
        }
    }

    void MoveUpdate()
    {
        if (currentFrame >= 0 && currentFrame < maxMoveFrame)
            gameObject.GetComponent<SpriteRenderer>().sprite = moveFrames[currentFrame];

        if (timerUpdate())
        {
            if (currentFrame < maxMoveFrame)
                currentFrame++;
            else
            {
                currentFrame = 0;
            }
        }
    }

    void DeathUpdate()
    {
        if (currentFrame >= 0 && currentFrame < maxDeathFrame)
            gameObject.GetComponent<SpriteRenderer>().sprite = deathFrames[currentFrame];

        if (timerUpdate(2.0f))
        {
            if (currentFrame < maxDeathFrame)
                currentFrame++;
            else
            {
                currentFrame = 0;
                pause = true;
                killUnit = false;
            }
        }
    }

    //returns true if moving
    void MoveCheck()
    {
        if(type == UnitType.player)
        {
            if(isMoving != player.moving)
            {
                isMoving = player.moving;
                currentFrame = 0;
            }
        }
    }

    public void DeathCheck()
    {
        currentFrame = 0;

        killUnit = true;
        pause = true;
    }

    void MoveAnimation()
    {
        if (lerp)
        {
            if (moveUnit)
            {
                transform.position = new Vector3(transform.position.x + repositionX, transform.position.y, transform.position.z);
                moveUnit = false;
            }
        }
    }

    public void PauseAnimation(bool toPause)
    {
        pause = toPause;
    }
}
