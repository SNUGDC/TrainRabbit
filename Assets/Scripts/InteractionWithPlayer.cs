using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithPlayer : MonoBehaviour
{
	public bool isMeetPlayer;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			isMeetPlayer = true;
			Debug.Log("만남!");
		}
	}

	private void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			isMeetPlayer = false;
			Debug.Log("헤어짐!");
			gameObject.transform.parent.GetComponent<BasicRabbitController>().isTalking = false;
		}
	}
}