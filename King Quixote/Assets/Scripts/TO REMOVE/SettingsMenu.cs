using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMenu()
    {
        if (Menu.activeSelf == true)
        {
            Menu.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            Menu.SetActive(true);
            Time.timeScale = 0.001f;
        }
    
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }
}
