using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_AttackAction : AttackAction
{
    public GameObject bolt;
    public float boltForce = 15.0f;
    public int fireFrame = 1;
    private bool fired = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        if (attackType == 1)
            buttonName = "Attack 1";
        else
            buttonName = "Attack 2";
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckActive())
        {
            UpdateFrame();

            if(GetCurrentFrame() == fireFrame && fired == false)
            {
                //spawn Crossbow Arrow and fire it
                GameObject temp = Instantiate(bolt);
                temp.transform.position = parent.transform.position;

                float direction = -1.0f;
                if (GetPlayer().GetComponent<PlayerController>().m_FacingRight)
                    direction = 1.0f;

                temp.transform.localScale *= direction;

                temp.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0.0f) * boltForce, ForceMode2D.Impulse);

                fired = true;
            }

            if (GetCurrentFrame() == GetMaxFrames())
            {
                ResetData();
                fired = false;
            }

        }
    }
}
