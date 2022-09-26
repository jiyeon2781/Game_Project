using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startCamera;
    public GameObject startPanel;
    public GameObject gamePanel;
    public MainPlayer player;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void gameStart()
    {
        player.isStart = true;
        startCamera.SetActive(false);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void gameExit()
    {
        Application.Quit();
    }
}
