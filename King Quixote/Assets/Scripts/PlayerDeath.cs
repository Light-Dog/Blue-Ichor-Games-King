using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public GameObject fadeObject;
    public float fadePercent = 0f;
    public int deathLevel = 4;

    // Start is called before the first frame update
    void Start()
    {
        fadeObject = fadeObject.GetComponentInChildren<UITag>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerController>().dead == true)
        {
            if (fadePercent < 1f)
            {
                fadePercent += Time.deltaTime;
                fadeObject.GetComponent<Image>().color = new Color (255f, 255f, 255f, fadePercent);
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
