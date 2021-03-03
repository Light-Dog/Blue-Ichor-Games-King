using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAction : WeaponAction
{
    public string comboName;
    public List<string> buttons;
    public List<int> keyframes;  //make into a window of frames to recieve input
    public bool comboEnabled = false;
    public int comboIndex = 0;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        actionType = typeOfAction.Combo;
    }

    // Update is called once per frame
    void Update()
    {
        //Activate Once a Combo is complete
        if(CheckActive())
        {
            //update sprite
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
        }
    }

    public bool CheckComboComplete()
    {
        if (comboIndex == buttons.Capacity)
        {
            comboIndex = 0;
            comboEnabled = false;

            ActivateAction();
            return true;
        }

        return false;
    }

    public bool ContinueCombo(string button, int currrentFrame)
    {
        if(currrentFrame == keyframes[comboIndex])
        {
            if(button == buttons[comboIndex])
            {
                comboIndex++;
                comboEnabled = true;
            }
            else
                comboEnabled = false;
        }

        return comboEnabled;
    }

    public void ResetCombo()
    {
        comboEnabled = false;
        comboIndex = 0;
        base.CancelAction();
    }

    public bool ComboEnabled()
    {
        return comboEnabled;
    }
}
