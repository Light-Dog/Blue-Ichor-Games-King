using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAction : WeaponAction
{
    public KeyCode dashButton;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        actionType = typeOfAction.Dash;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckActive())
        {
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
        }
    }
}
