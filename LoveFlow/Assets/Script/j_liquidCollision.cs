using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 유체가 바닥에 충돌 세번시 바로 게임오버 버튼 뜨는 스크립트
/// </summary>
/// 


// 이 스크립트는 유체에 넣어주기(?)
// 그래야 이 스크립트 가진 애가 바닥에 세번 부딪혔을 때 게임오버 버튼 뜸

public class j_liquidCollision : MonoBehaviour
{
    public Button gameoverButton;   // 게임오버 버튼 뜨는거
    public GameObject floor;        // 유체가 충돌할 바닥

    public int life = 3;  // 목숨


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == floor)  // 바닥에 유체가 닿으면
        {
            if(life>1)
            {
                life--;
            }

            else // 바닥에 세번째 닿는 경우
            {
                gameoverButton.gameObject.SetActive(true); // 게임오버 버튼 뜨기
            }
        }
    }
}
