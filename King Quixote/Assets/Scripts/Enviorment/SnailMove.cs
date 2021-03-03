using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float moveDistance = 10.0f;
    public float distanceTravelled = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceTravelled < moveDistance)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
            distanceTravelled += moveSpeed;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
