using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushNumberButton : MonoBehaviour
{
	public Text Answer;
	private int remainStationAmount;

	public void NumberButton(int number)
	{
		remainStationAmount = number;
	}

	private void Update()
	{
		Answer.text = remainStationAmount.ToString();
	}
}