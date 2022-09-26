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
            StopCoroutine("Swing"); // 코루틴 정지함수
            StartCoroutine("Swing"); // 코루틴 실행함수
        }
    }
    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-Op)

    IEnumerator Swing() // 열거형 함수 클래스
    {
        yield return new WaitForSeconds(0.1f); // 결과를 전달하는 키워드 : yield
        // yield 키워드를 여러개 사용해 시간차 로직 작성 가능
        // WaitForSeconds() : 주어진 수치만큼 기다리는 함수
        swardArea.enabled = true;
        yield return new WaitForSeconds(0.3f);
        swardArea.enabled = false;
    }
}
