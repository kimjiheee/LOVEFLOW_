using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���۽� �÷��̾� ���� 4 �������� ��ġ�� �ʰ� �����Ǵ� ��ũ��Ʈ
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
        // ��ġ ��Ͽ��� ������ �ε����� ��´�.
        int index1 = Random.Range(0, spawnPositions.Length);

        // Player1�� ������ ��ġ�� �����Ѵ�.
        //Instantiate(player1Prefab, spawnPositions[index1], Quaternion.Euler(-90,0,-90));
        player1Prefab.transform.position = spawnPositions[index1];

        // Player1�� ������ ��ġ�� �ٸ� ������ �����Ѵ�.
        Vector3 player1Position = spawnPositions[index1];

        // �ٸ� ��ġ�� ã�� ������ ���� �ε����� �����Ѵ�.
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPositions.Length);
        }
        while (spawnPositions[index2] == player1Position);

        // Player2�� �ٸ� ������ ��ġ�� �����Ѵ�.
        //Instantiate(player2Prefab, spawnPositions[index2], Quaternion.Euler(-90, 0, -90));
        player2Prefab.transform.position = spawnPositions[index2];
    }
}
