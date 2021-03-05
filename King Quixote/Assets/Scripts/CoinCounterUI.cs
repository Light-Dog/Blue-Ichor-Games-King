using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterUI : MonoBehaviour
{
    public Image[] emptyCoins = new Image[5];
    public int filledCoins = 0;
    public int numFilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        filledCoins = FindObjectOfType<KingMe>().GetCoins();
    }

    // Update is called once per frame
    void Update()
    {
        filledCoins = FindObjectOfType<KingMe>().GetCoins();

        if (numFilled < filledCoins)
        {
            emptyCoins[numFilled].color = new Vector4(1, 1, 0, 1);
            numFilled++;
        }
    }
}
