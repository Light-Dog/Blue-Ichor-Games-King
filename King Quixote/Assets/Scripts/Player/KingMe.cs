using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMe : MonoBehaviour
{
    public static KingMe instance;
    public GameObject kingMe;
    int coins;
    public int goalCoins = 10;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            coins = 0;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        kingMe = GameObject.FindGameObjectWithTag("King");
    }

    public void AddCoin()
    {
        coins++;
    }

    public int GetCoins()
    {
        return coins;
    }

    public bool CoinsCollected()
    {
        if (coins >= goalCoins)
            return true;

        return false;
    }
}
