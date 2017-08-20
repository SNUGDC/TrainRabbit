using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
	public string ItemName;
	public Sprite ItemImage;
	public int Amount;

	private void Start()
	{
		if(PlayerPrefs.HasKey(ItemName))
		{
			GetComponent<Button>().interactable = true;
			GetComponent<Image>().sprite = ItemImage;
			//GetComponent<Image>().SetNativeSize();

			Amount = PlayerPrefs.GetInt(ItemName);
		}
		else
		{
			GetComponent<Button>().interactable = false;
		}
	}
}