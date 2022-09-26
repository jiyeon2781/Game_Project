using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonEvent()
    {
        SceneManager.LoadScene("Scenes/OneStageScene");
    }

    public void ExitButtonEvent()
    {
        Application.Quit();
    }
}
