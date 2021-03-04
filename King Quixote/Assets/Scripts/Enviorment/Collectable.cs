using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool heal = false;
    public float timer = 3.0f;
    bool pickUp = false;


    // Start is called before the first frame update
    void Update()
    {
        if (!pickUp)
        {
            if (timer >= 0.0f)
                timer -= Time.deltaTime;
            else
                MakePickUp();
        }
            
    }

    public void MakePickUp()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        pickUp = false;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && heal)
        {
            other.GetComponent<PlayerController>().health += 2;
            if (other.GetComponent<PlayerController>().health > 10)
                other.GetComponent<PlayerController>().health = 10;


            Destroy(gameObject);
        }
        else if (other.GetComponent<PlayerController>() && !heal)
        {
            other.GetComponent<PlayerController>().coins++;


            Destroy(gameObject);
        }
    }

}
