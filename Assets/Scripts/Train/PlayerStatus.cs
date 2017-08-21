using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
	public enum PlayerAge
	{
		Kinder, Elementry, Middle, High, Graduate
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
        ConscienceBar.color = new Color(255, 255, 255, (float)(PlayerController.Conscience) / 50);//((float)PlayerController.Conscience / 100);
	}
}