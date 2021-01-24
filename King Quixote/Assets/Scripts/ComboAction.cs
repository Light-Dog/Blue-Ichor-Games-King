using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAction : WeaponAction
{
    public string comboName;
    public List<KeyCode> buttons;
    public List<int> keyframes;  //make into a window of frames to recieve input
    public bool comboEnabled = false;
    public int comboIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckActive())
        {
            //play Sound
            if (gameObject.GetComponentInChildren<AudioSource>().isPlaying == false)
                gameObject.GetComponentInChildren<AudioSource>().Play();

            //update sprite
            UpdateFrame();

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
        }
    }

    public bool ContinueCombo(KeyCode keyPress, int currrentFrame)
    {
        if(currrentFrame == keyframes[comboIndex])
        {
            if(keyPress == buttons[comboIndex])
            {
                comboIndex++;
                comboEnabled = true;
            }

            comboEnabled = false;
        }

        if(comboIndex == buttons.Capacity)
        {
            ActivateAction();
        }

        return comboEnabled;
    }

    public bool ComboEnabled()
    {
        return comboEnabled;
    }
}
