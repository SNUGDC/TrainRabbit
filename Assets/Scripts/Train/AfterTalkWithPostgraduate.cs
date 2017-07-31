using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterTalkWithPostgraduate : MonoBehaviour
{
	public bool isTalked = false;

	private GameObject MapButton;
	private GameObject AttackPad;

	private void Start()
	{
		MapButton = GameObject.Find("Map Button");
		AttackPad = GameObject.Find("Attack Pad");
		MapButton.SetActive(false);
		AttackPad.SetActive(false);
	}

	private void Update()
	{
		if(isTalked == true)
		{
			MapButton.SetActive(true);
			AttackPad.SetActive(true);
			Destroy(GameObject.FindGameObjectWithTag("Postgraduate Rabbit"));
		}
	}

	public void TalkingFinished()
	{
		isTalked = true;
	}
}