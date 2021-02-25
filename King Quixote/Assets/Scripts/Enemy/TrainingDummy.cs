using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    public EnemyController dummy;
    public Color greyColor;

    private bool cooldown = false;
    private Color heldColor;

    public GameObject straw1;
    public GameObject straw2;
    public GameObject straw3;
    public GameObject straw4;

    public int numstraw = 8;
    private float shotForce = 2.5f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        heldColor = dummy.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(dummy.health <= 0)
        {
            //trigger straw-spolsion
            StrawSplosion();
            //reset health to full
            dummy.health = 5;
            cooldown = true;
            //disable boxcollider for 5 seconds & tint dummy grey for duration
            dummy.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            dummy.gameObject.GetComponent<SpriteRenderer>().color = greyColor;
        }

        if(cooldown)
        {
            //timer for dummy
            if(timer < 5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                dummy.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                dummy.gameObject.GetComponent<SpriteRenderer>().color = heldColor;
                cooldown = false;
                timer = 0.0f;
            }
        }
    }

    void StrawSplosion()
    {
        for (int i = 0; i < numstraw; i++)
        {
            Vector2 upShot1 = new Vector2(Random.Range(-0.05f, 0.25f), Random.Range(.65f, 1.2f));
            Vector2 upShot2 = new Vector2(Random.Range(-.25f, 0.05f), Random.Range(.65f, 1.2f));
            Vector2 upShot3 = new Vector2(Random.Range(-0.05f, 0.25f), Random.Range(.65f, 1.2f));
            Vector2 upShot4 = new Vector2(Random.Range(-.25f, 0.05f), Random.Range(.65f, 1.2f));


            GameObject temp1 = Instantiate(straw1, gameObject.transform.position, gameObject.transform.rotation);
            GameObject temp2 = Instantiate(straw2, gameObject.transform.position, gameObject.transform.rotation);
            GameObject temp3 = Instantiate(straw3, gameObject.transform.position, gameObject.transform.rotation);
            GameObject temp4 = Instantiate(straw4, gameObject.transform.position, gameObject.transform.rotation);

            temp1.GetComponent<Rigidbody2D>().AddForce(upShot1 * shotForce, ForceMode2D.Impulse);
            temp1.GetComponent<Rigidbody2D>().AddTorque(180, ForceMode2D.Impulse);
            temp1.GetComponent<KillTimer>().StartTimer();

            temp2.GetComponent<Rigidbody2D>().AddForce(upShot2 * shotForce, ForceMode2D.Impulse);
            temp2.GetComponent<Rigidbody2D>().AddTorque(-180, ForceMode2D.Impulse);
            temp2.GetComponent<KillTimer>().StartTimer();

            temp3.GetComponent<Rigidbody2D>().AddForce(upShot3 * shotForce, ForceMode2D.Impulse);
            temp3.GetComponent<Rigidbody2D>().AddTorque(-360, ForceMode2D.Impulse);
            temp3.GetComponent<KillTimer>().StartTimer();

            temp4.GetComponent<Rigidbody2D>().AddForce(upShot4 * shotForce, ForceMode2D.Impulse);
            temp4.GetComponent<Rigidbody2D>().AddTorque(-360, ForceMode2D.Impulse);
            temp4.GetComponent<KillTimer>().StartTimer();
        }
    }
}
