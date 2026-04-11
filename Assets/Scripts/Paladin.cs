using UnityEditor.Animations;
using UnityEngine;

public class Paladin : Character
{
    public Paladin(string name):base(name)
    {
        
    }
     public Weapon PrimaryWeapon;
     public Paladin(string name, Weapon weapon) : base(name)
    {
        this.PrimaryWeapon = weapon;
    }

    public override void PrintStatsInfo()
    {

        Debug.LogFormat("Hail: {0} - take up your {1}!", this.PrimaryWeapon.Name, this.PrimaryWeapon.Name);
    }
}
