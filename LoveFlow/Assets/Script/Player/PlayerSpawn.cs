using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 시작시 플레이어 랜덤 4 군데에서 겹치지 않게 스폰되는 스크립트
/// </summary>

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Vector3[] spawnPositions;

    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        // 위치 목록에서 랜덤한 인덱스를 얻는다.
        int index1 = Random.Range(0, spawnPositions.Length);

        // Player1을 랜덤한 위치에 스폰한다.
        //Instantiate(player1Prefab, spawnPositions[index1], Quaternion.Euler(-90,0,-90));
        player1Prefab.transform.position = spawnPositions[index1];

        // Player1이 스폰된 위치를 다른 변수에 저장한다.
        Vector3 player1Position = spawnPositions[index1];

        // 다른 위치를 찾을 때까지 랜덤 인덱스를 생성한다.
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPositions.Length);
        }
        while (spawnPositions[index2] == player1Position);

        // Player2를 다른 랜덤한 위치에 스폰한다.
        //Instantiate(player2Prefab, spawnPositions[index2], Quaternion.Euler(-90, 0, -90));
        player2Prefab.transform.position = spawnPositions[index2];
    }
}
