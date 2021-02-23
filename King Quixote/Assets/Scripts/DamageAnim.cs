using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnim : MonoBehaviour
{
    //this is a short animation that can be applied to any sprite.
    //it causes the sprite to quickly bounce up and inward
    //then returns to the starting position

    public float animationSpeed = 0.02f;
    public float maxHeight = 1.5f;
    public float minWidth = 0.8f;

    private bool damaged = false;
    private bool maxedOut = false;

    private float timer = 0.0f;
    private float maxTimer = 1f;

    private SpriteRenderer npc;
    private Vector2 newSize;
    private Vector3 oldSize;

    // Start is called before the first frame update
    void Start()
    {
        npc = gameObject.GetComponent<SpriteRenderer>();

        newSize = new Vector2(minWidth, maxHeight);
        oldSize = npc.size;

        maxTimer = animationSpeed * 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged == true)
        {
            if (maxedOut == false)
            {
                if (timer < maxTimer)
                {
                    timer += Time.deltaTime;
                    Vector2 tempSize = Vector2.Lerp(npc.size, newSize, animationSpeed);
                    npc.size.Set(tempSize.x, tempSize.y);
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
                    Vector2 tempSize = Vector2.Lerp(npc.size, oldSize, animationSpeed);
                    npc.size.Set(tempSize.x, tempSize.y);
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


    public void take_damage()
    {
        damaged = true;
        print("here");
    }

}
