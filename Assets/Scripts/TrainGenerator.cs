using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGenerator : MonoBehaviour
{
    //하는 일 : 주인공 토끼가 누구고 몇번째 칸인지에 따라 열차 안의 토끼들을 생성
    //지하철을 청소하는 함수와 생성하는 함수가 필요

    public GameObject[] NormalRabbitPrefab;
    public GameObject[] BadRabbitPrefab;
    public GameObject[] GoodRabbitPrefab;

    private Dictionary<string, GameObject> NRdic;
    private Dictionary<string, GameObject> BRdic;
    private Dictionary<string, GameObject> GRdic;

    private void Start()
    {
        BRdic.Add("잡상인", BadRabbitPrefab[0]); //암튼 이런식으로 해야함
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateRabbits();//지하철 내의 토끼들 생성
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteAllRabbits();//지하철 내의 토끼들 제거
        }
    }

    private void CreateRabbits(/*주인동 토끼의 레벨, 열차 칸 번호를 받아와야 함*/)
    {

    }

    private void DeleteAllRabbits() //모든 토끼 제거
    {
    }
}