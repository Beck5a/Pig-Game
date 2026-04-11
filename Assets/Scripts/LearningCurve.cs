using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem.Interactions;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class learningcurve : MonoBehaviour
{  
   public Transform CamTransform; 



    void Start()
    {Character hero = new Character();
       hero.PrintStatsInfo();
        
      Character heroine = new Character("Agatha");
        heroine.PrintStatsInfo();

    Weapon huntingBow = new Weapon("Hunting Bow",105);
    huntingBow.PrintWeaponStats();
    
    Weapon warBow = huntingBow;
    warBow.Name = "War Bow";
    warBow.Damage= 155; 
    warBow.PrintWeaponStats();

    Character assistantHero = hero; 
    assistantHero.Name = "Sir Kane the Bold"; 
      assistantHero.PrintStatsInfo(); 
      
      Paladin knight = new Paladin("Sir Aurthur",huntingBow);
      knight.PrintStatsInfo();
       
     
     CamTransform = this.GetComponent<Transform>();
     Debug.Log(CamTransform.localPosition);
    
     
    }
       void Update()
    {
        
    }


 
 
    
}
