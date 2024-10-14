using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeysScript : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    public enum KeyType
    {
        GoldenKey,
        SilverKey
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyType == KeyType.GoldenKey)
            {
                other.GetComponent<PlayerPickUp>().AddGoldenKey();
            }
            else
            {
                other.GetComponent<PlayerPickUp>().AddSilverKey();
            }
            Destroy(gameObject);
        }
    }

    public void SetKeyType(KeyType keyType)
    {
        this.keyType = keyType;
    }
}
