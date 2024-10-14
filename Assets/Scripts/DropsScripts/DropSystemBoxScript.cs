using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropSystem : MonoBehaviour
{
   public static DropSystem Instance { get; private set; }
   public event EventHandler<DropPosition> DropAtEvent;
   
   public class DropPosition : EventArgs
   {
      public Vector2 position;
      public LootSO loot;
   }

   private void Awake()
   {
      Instance = this;
      DropAtEvent += DropItem;  
   }

   public void DropItem(object sender, DropPosition e)
   {
      GameObject item = GetDroppedItem(e.loot);
      if (item != null)
      {
         Instantiate(item, e.position, quaternion.identity);
      }
   }

   public void InvokeDropEvent(object sender,LootSO loot,Vector2 position)
   {
      DropAtEvent?.Invoke(sender,new DropPosition()
      {
         loot = loot,
         position = position
      });
   }
   
   
   public GameObject GetDroppedItem(LootSO loot) // pass the List of items here 
   {
      
      float randomValue = Random.Range(0, 100f);

      float cumulativeRate = 0f;

      foreach (LootSO.Items item in loot.itemsList)
      {
         cumulativeRate += item.dropRate;
         if (randomValue <= cumulativeRate)
         {
            return item.Item;
         }
      }

      return null; 
   }
   
  
}
