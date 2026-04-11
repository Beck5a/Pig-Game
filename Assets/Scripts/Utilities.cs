using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public static class Utilities
{
    
   public static int PlayersDeaths = 0;
   public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "Next time you'll be at number"+countReference;
    }
   public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public static bool RestartLevel(int sceneIndex)
    {
        Debug.Log("Player deaths:"+ PlayersDeaths);
        string message= UpdateDeathCount(ref PlayersDeaths);
        Debug.Log("Player deaths:"+ PlayersDeaths);
        Debug.Log(message);
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;            
        return true;
    }
}
