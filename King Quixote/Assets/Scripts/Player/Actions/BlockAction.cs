using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAction : WeaponAction
{
    public KeyCode blockButton;
    public float blockPercentage = 0.6f;
    public int holdFrame = 1;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        actionType = typeOfAction.Block;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckActive())
        {
  
            UpdateHoldFrame(CheckButtonHold(), holdFrame);

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
        }
    }

    public bool CheckButtonHold()
    {
        if (Input.GetKey(blockButton))
            return true;

        return false;
    }

    public bool BlockCheck()
    {
        if (Input.GetKeyDown(blockButton))
        {
            ActivateAction();
            return true;
        }

        return false;
    }
}
