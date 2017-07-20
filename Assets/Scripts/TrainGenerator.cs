using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGenerator : MonoBehaviour
{
    //하는 일 : 주인공 토끼가 누구고 몇번째 칸인지에 따라 열차 안의 토끼들을 생성
    //지하철을 청소하는 함수와 생성하는 함수가 필요

    public RabbitDictionary[] NRD;
    public RabbitDictionary[] BRD;
    public RabbitDictionary[] GRD;

    private Dictionary<string, GameObject> NRdic;
    private Dictionary<string, GameObject> BRdic;
    private Dictionary<string, GameObject> GRdic;

    private void Start()
    {
        ArrayToDictionary();
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
        Instantiate(GRdic["OldGentleman"]);
        Instantiate(GRdic["Postgraduate"]);
        Instantiate(GRdic["College"]);
    }

    private void DeleteAllRabbits() //모든 토끼 제거
    {
    }

    private void ArrayToDictionary()
    {
        NRdic = new Dictionary<string, GameObject>();
        BRdic = new Dictionary<string, GameObject>();
        GRdic = new Dictionary<string, GameObject>();

        foreach (RabbitDictionary rabbitDic in NRD)
        {
            NRdic.Add(rabbitDic.RabbitName, rabbitDic.RabbitPrefab);
        }
        foreach (RabbitDictionary rabbitDic in BRD)
        {
            BRdic.Add(rabbitDic.RabbitName, rabbitDic.RabbitPrefab);
        }
        foreach (RabbitDictionary rabbitDic in GRD)
        {
            GRdic.Add(rabbitDic.RabbitName, rabbitDic.RabbitPrefab);
        }
    }
}