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

    public PlayerStatus.PlayerAge playerAge;
    public int AmountOfNR;

    private Dictionary<string, GameObject> NRdic;
    private Dictionary<string, GameObject> BRdic;
    private Dictionary<string, GameObject> GRdic;

    private List<GameObject> Rabbits;
    private GameObject NowTrain;

    private void Start()
    {
        Rabbits = new List<GameObject>();
        NowTrain = GameObject.Find("Train");

        ArrayToDictionary();
        CreateRabbits();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteAllRabbits();//지하철 내의 모든 토끼들 제거
        }
    }

    private void CreateRabbits()
    {
        CreateGoodRabbits(playerAge, Train.trainNumber);
        CreateNormalRabbits();
    }

    private void CreateGoodRabbits(PlayerStatus.PlayerAge playerAge, int trainNum)
    {
        switch(playerAge)
        {
            case PlayerStatus.PlayerAge.Kinder:
                Rabbits.Add(Instantiate(GRdic["OldGentleman"]));
                Rabbits.Add(Instantiate(GRdic["Postgraduate"]));
                Rabbits.Add(Instantiate(GRdic["College"]));
                break;
            case PlayerStatus.PlayerAge.Elementry:
                break;
            case PlayerStatus.PlayerAge.Middle:
                break;
            case PlayerStatus.PlayerAge.High:
                break;
            case PlayerStatus.PlayerAge.Graduate:
                break;
        }
    }

    private void CreateNormalRabbits()
    {
        for(int amount = 0; amount < AmountOfNR; amount++)
        {
            int randomNum = Random.Range(0, 7);
            Vector2 spawnPos = new Vector2(Random.Range(-12.5f, 12.5f), Random.Range(-5f, 3f));

            Rabbits.Add(Instantiate(NRD[randomNum].RabbitPrefab, spawnPos, Quaternion.identity));
        }
    }

    private void DeleteAllRabbits() //모든 토끼 제거
    {
        foreach(GameObject rabbit in Rabbits)
        {
            Destroy(rabbit);
        }
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