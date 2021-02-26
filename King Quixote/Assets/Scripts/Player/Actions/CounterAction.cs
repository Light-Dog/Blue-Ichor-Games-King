using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAction : WeaponAction
{
    public KeyCode counterButton;

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
        if (Input.GetKeyDown(counterButton))
        {
            ActivateAction();
            return true;
        }

        return false;
    }
}
