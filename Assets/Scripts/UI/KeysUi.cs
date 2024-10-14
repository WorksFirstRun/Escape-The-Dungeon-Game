using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysUi : MonoBehaviour
{
    [SerializeField] private PlayerPickUp pickUpManager;
    [SerializeField] private TextMeshProUGUI silverKeyText;
    [SerializeField] private TextMeshProUGUI goldKeyText;

    private void Awake()
    {
        pickUpManager.updateUiForGoldenKeys += UpdateGoldKeyNumber;
        pickUpManager.updateUiForSilverKeys += UpdateSilverKeyNumber;
    }

    private void UpdateSilverKeyNumber(int obj)
    {
        silverKeyText.text = obj.ToString();
    }

    private void UpdateGoldKeyNumber(int obj)
    {
        goldKeyText.text = obj.ToString();
    }
}
