using UnityEngine;

public class ItemRotation : MonoBehaviour
{
   public int RotationSpeed = 100;
   private Transform _itemTransform;
    void Start()
    {
        _itemTransform = this.GetComponent<Transform>();
    }

    
    void Update()
    {
        _itemTransform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime,0,0);
    }
}
