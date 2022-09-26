using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamaged : MonoBehaviour
{
    [SerializeField]
    private int playerHP = 100;
    [SerializeField]
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("아파!");
            playerHP -= 20;
            slider.value = playerHP;
            if (playerHP <= 0) Debug.Log("게임 오버~");
        }
    }
}
