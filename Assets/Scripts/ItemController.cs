using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	public string ItemName;
	public bool isForQuest;

	private GameObject GetItemPanel;

	private void Start()
	{
		GetItemPanel = GameObject.Find("Canvas").transform.Find("Item Get Panel").gameObject;
	}

	private void Update()
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player" && isForQuest == false)
		{
			UpdateItemCollect();
			Debug.Log(ItemName + "을" + PlayerPrefs.GetInt(ItemName) + "째 획득!");
			SoundManager.PlayGetItem();
			GetItemPanel.SetActive(true);
			Destroy(gameObject);
		}
		else if(coll.gameObject.tag == "Player" && isForQuest == true)
		{
			coll.gameObject.GetComponent<PlayerController>().isQuestComplete = true;
			Destroy(gameObject);
		}
	}

	private void UpdateItemCollect()
	{
		if(PlayerPrefs.HasKey(ItemName))
		{
			PlayerPrefs.SetInt(ItemName, PlayerPrefs.GetInt(ItemName) + 1);
			return;
		}
		else
		{
			PlayerPrefs.SetInt(ItemName, 1);
		}
	}
}