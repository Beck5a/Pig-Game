using UnityEngine;

public class CamaraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);
    private Transform _target;

    void Start()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            _target = player.transform;
        }
    }

    void LateUpdate()
    {
        if (_target == null)
        {
            GameObject player = GameObject.Find("Player");

            if (player != null)
            {
                _target = player.transform;
            }
            else
            {
                return;
            }
        }

        transform.position = _target.TransformPoint(CamOffset);
        transform.LookAt(_target);
    }
}