using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGenerator : MonoBehaviour
{
    //하는 일 : 주인공 토끼가 누구고 몇번째 칸인지에 따라 열차 안의 토끼들을 생성
    //지하철을 청소하는 함수와 생성하는 함수가 필요

    public RabbitDictionary[] NRD; //Normal Rabbit Dictionary
    public RabbitDictionary[] BRD; //Bad Rabbit Dictionary
    public RabbitDictionary[] GRD; //Good Rabbit Dictionary
    public RabbitDictionary[] SRD; //Sitting Rabbit Dictionary

    public PlayerStatus.PlayerAge playerAge;
    public int trainNum;
    public int AmountOfNR;

    private Dictionary<string, GameObject> NRdic;
    private Dictionary<string, GameObject> BRdic;
    private Dictionary<string, GameObject> GRdic;

    private List<GameObject> Rabbits;
    private GameObject NowTrain;
    private float[] chairPosX = new float[12] {-12.8f, -11.3f, -9.8f, -4.4f, -2.8f, -1.2f, 0.4f, 2.0f, 3.6f, 9.1f, 10.7f, 12.2f};

    private void Start()
    {
        Rabbits = new List<GameObject>();
        NowTrain = GameObject.Find("Train");
        trainNum = Train.trainNumber.Value;

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
        CreateGoodRabbits(playerAge, trainNum);
        CreateNormalRabbits();
        CreateSittingRabbits();
    }

    private void CreateGoodRabbits(PlayerStatus.PlayerAge playerAge, int trainNum)
    {
        switch(playerAge)
        {
            case PlayerStatus.PlayerAge.Kinder:
                if(trainNum == 20)
                    Rabbits.Add(Instantiate(GRdic["Postgraduate"]));
                else if (trainNum == 17)
                    Rabbits.Add(Instantiate(GRdic["OldGentleman"]));
                else if (trainNum == 14)
                    Rabbits.Add(Instantiate(GRdic["College"]));
                else if (trainNum == 10)
                    Rabbits.Add(Instantiate(GRdic["Mama"]));                
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

    private void CreateSittingRabbits()
    {
        for(int amount = 0; amount < chairPosX.Length; amount++)
        {
            int randomNum = Random.Range(0, SRD.Length);
            Vector2 spawnPos = new Vector2(chairPosX[amount], 4.8f);

            Rabbits.Add(Instantiate(SRD[randomNum].RabbitPrefab, spawnPos, Quaternion.identity));

            if(playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 17 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }
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