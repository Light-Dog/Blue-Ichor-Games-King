using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAction : WeaponAction
{
    public KeyCode blockButton;

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

            UpdateHoldFrame(CheckButtonHold());

            if (GetCurrentFrame() == GetMaxFrames())
                ResetData();
        }
    }

    private bool CheckButtonHold()
    {
        if (Input.GetKey(blockButton))
            return true;

        return false;
    }

    public bool BlockCheck()
    {
        if (Input.GetKeyDown(blockButton))
            return true;

        return false;
    }
}
