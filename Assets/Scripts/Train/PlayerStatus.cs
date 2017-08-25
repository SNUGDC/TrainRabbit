using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
	public enum PlayerAge
	{
		Kinder, Elementary, Middle, High, Soonung, Graduate, Happy, Sad
	}
	
	public Slider HPBar;
	public Image ConscienceBar;

	private void Start()
	{
		GameObject Canvas = GameObject.Find("Canvas");
		//HPBar = Canvas.transform.Find("HP bar").gameObject;
		//ConscienceBar = Canvas.transform.Find("Conscience Bar").gameObject;
	}

	private void Update()
	{
		HPBar.value = ((float)PlayerController.HP / 100);

		float ConscienceColor = ((float)PlayerData.Conscience / 100);
        ConscienceBar.color = new Color(ConscienceColor, ConscienceColor, ConscienceColor);
	}
}