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
		if(coll.gameObject.tag == "Player")
		{
			Debug.Log(ItemName + "을 획득!");
			GetItemPanel.SetActive(true);
			coll.gameObject.GetComponent<PlayerController>().isQuestComplete = true;
			Destroy(gameObject);
		}
	}
}