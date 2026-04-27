using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameBehavior : MonoBehaviour, IManager
{
    private int _itemsCollected = 0;
    private int _PigHP = 15;

    public int MaxItems = 4;
    public int MaxHP = 15;

    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;

    public Button WinButton;
    public Button LossButton;

    public GameObject EscapeZone;

    public Stack<Loot> LootStack = new Stack<Loot>();

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    void Start()
    {
        HealthText.text = "Health: " + _PigHP;
        ItemText.text = "Items: " + _itemsCollected + " / " + MaxItems;

        if (WinButton != null)
            WinButton.gameObject.SetActive(false);

        if (LossButton != null)
            LossButton.gameObject.SetActive(false);

        if (EscapeZone != null)
            EscapeZone.SetActive(false);

        Initialize();
    }

    public void Initialize()
    {
        _state = "Game Manager initialized.";
        Debug.Log(_state);

        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));

        FilterLoot();
    }

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items: " + _itemsCollected + " / " + MaxItems;

            if (_itemsCollected >= MaxItems)
            {
                ProgressText.text = "You found all the items! Get to the barn!";
                
                if (EscapeZone != null)
                    EscapeZone.SetActive(true);
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    public int HP
    {
        get { return _PigHP; }
        set
        {
            _PigHP = Mathf.Clamp(value, 0, MaxHP);
            HealthText.text = "Health: " + _PigHP;

            if (_PigHP <= 0)
            {
                LossButton.gameObject.SetActive(true);
                UpdateScene("You want another life with that?");
            }
        }
    }

    public void HealPig(int amount)
    {
        HP += amount;
        ProgressText.text = "Health restored!";
    }

    public void DamagePig(int amount)
    {
        HP -= amount;

        if (HP > 0)
        {
            ProgressText.text = "Ouch! The farmer hurt you!";
        }
    }

    public void WinGame()
    {
        WinButton.gameObject.SetActive(true);
        UpdateScene("You escaped the farm!");
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        Utilities.RestartLevel(0);
    }

    public void PrintLootReport()
    {
        if (LootStack.Count <= 1)
        {
            Debug.Log("No more loot hints available.");
            return;
        }

        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem.Name, nextItem.Name);
        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count);
    }

    public void FilterLoot()
    {
        var rareLoot = (from item in LootStack
                        where item.Rarity >= 3
                        orderby item.Rarity
                        select new { item.Name }).Skip(1);

        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.Name);
        }
    }

    public bool LootPredicate(Loot loot)
    {
        return loot.Rarity >= 3;
    }
}