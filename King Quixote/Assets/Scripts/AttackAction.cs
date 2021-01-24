using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : WeaponAction
{
    public KeyCode attackButton;

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

    public bool AttackCheck()
    {
        if(Input.GetKeyDown(attackButton))
            return true;
        
        return false;
    }
}
