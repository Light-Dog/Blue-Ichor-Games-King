using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{
    public Animator transition;
    public UpdateButton menu;

    private void Update()
    {
        if(InputManager.GetKeyDown("Escape"))
        {
            menu.ShowMenu();
        }
    }

    public void GoToSceneCall(int scene)
    {
        StartCoroutine(LoadNextLevel(scene));
    }

    public void DeathToMenu()
    {
        StartCoroutine(LoadLevel_Death());
    }

    //for the menu button
    public void GoToMenu_Transition()
    {
        StartCoroutine(LoadMenu());

        if (FindObjectOfType<PlayerController>())
            FindObjectOfType<PlayerController>().dead = true;
    }

    private void GoToMenu()
    {
        FindObjectOfType<AudioManager>().SwitchSongs("Menu");
        SceneManager.LoadScene(0);
    }

    private void GoToDeath()
    {
        FindObjectOfType<AudioManager>().SwitchSongs("Death");
        SceneManager.LoadScene(5);
    }

    IEnumerator LoadMenu()
    {
        transition.SetTrigger("SpinEnd");

        yield return new WaitForSeconds(1.5f);

        GoToMenu();
    }

    IEnumerator LoadLevel_Death()
    {
        transition.SetTrigger("PlayerDied");

        yield return new WaitForSeconds(1.5f);

        GoToDeath();
    }

    IEnumerator LoadNextLevel(int scene)
    {
        transition.SetTrigger("SpinEnd");

        yield return new WaitForSeconds(1.5f);

        switch (scene)
        {
            case 1:
                FindObjectOfType<AudioManager>().SwitchSongs("Level1");
                break;
            case 3:
                FindObjectOfType<AudioManager>().SwitchSongs("Level3");
                break;
            case 4:
                FindObjectOfType<AudioManager>().SwitchSongs("Menu");
                break;
            case 5:
                FindObjectOfType<AudioManager>().SwitchSongs("Death");
                break;
        }

        SceneManager.LoadScene(scene);
    }
}
