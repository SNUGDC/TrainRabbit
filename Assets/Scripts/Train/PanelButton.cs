using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    public Button yourButton;
	public int textPanelPress;

    void Start()
    {
		textPanelPress = 0;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        textPanelPress++;
        Debug.Log("Panel Press : " + textPanelPress);
    }

}