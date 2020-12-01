using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCycle : MonoBehaviour
{
    public Sprite Frame1;
    public Sprite Frame2;
    public Sprite Frame3;
    public Sprite Frame4;
    public int currentFrame = 1;
    public int maxFrame = 1;
    public float animationSpeed = 0.2f;
    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFrame < maxFrame)
        {
            if (timer < animationSpeed)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0.0f;
                currentFrame++;
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
                timer = 0.0f;
                currentFrame = 1;
            }
        }

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
    }
}
