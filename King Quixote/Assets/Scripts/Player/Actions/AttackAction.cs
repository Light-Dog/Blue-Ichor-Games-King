using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : WeaponAction
{
    public int attackType = 1;
    public string buttonName;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        actionType = typeOfAction.Attack;

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
            //update sprite
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
            
        }
    }

    public bool AttackStart()
    {
        if (Input.GetButtonDown(buttonName))
        {
            //print("Attack Buytton: " + attackButton);
            ActivateAction();
            return true;
        }

        return false;
    }

    public bool AttackCheck()
    {
        if(Input.GetButtonDown(buttonName))
        {
            return true;
        }
        
        return false;
    }
}
