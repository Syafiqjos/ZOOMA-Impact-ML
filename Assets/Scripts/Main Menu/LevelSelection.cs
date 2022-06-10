using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void GoToMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void GoToChapter1()
    {
        SceneManager.LoadScene("StoryCutscene1");
    }

    public void GoToChapter2()
    {
        SceneManager.LoadScene("StoryCutscene2");
    }

    public void GoToChapter3()
    {
        SceneManager.LoadScene("StoryCutscene3");
    }
}
