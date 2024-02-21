using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject[] pipes;  // ������ �װ�
    public Vector3[] positions; // ������ ��ġ �� ����

    void Start()
    {
        // ��ġ �迭 �������� ����
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 temp = positions[i];
            int randomIndex = Random.Range(i, positions.Length);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // ���� ��ġ�� ������ ���� 
        for (int i = 0; i < positions.Length; i++)
        {
            pipes[i].SetActive(true);
            Instantiate(pipes[i], positions[i], Quaternion.identity);
            // Quaternion.identity�� �ƹ��� ȸ���� ���� �ʴ� ���ʹϾ�
            // ��ü�� �������� ������ ����(��, ȸ�� ����)�� �����ϰ� �ϱ� ���� ��
            // Instantiate �Լ����� Quaternion.identity�� ����ϸ� �������� ������ ������ ������ ä�� ����
        }
    }
}
