using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStarter : MonoBehaviour
{
	private TalkManager talkManager;

	private void Start()
	{
		talkManager = GameObject.Find("Talk Manager").GetComponent<TalkManager>();
	}

	public void OnMouseDown()
	{
		if(transform.parent.gameObject.name.Contains("Postgraduate"))
		{
			talkManager.isClickPostgraduateRabbit = true;
		}
	}
}