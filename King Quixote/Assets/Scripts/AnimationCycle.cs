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

    public Sprite jumping;
    public Sprite falling;
    private bool rising = false;
    private bool descending = false;

    public bool reverseIdle = false;

    public bool lerp = false;
    public float repositionX = 0.0f;

    public int currentFrame = 0;
    public int maxFrame;
    private int maxMoveFrame = 0;
    private int maxDeathFrame = 0;
    float timer = 0.0f;

    public GameObject unitController = null;
    PlayerController player = null;
    EnemyController enemy = null;
    UnitType type = UnitType.none;

    public bool facingRight = false;
    private bool isMoving = false;

    bool forwardPlay = true;
    bool pause = false;
    bool moveUnit = false;
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
        else if(unitController != null && unitController.GetComponent<EnemyController>())
        {
            enemy = unitController.GetComponent<EnemyController>();
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

        if(!reverseIdle)
        {
            UpdateFrame();

            if (currentFrame == maxFrame)
            {
                currentFrame = 0;
                forwardPlay = true;
            }
        }
        else
        {

            if (forwardPlay)
                UpdateFrame();
            else
                ReverseUpdate();

            if(currentFrame == maxFrame)
            {
                forwardPlay = false;
                currentFrame--;
            }

            if(currentFrame < 0)
            {
                currentFrame = 0;
                forwardPlay = true;
            }
        }
    }

    void MoveUpdate()
    {
        if (currentFrame >= 0 && currentFrame < maxMoveFrame)
        {
            if(!AerialCheck())
                gameObject.GetComponent<SpriteRenderer>().sprite = moveFrames[currentFrame];
        }

        if (timerUpdate())
        {
            if (currentFrame < maxMoveFrame)
            {
                MoveAnimation();
                currentFrame++;
            }
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

    void UpdateFrame()
    {
        if(currentFrame >= 0 && currentFrame < maxFrame)
        {
            if (!AerialCheck())
                gameObject.GetComponent<SpriteRenderer>().sprite = idleFrames[currentFrame];
        }

        if (timerUpdate())
        {
            if (currentFrame < maxFrame)
                currentFrame++;
        }
    }

    void ReverseUpdate()
    {
        if (!AerialCheck())
            gameObject.GetComponent<SpriteRenderer>().sprite = idleFrames[currentFrame];

        if (timerUpdate())
        {
            if (currentFrame >= 0)
                currentFrame--;
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
        if(type == UnitType.enemy)
        {
            if(isMoving != enemy.moving)
            {
                isMoving = enemy.moving;
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
            if(facingRight)
                unitController.transform.position = new Vector3(unitController.transform.position.x + repositionX, unitController.transform.position.y, unitController.transform.position.z);
            else
                unitController.transform.position = new Vector3(unitController.transform.position.x - repositionX, unitController.transform.position.y, unitController.transform.position.z);

        }
    }

    public void PauseAnimation(bool toPause)
    {
        pause = toPause;
    }

    private bool AerialCheck()
    {
        if(rising)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = jumping;
            return true;
        }

        if(descending)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = falling;
            return true;
        }

        return false;
    }

    public void StartJump() { rising = true; }
    public void PeakJump() { descending = true;  rising = false; }
    public void Landed() { descending = false; }
}
