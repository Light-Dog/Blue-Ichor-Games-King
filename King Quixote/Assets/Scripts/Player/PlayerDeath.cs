using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Image fadeObject;
    float fadePercent = 0f;
    float fadeColor = 0.0f;
    float fadeSpeed = .5f;
    int deathLevel = 4;
    

    // Start is called before the first frame update
    void Start()
    {
        //fadeObject = fadeObject.GetComponentInChildren<UITag>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerController>().dead == true)
        {
            if (fadePercent < 2f)
            {
                fadePercent += Time.deltaTime;
                fadeColor = fadeObject.color.a + (fadeSpeed * Time.deltaTime);
                fadeObject.color = new Color(fadeObject.color.r, fadeObject.color.g, fadeObject.color.b, fadeColor);

                Debug.Log(fadePercent);
            }
            else
            {
                Debug.Log("dead level");
                fadePercent = 0f;
                SceneManager.LoadScene(deathLevel);
            }
        }
    }
}
