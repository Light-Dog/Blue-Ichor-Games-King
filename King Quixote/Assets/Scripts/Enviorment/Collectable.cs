using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool heal = false;
    public float timer = 3.0f;
    bool pickUp = false;

    bool destroy = false;
    float deathtimer = 0f;

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
            
        if(destroy)
        {
            if (deathtimer < 1f)
                deathtimer += Time.deltaTime;
            else
                Destroy(gameObject);
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

            //gameObject.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }
        else if (other.GetComponent<PlayerController>() && !heal && !destroy)
        {
            other.GetComponent<PlayerController>().coins++;
            gameObject.GetComponent<AudioSource>().Play();

            gameObject.GetComponent<SpriteRenderer>().color = Vector4.zero;
            destroy = true;
        }
    }

}
