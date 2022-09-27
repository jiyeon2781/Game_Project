using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public BoxCollider swardArea;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        StartCoroutine(Swing()); // �ڷ�ƾ �����Լ�
    }

    IEnumerator Swing() // ������ �Լ� Ŭ����
    {
        yield return new WaitForSeconds(0.1f);
        swardArea.enabled = true;
        yield return new WaitForSeconds(1f);
        swardArea.enabled = false;
    }
}
