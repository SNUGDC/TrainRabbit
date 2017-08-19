using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader
{
	public List<Dialogue> dialogueList = new List<Dialogue>(); 

	private string[] text;

	public void Parse(TextAsset textFile)
	{
		dialogueList.Clear();

		text = textFile.text.Split('\n');
		
		foreach(string word in text)
		{
			string[] speakerAndText = word.Split('/');
			Dialogue dialogue = new Dialogue();

			dialogue.Speaker = speakerAndText[0];
			dialogue.Text = speakerAndText[1];

			dialogueList.Add(dialogue);
		}
	}
}