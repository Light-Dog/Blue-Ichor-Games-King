using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{

    public int TargetSceneIndex = 1;
    public Scene TargetScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SwitchScene()
    {

        SceneManager.LoadScene(TargetSceneIndex);
        //SceneManager.LoadScene(TargetScene.buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
