using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnim : MonoBehaviour
{
    //this is a short animation that can be applied to any sprite.
    //it causes the sprite to quickly bounce up and inward
    //then returns to the starting position

    public bool damaged = false;
    public float animationSpeed = 0.02f;
    public float maxHeight = 1.5f;
    public float minWidth = 0.8f;
    private bool maxedOut = false;
    private float timer = 0.0f;
    private float maxTimer = 1f;
    private Vector3 newSize;
    private Vector3 oldSize;

    // Start is called before the first frame update
    void Start()
    {
        newSize = new Vector3(minWidth, maxHeight);
        oldSize = new Vector3(transform.localScale.x, transform.localScale.y);

        maxTimer = animationSpeed * 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            damaged = true;
        }

        if (damaged == true)
        {
            if (maxedOut == false)
            {
                if (timer < maxTimer)
                {
                    timer += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(transform.localScale, newSize, animationSpeed);
                }
                else
                {
                    timer = 0.0f;
                    maxedOut = true;
                }
            }
            else
            {
                if (timer < maxTimer)
                {
                    timer += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(transform.localScale, oldSize, animationSpeed);
                }
                else
                {
                    timer = 0.0f;
                    maxedOut = false;
                    damaged = false;
                }
            }
        }
    }

}
