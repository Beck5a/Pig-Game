using UnityEngine;

public class EscapeZoneBehavior : MonoBehaviour
{
    public GameBehavior GameManager;

    void Start()
    {
        if (GameManager == null)
        {
            GameManager = FindFirstObjectByType<GameBehavior>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pig")
        {
            if (GameManager.Items >= GameManager.MaxItems)
            {
                GameManager.WinGame();
            }
            else
            {
                GameManager.ProgressText.text = "You need to collect all items first!";
            }
        }
    }
}