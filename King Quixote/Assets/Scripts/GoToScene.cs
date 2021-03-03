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
            SceneManager.LoadScene(TargetSceneIndex);
    }
}
