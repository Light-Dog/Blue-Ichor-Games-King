using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public Sprite damagedSprite;

    public GameObject broken1;
    public GameObject broken2;

    public int numChips = 5;
    public float shotForce = 8.0f;

    public int health = 5;
    private bool broken = false;
    private bool summoned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (broken == true && summoned == false)
        {
            //Change Sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprite;

            //Spawn Chips and YEET them UP and a little away
            SummonBroken();

            //disable collider
            if (gameObject.GetComponent<BoxCollider2D>())
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            broken = false;
            summoned = true;
        }

        if (health < 0)
        {
            broken = true;
        }
    }

    public void SummonBroken()
    {

        for (int i = 0; i < numChips; i++)
        {
            Vector2 upShotRight = new Vector2(Random.Range(0.0f, 0.3f), Random.Range(.7f, 1.1f));
            Vector2 upShotLeft = new Vector2(Random.Range(-.3f, 0.0f), Random.Range(.7f, 1.1f));


            GameObject temp1 = Instantiate(broken1, gameObject.transform.position, gameObject.transform.rotation);
            GameObject temp2 = Instantiate(broken2, gameObject.transform.position, gameObject.transform.rotation);

            temp1.GetComponent<Rigidbody2D>().AddForce(upShotRight * shotForce, ForceMode2D.Impulse);
            temp1.GetComponent<Rigidbody2D>().AddTorque(360, ForceMode2D.Impulse);
            temp1.GetComponent<KillTimer>().StartTimer();

            temp2.GetComponent<Rigidbody2D>().AddForce(upShotLeft * shotForce, ForceMode2D.Impulse);
            temp2.GetComponent<Rigidbody2D>().AddTorque(-360, ForceMode2D.Impulse);
            temp2.GetComponent<KillTimer>().StartTimer();
        }
    }

    public void Break()
    {
        broken = true;
    }
}
