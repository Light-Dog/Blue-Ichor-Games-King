using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AttackAction : AttackAction
{
    private float chargeTime = 0.0f;
    private float minTime = .6f;
    private float maxTime = 1.0f;
    private bool held = false;
    private bool first = false;

    public int holdFrame = 0;
    public float chargeForce = 6.0f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        first = true;

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
            if (first)
            {
                held = true;
                first = false;
            }

            if(Charge())
            {
                if (GetPlayer().GetComponent<PlayerController>().m_FacingRight)
                    GetPlayer().GetComponent<Rigidbody2D>().AddForce(new Vector3(1.0f, 0f, 0.0f) * chargeForce, ForceMode2D.Impulse);
                else
                    GetPlayer().GetComponent<Rigidbody2D>().AddForce(new Vector3(-1.0f, 0f, 0.0f) * chargeForce, ForceMode2D.Impulse);
            }

            //dont hardcode the frame
            UpdateHoldFrame(held, holdFrame);

            if (GetCurrentFrame() == GetMaxFrames())
            {
                ResetData();
                first = true;
            }
        }
    }

    private bool Charge()
    {
        if(held)
        {
            if(chargeTime < maxTime)
            {
                chargeTime += Time.deltaTime;

                if(InputManager.GetKeyUp(buttonName))
                {
                    held = false;

                    if(chargeTime >= minTime)
                    {
                        chargeTime = 0.0f;
                        return true;
                    }

                    chargeTime = 0.0f;
                }
            }
            else
            {
                held = false;
                chargeTime = 0.0f;
            }

            
        }

        return false;
    }

}
