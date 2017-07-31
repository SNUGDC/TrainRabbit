using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
	static public int? trainNumber;

    private void Awake()
    {
        if(trainNumber.HasValue == false)
            trainNumber = 20;
    }

    public bool IsThereNextTrain(bool isRightDoor)
    {
        if (trainNumber == 10 && isRightDoor == false)
            return false;
        else if (trainNumber == 1 && isRightDoor == true)
            return false;

        return true;
    }
}