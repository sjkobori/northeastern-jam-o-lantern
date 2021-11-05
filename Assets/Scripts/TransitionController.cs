 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToGame()
    {
        SceneManager.LoadScene("Tutorial test", LoadSceneMode.Single);
    }


    public void ToCredits()
    {
        SceneManager.LoadScene("CreditsScene", LoadSceneMode.Single);
    }

    public void ToTitleScreen()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}