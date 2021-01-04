using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool broken = false;
    public Sprite become;
    public GameObject chip1;
    public GameObject chip2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            broken = true;
        }

        if (broken == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            gameObject.GetComponent<SpriteRenderer>().sprite = become;
            Instantiate(chip1, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(chip2, gameObject.transform.position, gameObject.transform.rotation);

            if (gameObject.GetComponent<BoxCollider2D>())
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            broken = false;
        }
    }
}
