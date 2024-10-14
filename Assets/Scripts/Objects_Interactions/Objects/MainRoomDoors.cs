using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomDoors : MonoBehaviour
{
    [SerializeField] private int doorNumber;
    [SerializeField]
    private SaveLoadOpenedDoors StateSaver;

    [SerializeField] private DoorScript doorScript;

    private void Start()
    {
        doorScript.onSaveState += () => { StateSaver.SaveDoorState(doorNumber,true);};
    }
}
