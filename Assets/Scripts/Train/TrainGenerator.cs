﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class TrainGenerator : MonoBehaviour
{
    //하는 일 : 주인공 토끼가 누구고 몇번째 칸인지에 따라 열차 안의 토끼들을 생성
    //지하철을 청소하는 함수와 생성하는 함수가 필요

    public RabbitDictionary[] NRD; //Normal Rabbit Dictionary
    public RabbitDictionary[] BRD; //Bad Rabbit Dictionary
    public RabbitDictionary[] GRD; //Good Rabbit Dictionary
    public RabbitDictionary[] SRD; //Sitting Rabbit Dictionary
    public ItemInfo[] ItemDictionary;

    public PlayerStatus.PlayerAge playerAge;
    public int trainNum;
    public int AmountOfNR;

    private Dictionary<string, GameObject> NRdic;
    private Dictionary<string, GameObject> BRdic;
    private Dictionary<string, GameObject> GRdic;

    private List<GameObject> Rabbits;
    private GameObject NowTrain;
    private float[] chairPosX = new float[12] {-12.8f, -11.3f, -9.8f, -4.4f, -2.8f, -1.2f, 0.4f, 2.0f, 3.6f, 9.1f, 10.7f, 12.2f};
    public float GongikInstanceCooltime;
    private bool wasConscienceHigherThan30;

    private void Start()
    {
        Rabbits = new List<GameObject>();
        NowTrain = GameObject.Find("Train");
        trainNum = Train.trainNumber.Value;

        EndEnding();
        ArrayToDictionary();
        CreateRabbits();

        if(PlayerData.Conscience < 30)
        {
            wasConscienceHigherThan30 = false;
        }
        else
        {
            wasConscienceHigherThan30 = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteAllRabbits();//지하철 내의 모든 토끼들 제거
        }

        if (playerAge != PlayerStatus.PlayerAge.Kinder 
            && playerAge != PlayerStatus.PlayerAge.Happy 
            && playerAge != PlayerStatus.PlayerAge.Sad)
        {
            GongikInstanceCooltime += Time.deltaTime;
            if(PlayerData.Conscience < 30)
            {
                if(GongikInstanceCooltime > 6f && !FindObjectsOfType<BadRabbitController>().Any(a => a.badRabbit == BadRabbit.Gongik))
                {
                    GongikInstanceCooltime = 0f;
                    GameObject newGongik = Instantiate(BRdic["Gongik"]);
                    newGongik.GetComponent<BadRabbitController>().tr = this;
                    Rabbits.Add(newGongik);
                }

                if(wasConscienceHigherThan30)
                {
                    wasConscienceHigherThan30 = false;
                    SwitchMusic(MusicType.badMain);
                }
            }
            else
            {
                if(!wasConscienceHigherThan30)
                {
                    wasConscienceHigherThan30 = true;
                    SwitchMusic(SoundManager.ChooseGoodMusic());
                }
            }
        }
    }
    private void EndEnding(){
        if(playerAge == PlayerStatus.PlayerAge.Happy && trainNum <= 14){
            GetComponent<SceneMover>().MoveScene();
        }
        if(playerAge == PlayerStatus.PlayerAge.Sad && trainNum <= 14){
            GetComponent<SceneMover>().MoveScene();
        }
    }

    private void SwitchMusic(MusicType mt)
    {
        if(DialogueController.isTalking)
        {
            SoundManager.SetSubPlayerMusic(mt);
        }  
        else
        {
            SoundManager.PlayOtherMusic(mt);
        }
    }

    private void CreateRabbits()
    {
        CreateGoodRabbits(playerAge, trainNum);
        CreateBadRabbits(playerAge, trainNum);
        CreateNormalRabbits();
        CreateSittingRabbits();
        CreateItem();
    }

    private void CreateGoodRabbits(PlayerStatus.PlayerAge playerAge, int trainNum)
    {
        switch(playerAge)
        {
            case PlayerStatus.PlayerAge.Kinder:
                if(trainNum == 20)
                    Rabbits.Add(Instantiate(GRdic["Postgraduate"]));
                else if (trainNum == 19)
                    Rabbits.Add(Instantiate(GRdic["Bernard"]));
                else if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                else if (trainNum == 17)
                    Rabbits.Add(Instantiate(GRdic["OldGentleman"]));
                else if (trainNum == 16)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                else if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Fact"]));
                else if (trainNum == 14)
                    Rabbits.Add(Instantiate(GRdic["College"]));
                else if (trainNum == 13)
                    Rabbits.Add(Instantiate(GRdic["Rachel"]));
                else if (trainNum == 12)
                    Rabbits.Add(Instantiate(GRdic["Gag"]));
                else if (trainNum == 10)
                    Rabbits.Add(Instantiate(GRdic["Mama"]));
                break;
            case PlayerStatus.PlayerAge.Elementary:
                if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Bernard"]));
                if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                if (trainNum == 12)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                if (trainNum == 9)
                    Rabbits.Add(Instantiate(GRdic["Rachel"]));
                if (trainNum == 7)
                    Rabbits.Add(Instantiate(GRdic["Foreigner"]));
                if (trainNum == 6)
                    Rabbits.Add(Instantiate(GRdic["Fact"]));
                if (trainNum == 3)
                    Rabbits.Add(Instantiate(GRdic["Gag"]));
                if (trainNum == 1)
                    Rabbits.Add(Instantiate(GRdic["Last"]));
                break;
            case PlayerStatus.PlayerAge.Middle:
                if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Bernard"]));
                if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                if (trainNum == 12)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                if (trainNum == 9)
                    Rabbits.Add(Instantiate(GRdic["Rachel"]));
                if (trainNum == 6)
                    Rabbits.Add(Instantiate(GRdic["Fact"]));
                if (trainNum == 3)
                    Rabbits.Add(Instantiate(GRdic["Gag"]));
                if (trainNum == 1)
                    Rabbits.Add(Instantiate(GRdic["Last"]));
                break;
            case PlayerStatus.PlayerAge.High:
                if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Bernard"]));
                if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                if (trainNum == 12)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                if (trainNum == 9)
                    Rabbits.Add(Instantiate(GRdic["Rachel"]));
                if (trainNum == 6)
                    Rabbits.Add(Instantiate(GRdic["Fact"]));
                if (trainNum == 4)
                    Rabbits.Add(Instantiate(GRdic["Pregnant"]));
                if (trainNum == 3)
                    Rabbits.Add(Instantiate(GRdic["Gag"]));
                if (trainNum == 1)
                    Rabbits.Add(Instantiate(GRdic["Last"]));
                break;
            case PlayerStatus.PlayerAge.Soonung:
                if (trainNum == 1)
                    Rabbits.Add(Instantiate(GRdic["Last"]));
                break;
            case PlayerStatus.PlayerAge.Graduate:
                if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Bernard"]));
                if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                if (trainNum == 12)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                if (trainNum == 9)
                    Rabbits.Add(Instantiate(GRdic["Rachel"]));
                if (trainNum == 6)
                    Rabbits.Add(Instantiate(GRdic["Fact"]));
                if (trainNum == 3)
                    Rabbits.Add(Instantiate(GRdic["Gag"]));
                if (trainNum == 1)
                    Rabbits.Add(Instantiate(GRdic["Last"]));
                break;
            case PlayerStatus.PlayerAge.Happy:
                if (trainNum == 20)
                    Rabbits.Add(Instantiate(GRdic["Postgraduate"]));
                if (trainNum == 19)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Pregnant"]));
                if (trainNum == 17)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                if (trainNum == 16)
                    Rabbits.Add(Instantiate(GRdic["Rachel"]));
                if (trainNum == 16)
                    Rabbits.Add(Instantiate(GRdic["Bernard"]));
                if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Gongik"]));
                break;
            case PlayerStatus.PlayerAge.Sad:
                if (trainNum == 20)
                    Rabbits.Add(Instantiate(GRdic["Postgraduate"]));
                if (trainNum == 19)
                    Rabbits.Add(Instantiate(GRdic["Scared"]));
                if (trainNum == 18)
                    Rabbits.Add(Instantiate(GRdic["Choding"]));
                if (trainNum == 17)
                    Rabbits.Add(Instantiate(GRdic["Getout"]));
                if (trainNum == 16)
                    Rabbits.Add(Instantiate(GRdic["Pregnant"]));
                if (trainNum == 15)
                    Rabbits.Add(Instantiate(GRdic["Hentai"]));
                break;
        }
    }

    private void CreateBadRabbits(PlayerStatus.PlayerAge playerAge, int trainNum)
    {
        switch (playerAge)
        {
            case PlayerStatus.PlayerAge.Kinder:
                break;

            case PlayerStatus.PlayerAge.Elementary:
                if (trainNum == 20)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 12)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 7)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                break;

            case PlayerStatus.PlayerAge.Middle:
                if (trainNum == 20)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 18)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 15)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 12)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 9)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 6)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 3)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                break;

            case PlayerStatus.PlayerAge.High:
                if (trainNum == 20)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                else if (trainNum == 19)
                {
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                }
                else if (trainNum == 16)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 14)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 11)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 10)
                {
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                }
                else if (trainNum == 7)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                else if (trainNum == 6)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 4)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 2)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                else if (trainNum == 1)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                break;

            case PlayerStatus.PlayerAge.Graduate:
                if (trainNum == 20)
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                else if (trainNum == 19)
                {
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                }
                else if (trainNum == 18)
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                else if (trainNum == 17)
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                else if (trainNum == 15)
                {
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                }
                else if (trainNum == 14)
                    Rabbits.Add(Instantiate(BRdic["Jeondomon"]));
                else if (trainNum == 12)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                else if (trainNum == 11)
                {
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                }
                else if (trainNum == 9)
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                else if (trainNum == 8)
                {
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                }
                else if (trainNum == 7)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                else if (trainNum == 5)
                {
                    Rabbits.Add(Instantiate(BRdic["Merchant"]));
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                }
                else if (trainNum == 3)
                    Rabbits.Add(Instantiate(BRdic["Hentai"]));
                else if (trainNum == 2)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));
                else if (trainNum == 1)
                    Rabbits.Add(Instantiate(BRdic["Drunken"]));

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

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 17 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 19 && amount == 5)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 18 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 16 && amount == 7)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 15 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 13 && amount == 1)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 12 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if(playerAge == PlayerStatus.PlayerAge.Kinder && trainNum == 12 && amount == 10)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 18 && amount == 5)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 15 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 12 && amount == 7)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 9 && amount == 1)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 6 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 3 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Elementary && trainNum == 3 && amount == 10)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 18 && amount == 5)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 15 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 12 && amount == 7)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 9 && amount == 1)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 6 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 3 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Middle && trainNum == 3 && amount == 10)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 18 && amount == 5)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 15 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 12 && amount == 7)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 9 && amount == 1)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 6 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 4 && amount == 3)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 3 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.High && trainNum == 3 && amount == 10)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 18 && amount == 5)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 15 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 12 && amount == 7)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 9 && amount == 1)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 6 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 3 && amount == 9)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }

            if (playerAge == PlayerStatus.PlayerAge.Graduate && trainNum == 3 && amount == 10)
            {
                Destroy(Rabbits[Rabbits.Count - 1]);
            }
        }
    }

    private void CreateItem()
    {
        if (ItemDictionary.Length != 0)
        {
            int randomNum = Random.Range(0, ItemDictionary.Length);
            Vector2 spawnPos = new Vector2(Random.Range(-12.5f, 12.5f), Random.Range(-5f, 1f));

              Instantiate(ItemDictionary[randomNum].ItemPrefab, spawnPos, Quaternion.identity);
        }
        else return;
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