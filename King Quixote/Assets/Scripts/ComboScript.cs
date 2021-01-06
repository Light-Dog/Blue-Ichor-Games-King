using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    public string combo;
    public List<KeyCode> buttons;
    public List<int> keyframes;
    public bool comboEnabled = false;
    public int comboIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ContinueCombo(KeyCode buttonPress, int currentFrame)
    {
        if (currentFrame == keyframes[comboIndex])
        {
            if (buttonPress == buttons[comboIndex])
                comboEnabled = true;

            comboIndex++;
        }
        else
            comboEnabled = false;

        return comboEnabled;
    }
}
