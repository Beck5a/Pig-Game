using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Loot
{
    public string Name;
    public int Rarity;


   public Loot(string name, int rarity)
    {
        this.Name = name;
        this.Rarity = rarity;
    }
}