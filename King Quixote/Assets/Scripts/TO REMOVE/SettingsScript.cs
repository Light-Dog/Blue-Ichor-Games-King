using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public GameObject forKings = null;
    public bool hidden = true;
    bool once = false;
    KingMe instance;

    private void Update()
    {
        if (FindObjectOfType<KingMe>().CoinsCollected() && !once)
            EnableKing();
    }

    public void coinFlip()
    {
        hidden = false;
    }

    public void EnableKing()
    {
        print("Enabled King");
        forKings.SetActive(true);
        once = true;
    }
}
