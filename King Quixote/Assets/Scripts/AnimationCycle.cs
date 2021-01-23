using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCycle : MonoBehaviour
{
    public Sprite[] frames;
    public float animationSpeed = .2f;
    public bool reverse = false;

    public bool lerp = false;
    public float repositionX = 0.0f;

    bool forward = true;
    public int currentFrame = 0;
    public int maxFrame;
    float timer = 0.0f;
    bool pause = false;

    bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        //Checks for max size of the spirte list
        maxFrame = frames.Length;

        //repositionX *= transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //pause for attack animation
        if (pause == false)
        {
            if(currentFrame >= 0 && currentFrame < maxFrame)
                gameObject.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

            if(forward)
            {
                if(timerUpdate())
                {
                    if (currentFrame < maxFrame)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        forward = false;
                        move = true;
                    }


                }
            }
            else
            {
                if(reverse)
                {
                    if(timerUpdate())
                    {
                        if (currentFrame == 0)
                            forward = true;
                        else
                            currentFrame--;
                    }
                }
                else
                {
                    currentFrame = 0;
                    forward = true;
                    if (lerp)
                    {
                        if(move)
                        {
                            transform.position = new Vector3(transform.position.x + repositionX, transform.position.y, transform.position.z);
                            move = false;
                        }
                    }

                }
            }
        }
    }

    bool timerUpdate()
    {
        if(timer < animationSpeed)
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

    public void PauseAnimation(bool toPause)
    {
        pause = toPause;
    }
}
