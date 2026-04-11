using UnityEngine;

public class CamaraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);
    [SerializeField] private Transform _target;

    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = _target.TransformPoint(CamOffset);
        transform.LookAt(_target);
    }
}