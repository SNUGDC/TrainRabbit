using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	public bool isForQuest;

	private void Update()
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			Debug.Log("당근 쿠키와 플레이어의 만남!");
			coll.gameObject.GetComponent<PlayerController>().isQuestComplete = true;
			Destroy(gameObject);
		}
	}
}