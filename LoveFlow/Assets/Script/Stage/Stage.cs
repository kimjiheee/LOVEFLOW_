using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ť�� ������ 
// 1�� �迭 12*12
// 2�� �迭 8*8

// ��� 1. �ȱ� ��� 2. ������ ��ġ / ����/ �μ��� �� ���


public class Stage : MonoBehaviour
{

    // ������, ȣ����
    public int _life;
    public int _feelingBar;

    // ������ �ʰ� ����
    public PipeCube[] _firstPipeFloor = new PipeCube[64];
    public PipeCube[] _secondPipeFloor = new PipeCube[64];
    public PipeCube[] _thirdPipeFloor = new PipeCube[144];

    void Start()
    {
        _life = 3;
        _feelingBar = 0;
    }

    // �������� �����ؼ� ������ ť�� Ŭ������ �Ѿ ��ġ�ϰ� ������ ����
    // �ٽ� �������� �����ؼ� ������ ���� ���� ���ư���.

    void Update()
    {
        
    }

    void LifeDecrease()
    {
        // ��ü�� �ٴڿ� ����� ���
        // ������ ���� ó��
        // ������ � ������ ���� ����
    }

    void feelingChange()
    {
        // ���� �� ������ �亯�� �� ��� ȣ���� ����


        // �� �ݴ뿡�� ȣ���� ���
    }

    void Gameover()
    {
        // ���ӿ�����?
    }

    void GameClear()
    {
        // ���� ��������
    }
}
