using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager;
  
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Pig")
        {
           Destroy(this.transform.parent.gameObject);
           Debug.Log("Item collected!!!");
           GameManager.Items += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
