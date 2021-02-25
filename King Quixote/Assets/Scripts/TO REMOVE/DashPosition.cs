using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPosition : MonoBehaviour
{
    // Start is called before the first frame update

    public KeyCode DashKey = KeyCode.C;
    public float dashLength = 1.0f;
    public float dashHeight = 1.0f;
    public float animationSpeed;
    public bool dashing;
    private Vector3 savedV3;
    private bool jumped = false;
    public Rigidbody2D rb2d;

    void Start()
    {
    }

    public void Dashing()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {

        //if player hits the Dodge key
        if (Input.GetKeyDown(DashKey))
        {
            //enter Dashing state, saves previous position as v3
            dashing = true;
            savedV3 = new Vector3(this.transform.position.x, transform.position.y, transform.position.z);
        }
        /*
        if (dashing == true)
        {
            if (jumped == false)
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dashLength, -1.0f));
                jumped = true;
            }

            //
            if (transform.position.x - savedV3.x < dashLength)
            {
                transform.position = new Vector3(transform.position.x + dashLength, transform.position.y, transform.position.z);
            }
            else
            { 
                
            }
                
        }
        */
    }
}
