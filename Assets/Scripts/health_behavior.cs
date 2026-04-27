using UnityEngine;

public class health_behavior : MonoBehaviour
{
    public GameBehavior GameManager;
    public int healthAmount = 5;

    void Start()
    {
        if (GameManager == null)
        {
            GameManager = FindFirstObjectByType<GameBehavior>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Pig")
        {
            if (GameManager != null)
            {
                GameManager.HealPig(healthAmount);
            }

            Destroy(gameObject);
        }
    }
}