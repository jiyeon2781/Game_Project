using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startCamera;
    public GameObject startPanel;
    public GameObject gamePanel;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void gameStart()
    {
        startCamera.SetActive(false);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void gameExit()
    {
        Application.Quit();
    }
}
