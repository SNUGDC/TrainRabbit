using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public QuestItemInfo[] QuestItem;
	[HideInInspector]
	public string QuestItemName;

	public void CreateQuestItem()
	{
		Instantiate(GetItemFromName(QuestItemName));
	}

	private GameObject GetItemFromName(string name)
	{
		for(int i = 0; i < QuestItem.Length; i++)
		{
			if(QuestItem[i].Name == name)
			{
				return QuestItem[i].ItemPrefab;
			}
		}

		Debug.Log("에러남!");
		return null;
	}
}