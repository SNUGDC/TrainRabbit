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
	
	public GameObject HPBar;
	public GameObject ConscienceBar;

	private int HP;
	private int Conscience;

	private void Start()
	{
		GameObject Canvas = GameObject.Find("Canvas");
		//HPBar = Canvas.transform.Find("HP bar").gameObject;
		//ConscienceBar = Canvas.transform.Find("Conscience Bar").gameObject;
	}

	private void Update()
	{
		HPBar.GetComponent<Scrollbar>().size = ((float)PlayerController.HP / 100);
		ConscienceBar.GetComponent<Scrollbar>().size = ((float)PlayerController.Conscience / 100);
	}
}