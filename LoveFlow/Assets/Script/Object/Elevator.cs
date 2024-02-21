using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 1f; // ���������� �̵� �ӵ�
    public float pauseTime = 2f; // ���ߴ� �ð�
    public float lowPoint = 0f; // ���� ���� (y ��)
    public float highPoint = 2f; // ���� ���� (y ��)

    private bool goingUp = true; // ���� ���� ���� �ִ��� �Ʒ��� ���� �ִ��� ǥ��
    private float pauseTimer = 0f; // ���� �ð� Ÿ�̸�

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

        if (goingUp)    // �ö󰡴� ���̸�
        {
            if (transform.position.y < highPoint)
            {
                // ���� �̵�
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else
            {
                // ��ǥ���� �����ϸ�, pauseTimer�� ������Ų��.
                pauseTimer += Time.deltaTime;

                if (pauseTimer >= pauseTime)
                {
                    // ��� ���� ��, �Ʒ��� ���� ����
                    goingUp = false;
                    pauseTimer = 0f;
                }
            }
        }
        else   // �������� ���̸� 
        {
            if (transform.position.y > lowPoint)
            {
                // �Ʒ��� �̵�
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else
            {
                // �Ʒ��� �����ϸ�, pauseTimer�� ������Ų��.
                pauseTimer += Time.deltaTime;

                if (pauseTimer >= pauseTime)
                {
                    // ��� ���� ��, ���� ���� ����
                    goingUp = true;
                    pauseTimer = 0f;
                }
            }
        }
    }
}