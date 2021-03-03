using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAction : WeaponAction
{
    string buttonDash = "Dodge";
    public float dashForce = 0.0f;
    public float dashCooldown = .15f;
    bool delay = false;
    float dashTimer = 0.0f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        actionType = typeOfAction.Dash;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay)
        {
            if (dashTimer >= dashCooldown)
            {
                dashTimer = 0.0f;
                delay = false;
                ResetData();
            }
            else
            {
                dashTimer += Time.deltaTime;
            }
        }
        else if(CheckActive())
        {
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
            {
                delay = true;
            }
        }
    }

    public bool DashStart()
    {
        //Give it the yeet
        if (InputManager.GetKeyDown(buttonDash))
        {
            ActivateAction();
            GetPlayer().GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            if(GetPlayer().GetComponent<PlayerController>().m_FacingRight)
                GetPlayer().GetComponent<Rigidbody2D>().AddForce(new Vector3(-1.0f, 0.2f, 0.0f) * dashForce, ForceMode2D.Impulse);
            else
                GetPlayer().GetComponent<Rigidbody2D>().AddForce(new Vector3(1.0f, 0.2f, 0.0f) * dashForce, ForceMode2D.Impulse);

            return true;
        }

        return false;
    }
}
