using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAction : WeaponAction
{
    string button1 = "Attack 1";
    string button2 = "Attack 2";

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        actionType = typeOfAction.Counter;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckActive())
        {
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
        }
    }

    public bool CounterStart()
    {
        if (InputManager.GetKeyDown(button1) || InputManager.GetKeyDown(button2))
        {
            ActivateAction();
            return true;
        }

        return false;
    }
}
