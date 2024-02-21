using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ü�� �ٴڿ� �浹 ������ �ٷ� ���ӿ��� ��ư �ߴ� ��ũ��Ʈ
/// </summary>
/// 


// �� ��ũ��Ʈ�� ��ü�� �־��ֱ�(?)
// �׷��� �� ��ũ��Ʈ ���� �ְ� �ٴڿ� ���� �ε����� �� ���ӿ��� ��ư ��

public class j_liquidCollision : MonoBehaviour
{
    public Button gameoverButton;   // ���ӿ��� ��ư �ߴ°�
    public GameObject floor;        // ��ü�� �浹�� �ٴ�

    public int life = 3;  // ���


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == floor)  // �ٴڿ� ��ü�� ������
        {
            if(life>1)
            {
                life--;
            }

            else // �ٴڿ� ����° ��� ���
            {
                gameoverButton.gameObject.SetActive(true); // ���ӿ��� ��ư �߱�
            }
        }
    }
}
