using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{

    public int TargetSceneIndex = 1;
    public Scene TargetScene;
    public bool touch = false;
    public float touchTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SwitchScene()
    {

        SceneManager.LoadScene(TargetSceneIndex);
        //SceneManager.LoadScene(TargetScene.buildIndex);
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (touch == true)
        {
            if (touchTime > 0)
            {
                touchTime -= Time.deltaTime;
            }
            else
            {
                SwitchScene();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
