using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleArc : MonoBehaviour
{
    public float minY = 4f;
    public float maxY = 10f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minRot = -1f;
    public float maxRot = 1f;
    private Rigidbody2D rb2d;
    private float myRot;
    public float lifetime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);

        float myX = Random.Range(minX, maxX);
        float myY = Random.Range(minY, maxY);
        myRot = Random.Range(minRot, maxRot);

        Vector2 myVector = new Vector2(myX, myY);

        if (gameObject.GetComponent<Rigidbody2D>())
        {
            rb2d = gameObject.GetComponent<Rigidbody2D>();
        }

        rb2d.AddForce(myVector);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, myRot, Space.Self);

        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
