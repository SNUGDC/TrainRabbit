using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
	public ItemInfo[] CollectItem;

	private GameObject ItemInfoPanel;
	private Text ItemNameBox;
	private Text ItemInfoBox;
	private Image ItemImageBox;

	private void Start()
	{
		ItemInfoPanel = GameObject.Find("Canvas").transform.Find("Item Info Panel").gameObject;

		GameObject InfoPanel = ItemInfoPanel.transform.Find("Item Info").gameObject;

		ItemNameBox = InfoPanel.transform.Find("Item Name").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
		ItemInfoBox = InfoPanel.transform.Find("Item Info Box").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
		ItemImageBox = InfoPanel.transform.Find("Item Box").gameObject.transform.Find("Image").gameObject.GetComponent<Image>();
	}

	public void SeeDetail(string ItemName)
	{
		int ItemNumber = GetItemNumberFromName(ItemName);

		ItemNameBox.text = CollectItem[ItemNumber].Name;
		ItemInfoBox.text = CollectItem[ItemNumber].Information;
		ItemImageBox.sprite = CollectItem[ItemNumber].ItemPrefab.GetComponent<SpriteRenderer>().sprite;
		ItemImageBox.SetNativeSize();

		ItemInfoPanel.SetActive(true);
	}

	private int GetItemNumberFromName(string ItemName)
	{
		for(int i = 0; i < CollectItem.Length; i++)
		{
			if(CollectItem[i].Name == ItemName)
			{
				Debug.Log(i);
				return i;
			}
		}

		Debug.Log("Error!!!");
		return 0;
	}
}