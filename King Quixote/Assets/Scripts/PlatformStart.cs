using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStart : MonoBehaviour
{
    public Sprite platform1;
    public Sprite platform2;
    public Sprite platform3;

    private void Awake()
    {
        float rand = (Random.Range(0f, 3f));

        if (rand < 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = platform1;
        }
        else if (rand > 1 && rand < 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = platform2;
        }
        else if (rand > 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = platform3;
        }

        if (Random.Range(0, 2) > 1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
