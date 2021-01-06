using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCycle : MonoBehaviour
{
    public Sprite Frame1;
    public Sprite Frame2;
    public Sprite Frame3;
    public Sprite Frame4;
    public Sprite Frame5;
    public Sprite Frame6;
    public int currentFrame = 1;
    public int maxFrame = 1;
    public float animationSpeed = 0.2f;
    public float timer = 0.0f;

    public bool pause = false;
    public float repositionX = 0.0f;
    public bool reverseAtEnd = false;
    private bool reversing = false;

    // Start is called before the first frame update
    void Start()
    {
        //Checks for max size of the spirte list
        if (Frame1 != null)
        {
            maxFrame = 1;
        }
        if (Frame2 != null)
        {
            maxFrame = 2;
        }
        if (Frame3 != null)
        {
            maxFrame = 3;
        }
        if (Frame4 != null)
        {
            maxFrame = 4;
        }
        if (Frame5 != null)
        {
            maxFrame = 5;
        }
        if (Frame6 != null)
        {
            maxFrame = 6;
        }

        repositionX *= transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //pause for attack animation
        if (pause == false)
        {
            //update frame
            if (currentFrame < maxFrame)
            {
                if (timer < animationSpeed)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0.0f;
                    if (reversing == false)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame--;
                    }
                }
                if (reversing == true)
                {
                    if (currentFrame < 1)
                    {
                        timer = 0.0f;
                        currentFrame++;
                        reversing = false;
                    }
                }
            }
            else
            {
                if (timer < animationSpeed)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    //loop animation
                    if (reverseAtEnd == false)
                    {
                        timer = 0.0f;
                        currentFrame = 1;

                        transform.position = new Vector3(transform.position.x + repositionX, transform.position.y, transform.position.z);
                    }
                    //reverse animation
                    else
                    {
                        timer = 0.0f;
                        currentFrame--;
                        reversing = true;

                        //transform.position = new Vector3(transform.position.x - repositionX, transform.position.y, transform.position.z);
                    }
                }
            }


            //change animation
            if (currentFrame == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Frame1;
            }
            if (currentFrame == 2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Frame2;
            }
            if (currentFrame == 3)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Frame3;
            }
            if (currentFrame == 4)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Frame4;
            }
            if (currentFrame == 5)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Frame5;
            }
            if (currentFrame == 6)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Frame6;
            }
        }
    }

    public void PauseAnimation(bool toPause)
    {
        pause = toPause;
    }
}
