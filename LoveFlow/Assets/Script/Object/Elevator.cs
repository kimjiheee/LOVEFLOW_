using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 1f; // 엘레베이터 이동 속도
    public float pauseTime = 2f; // 멈추는 시간
    public float lowPoint = 0f; // 낮은 지점 (y 값)
    public float highPoint = 2f; // 높은 지점 (y 값)

    private bool goingUp = true; // 현재 위로 가고 있는지 아래로 가고 있는지 표시
    private float pauseTimer = 0f; // 멈춤 시간 타이머

    [SerializeField]
    Collider boxCollider;

    void Update()
    {
        if(transform.position.y >= -2.6f)
        {
            boxCollider.gameObject.SetActive(true);
        }
        else
        {
            boxCollider.gameObject.SetActive(false);
        }

        if (goingUp)    // 올라가는 중이면
        {
            if (transform.position.y < highPoint)
            {
                // 위로 이동
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else
            {
                // 목표지점 도달하면, pauseTimer를 증가시킨다.
                pauseTimer += Time.deltaTime;

                if (pauseTimer >= pauseTime)
                {
                    // 잠시 멈춘 후, 아래로 가기 시작
                    goingUp = false;
                    pauseTimer = 0f;
                }
            }
        }
        else   // 내려가는 중이면 
        {
            if (transform.position.y > lowPoint)
            {
                // 아래로 이동
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else
            {
                // 아래에 도달하면, pauseTimer를 증가시킨다.
                pauseTimer += Time.deltaTime;

                if (pauseTimer >= pauseTime)
                {
                    // 잠시 멈춘 후, 위로 가기 시작
                    goingUp = true;
                    pauseTimer = 0f;
                }
            }
        }
    }
}