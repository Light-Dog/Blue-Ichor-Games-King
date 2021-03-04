using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    public int TargetSceneIndex = 1;
    public float touchTime = 3.0f;

    bool touch = false;
    bool once = true;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
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

    private void Update()
    {
        if(touch && once)
        {
            FindObjectOfType<GoToScene>().GoToSceneCall(TargetSceneIndex);
            once = false;
        }
    }
}
