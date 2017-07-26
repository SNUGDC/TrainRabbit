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
	}
	private void Update()
	{
		HPBar.GetComponent<Scrollbar>().size = ((float)GetComponent<BasicRabbitController>().HP / 100);
		ConscienceBar.GetComponent<Scrollbar>().size = ((float)GetComponent<PlayerController>().Conscience / 1000);
	}
}