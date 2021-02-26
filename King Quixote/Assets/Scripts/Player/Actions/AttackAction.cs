using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : WeaponAction
{
    public KeyCode attackButton;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        actionType = typeOfAction.Attack;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckActive())
        {
            //update sprite
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
            
        }
    }

    public bool AttackStart()
    {
        if (Input.GetKeyDown(attackButton))
        {
            //print("Attack Buytton: " + attackButton);
            ActivateAction();
            return true;
        }

        return false;
    }

    public bool AttackCheck()
    {
        if(Input.GetKeyDown(attackButton))
        {
            return true;
        }
        
        return false;
    }
}
