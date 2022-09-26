using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Type { Sward, Range };
    public Type type;
    public int damage;
    public float rate;
    public MeshCollider swardArea;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        if (type == Type.Sward)
        {
            StopCoroutine("Swing"); // �ڷ�ƾ �����Լ�
            StartCoroutine("Swing"); // �ڷ�ƾ �����Լ�
        }
    }
    // Use() ���η�ƾ -> Swing() �����ƾ -> Use() ���η�ƾ
    // Use() ���η�ƾ + Swing() �ڷ�ƾ (Co-Op)

    IEnumerator Swing() // ������ �Լ� Ŭ����
    {
        yield return new WaitForSeconds(0.1f); // ����� �����ϴ� Ű���� : yield
        // yield Ű���带 ������ ����� �ð��� ���� �ۼ� ����
        // WaitForSeconds() : �־��� ��ġ��ŭ ��ٸ��� �Լ�
        swardArea.enabled = true;
        yield return new WaitForSeconds(0.3f);
        swardArea.enabled = false;
    }
}
