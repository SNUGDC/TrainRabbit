using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
	public string ItemName;
	public Sprite ItemImage;

	private void Start()
	{
		if(PlayerPrefs.HasKey(ItemName))
		{
			GetComponent<Image>().sprite = ItemImage;
			GetComponent<Image>().SetNativeSize();
		}
	}
}