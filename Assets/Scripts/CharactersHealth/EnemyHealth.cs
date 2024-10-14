using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealthScript
{
    [SerializeField] private LootSO loot;

    public LootSO GetLoot()
    {
        return loot;
    }
}
