using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterTimeLoadScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Menu";
    [SerializeField] private float time = 0;

    private float timeProcess = 0;
    private bool triggered = false;

    private void Update()
    {
        timeProcess += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) ||
            
            (triggered == false && timeProcess > time)) {
            triggered = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
