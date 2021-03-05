using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateButton : MonoBehaviour
{
    public GameObject menu;
    KeyCode[] oldKeys;

    public GameObject keyText;
    string text = "";
    string emptyText = "";

    bool changeKey = false;
    KeyCode newKey = KeyCode.None;
    int whichKey = 0;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetKeyDown("Escape"))
        {
            ShowMenu();

        }

        if(changeKey)
        {
            newKey = ChangeKey();

            if (newKey != KeyCode.None)
            {
                changeKey = false;

                keyText.GetComponent<TextMeshProUGUI>().text = text;
                text = "";

                UpdateInput(newKey, whichKey);
            }
        }
    }

    public void ChangeButton(int key)
    {
        changeKey = true;
        whichKey = key;
        newKey = KeyCode.None;
        keyText.GetComponent<TextMeshProUGUI>().text = "";
    }

    public KeyCode ChangeKey()
    {
        //a-z + number + shift + control + space + left/right/middle
        if (Input.GetKeyDown(KeyCode.A))
        {
            text = "A";
            return KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            text = "B";
            return KeyCode.B;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            text = "C";
            return KeyCode.C;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            text = "D";
            return KeyCode.D;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            text = "E";
            return KeyCode.E;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            text = "F";
            return KeyCode.F;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            text = "G";
            return KeyCode.G;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            text = "H";
            return KeyCode.H;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            text = "I";
            return KeyCode.I;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            text = "J";
            return KeyCode.J;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            text = "K";
            return KeyCode.K;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            text = "L";
            return KeyCode.L;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            text = "M";
            return KeyCode.M;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            text = "N";
            return KeyCode.N;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            text = "O";
            return KeyCode.O;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            text = "P";
            return KeyCode.P;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            text = "Q";
            return KeyCode.Q;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            text = "R";
            return KeyCode.R;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            text = "S";
            return KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            text = "T";
            return KeyCode.T;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            text = "U";
            return KeyCode.U;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            text = "V";
            return KeyCode.V;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            text = "W";
            return KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            text = "X";
            return KeyCode.X;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            text = "Y";
            return KeyCode.Y;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            text = "Z";
            return KeyCode.Z;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            text = "0";
            return KeyCode.Alpha0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            text = "1";
            return KeyCode.Alpha1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            text = "2";
            return KeyCode.Alpha2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            text = "3";
            return KeyCode.Alpha3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            text = "4";
            return KeyCode.Alpha4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            text = "5";
            return KeyCode.Alpha5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            text = "6";
            return KeyCode.Alpha6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            text = "7";
            return KeyCode.Alpha7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            text = "8";
            return KeyCode.Alpha8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            text = "9";
            return KeyCode.Alpha9;
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            text = "R Shift";
            return KeyCode.RightShift;
        }
        else if (Input.GetKeyDown(KeyCode.RightControl))
        {
            text = "R Ctrl";
            return KeyCode.RightControl;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            text = "Space";
            return KeyCode.Space;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            text = "M1";
            return KeyCode.Mouse0;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            text = "M2";
            return KeyCode.Mouse1;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            text = "M3";
            return KeyCode.Mouse2;
        }


        return KeyCode.None;
    }

    public void ShowMenu()
    {
        if (menu.activeSelf == false)
        {
            menu.SetActive(true);
            Time.timeScale = 0.001f;
            FindObjectOfType<AudioManager>().Deafen(true);
            FindObjectOfType<PlayerController>().paused = true;

            oldKeys = InputManager.GetKeyCodes();
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1.0f;
            FindObjectOfType<AudioManager>().Deafen(false);
            FindObjectOfType<PlayerController>().paused = false;
        }
    }

    public void ResumeToMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Deafen(false);
        FindObjectOfType<PlayerController>().paused = false;
        FindObjectOfType<GoToScene>().GoToMenu_Transition();
    }

    public void MuteMusic()
    {
        if (FindObjectOfType<AudioManager>().MuteSongs())
            keyText.GetComponent<TextMeshProUGUI>().text = "Off";
        else
            keyText.GetComponent<TextMeshProUGUI>().text = "On";
    }

    public void InputChange()
    {
        if (InputManager.SwapBinding())
            keyText.GetComponent<TextMeshProUGUI>().text = "On";
        else
            keyText.GetComponent<TextMeshProUGUI>().text = "Off";
    }

    public void UpdateInput(KeyCode key, int whichKey)
    {
        oldKeys = InputManager.GetKeyCodes();
        bool overwrite = false;

        if (InputManager.CheckKey(key))
            overwrite = true;

        for(int i = 0; i < 9; i++)
        {
            if (overwrite && oldKeys[i] == key)
                oldKeys[i] = KeyCode.None;

            if (i == whichKey)
                oldKeys[i] = key;
        }

        InputManager.UpdateKeyMap(oldKeys);
    }
}
