using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{

    public int TargetSceneIndex = 1;
    public float touchTime = 1.0f;

    bool touch = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()!= null)
        {
            if (touchTime > 0)
            {
                touchTime -= Time.deltaTime;
            }
            else
            {
                touch = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(touch)
        {
            switch (TargetSceneIndex)
            {
                case 1:
                    FindObjectOfType<AudioManager>().SwitchSongs("Level1");
                    break;
                case 3:
                    FindObjectOfType<AudioManager>().SwitchSongs("Level3");
                    break;
                case 5:
                    FindObjectOfType<AudioManager>().SwitchSongs("Death");
                    break;
                default:
                    FindObjectOfType<AudioManager>().SwitchSongs("Menu");
                    break;
            }

            SceneManager.LoadScene(TargetSceneIndex);
        }
    }

    public void GoToSceneCall(int scene)
    {
        switch (scene)
        {
            case 1:
                FindObjectOfType<AudioManager>().SwitchSongs("Level1");
                break;
            case 3:
                FindObjectOfType<AudioManager>().SwitchSongs("Level3");
                break;
            case 5:
                FindObjectOfType<AudioManager>().SwitchSongs("Death");
                break;
        }

        SceneManager.LoadScene(scene);
    }

    public void GoToMenu()
    {
        FindObjectOfType<AudioManager>().SwitchSongs("Menu");
        SceneManager.LoadScene(0);
    }
}
