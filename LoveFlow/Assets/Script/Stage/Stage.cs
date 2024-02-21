using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맵의 큐브 데이터 
// 1층 배열 12*12
// 2층 배열 8*8

// 모드 1. 걷기 모드 2. 파이프 설치 / 수리/ 부수기 등 모드


public class Stage : MonoBehaviour
{

    // 라이프, 호감도
    public int _life;
    public int _feelingBar;

    // 파이프 맵과 정보
    public PipeCube[] _firstPipeFloor = new PipeCube[64];
    public PipeCube[] _secondPipeFloor = new PipeCube[64];
    public PipeCube[] _thirdPipeFloor = new PipeCube[144];

    void Start()
    {
        _life = 3;
        _feelingBar = 0;
    }

    // 파이프를 선택해서 파이프 큐브 클래스로 넘어가 설치하고 데이터 저장
    // 다시 파이프를 해제해서 파이프 선택 모드로 돌아간다.

    void Update()
    {
        
    }

    void LifeDecrease()
    {
        // 액체가 바닥에 닿았을 경우
        // 라이프 감소 처리
        // 라이프 몇만 남으면 게임 오버
    }

    void feelingChange()
    {
        // 문맥 상 엉뚱한 답변을 할 경우 호감도 차감


        // 그 반대에는 호감도 상승
    }

    void Gameover()
    {
        // 게임오버씬?
    }

    void GameClear()
    {
        // 다음 스테이지
    }
}
