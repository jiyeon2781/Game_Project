using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startCamera;
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject overPanel;
    public GameObject clearPanel;
    public MainPlayer player;

    public bool isStart;
    public int enemyCount;
    public Transform enemyGroup;
    public Slider playerSlider;

    public float playTime;
    public Text playTimeTxt;
    public Text totalTxt;
    public Text clearTotalTxt;

    private int hour, min, second;


    void Start()
    {
        
    }

    void Update()
    {
        if (isStart) playTime += Time.deltaTime;
        enemyCount = enemyGroup.childCount;
        if (enemyCount <= 0) gameClear();
    }

    private void LateUpdate()
    {
        playerSlider.value = player.playerHP;
        if (isStart)
        {
            hour = (int)(playTime / 3600); // �ð�
            min = (int)((playTime - hour * 3600) / 60); // ��
            second = (int)playTime % 60; // ��
            playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
        }
    }


    public void reStart()
    {
        overPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void gameStart()
    {
        player.isStart = true;
        isStart = true;
        startCamera.SetActive(false);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);

        enemyGroup.gameObject.SetActive(true);
    }

    public void gameOver()
    {
        player.isStart = false;
        isStart = false;
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        
        totalTxt.text = "�Ϳ�! " + (hour > 0 ? string.Format("{0}", hour) + "�ð� " : "") + (min > 0 ? string.Format("{0}", min) + "�� " : "") + string.Format("{0}", second) + "�ʳ� ������!";

    }

    public void gameClear()
    {
        isStart = false;
        gamePanel.SetActive(false);
        clearPanel.SetActive(true);
        clearTotalTxt.text = "����ؿ�! " + (hour > 0 ? string.Format("{0}", hour) + "�ð� " : "") + (min > 0 ? string.Format("{0}", min) + "�� " : "") + string.Format("{0}", second) + "�ʸ��� �����!";

    }

    public void gameExit()
    {
        Application.Quit();
    }
}
