using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithPostgraduate : MonoBehaviour
{
	private TalkManager talkManager;

	private void Start()
	{
		talkManager = GameObject.Find("Talk Manager").GetComponent<TalkManager>();
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Talk Box")
		{
			if(coll.gameObject.transform.parent.gameObject.tag == "Postgraduate Rabbit")
			{
				talkManager.isMeetPostgraduateRabbit = true;
				Debug.Log("만남!");
			}
		}
	}

	private void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Talk Box")
		{
			talkManager.isMeetPostgraduateRabbit = false;
			Debug.Log("헤어짐!");
		}
	}
}