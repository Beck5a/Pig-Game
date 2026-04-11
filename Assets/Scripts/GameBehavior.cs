using UnityEngine;
using TMPro;
public class GameBehavior : MonoBehaviour
{

 public int MaxItems = 6;
    public TMP_Text ItemsText;
    public TMP_Text ProgressText;
    public TMP_Text HealthText;

    void Start()
    {
        ItemsText.text += _itemscollected;
        HealthText.text += _PigHp;
    }

    private int _itemscollected = 0;
    public int Items
    {
        get { return _itemscollected; }
        set
        {
            _itemscollected = value;
            ItemsText.text = "Items:"+Items;
           if (_itemscollected >= MaxItems)
            {
                ProgressText.text = "Found all the items! ";
            }
            else
            {
                ProgressText.text = "Items found, only "+(MaxItems - _itemscollected)+" more!";
            }
        }
    }

    private int _PigHp = 10;
    public int Hp
    {
        get { return _PigHp; }
        set
        {
            _PigHp = value;
            HealthText.text = "Health:"+Hp;
            
        }
    }
}
  
 

