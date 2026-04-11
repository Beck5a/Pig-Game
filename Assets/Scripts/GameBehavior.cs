using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;

public class GameBehavior : MonoBehaviour, IManager 
{
    public Stack<Loot> LootStack = new Stack<Loot>();  

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }
    public Button WinButton;
    public int MaxItems = 1;
    public TMP_Text HeathText;
    public TMP_Text ItemsText;
    public TMP_Text ProgressText;

    private int _itemsCollected = 0;
    private int _playerHP = 10;
    public Button LossButton;
    

    void Start()
    {
        ItemsText.text +=  _itemsCollected;
        HeathText.text +=  _playerHP;
        WinButton.gameObject.SetActive(false);
        Initialize();

    }
    public void Initialize()
    {
        _state= "Game Manager intialized";  
        _state.FancyDebug();
        Debug.Log(_state);
        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("Hp Boost", 1));         
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Wingend Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));

        FilterLoot();
    
    }
    

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemsText.text = "Items: " + Items;

            if (_itemsCollected >= MaxItems)
            {
              
                WinButton.gameObject.SetActive(true);
               UpdateScene("You found all the items!");     
            
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    public void RestartScene()
    {
        Utilities.RestartLevel(0);
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            HeathText.text = "Health: " + HP;
            if (_playerHP <= 0)
            {
              
                LossButton.gameObject.SetActive(true);
                UpdateScene("You want another life with that?");
            }
            else
            {
                ProgressText.text = "Ouch! That's gotta hurt! ";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

        public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        
        var nextItem = LootStack.Peek();

        Debug.LogFormat("you got a {0}! youve got a good chance of finding a {1} next!", currentItem.Name, nextItem.Name);
        Debug.LogFormat("There are  {0} random loot items waiting for you ", LootStack.Count);
    }

    public void FilterLoot()
    {
        var rareLoot = LootStack.Where(LootPredicate);

        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item:{0}!", item.Name); 
        }
    }
       

    public bool LootPredicate(Loot loot)
    {
        return loot.Rarity >= 3;
    }

    void Update()
    {
    }
}