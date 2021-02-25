using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AttackAction : AttackAction
{
    private float chargeTime = 0.0f;
    private float minTime = .75f;
    private float maxTime = 1.0f;
    private bool held = false;
    private bool first = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        first = true;
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
                //yeet player
                print("YEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEET");
            }

            //dont hardcode the frame
            UpdateHoldFrame(held, 0);

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

                if(Input.GetKeyUp(attackButton))
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
