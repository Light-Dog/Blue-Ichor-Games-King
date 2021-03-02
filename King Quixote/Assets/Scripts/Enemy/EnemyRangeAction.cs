using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAction : EnemyAction
{
    public GameObject spit;
    public float spitForce = 15.0f;
    public int fireFrame = 4;
    private bool fired = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive())
        {
            UpdateFrame();

            if(GetCurrentFrame() == fireFrame && fired == false)
            {
                GameObject temp = Instantiate(spit);
                temp.transform.position = parent.transform.position;
                temp.GetComponent<SpitDamage>().damage = damage;

                float direction = -0.4f;
                if (parent.facingRight)
                    direction = 0.4f;

                temp.transform.localScale *= direction;

                temp.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0.2f) * spitForce, ForceMode2D.Impulse);

                fired = true;
            }

            if (FinalFrameCheck())
            {
                ResetData();
                fired = false;
            }
        }
    }
}
