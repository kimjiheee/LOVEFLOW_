using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject[] pipes;  // 파이프 네개
    public Vector3[] positions; // 파이프 위치 네 군데

    void Start()
    {
        // 위치 배열 램덤으로 섞기
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 temp = positions[i];
            int randomIndex = Random.Range(i, positions.Length);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // 섞인 위치에 파이프 생성 
        for (int i = 0; i < positions.Length; i++)
        {
            pipes[i].SetActive(true);
            Instantiate(pipes[i], positions[i], Quaternion.identity);
            // Quaternion.identity는 아무런 회전도 하지 않는 쿼터니언
            // 객체가 원점에서 원래의 방향(즉, 회전 없음)을 유지하게 하기 위한 값
            // Instantiate 함수에서 Quaternion.identity를 사용하면 프리팹이 원래의 방향을 유지한 채로 생성
        }
    }
}
