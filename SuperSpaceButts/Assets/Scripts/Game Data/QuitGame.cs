using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
    public static QuitGame instance;

    public static QuitGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (QuitGame)FindObjectOfType(typeof(QuitGame));
                if (instance == null)
                {
                    instance = (new GameObject("LevelManager")).AddComponent<QuitGame>();
                }
            }
            return instance;
        }
    }

    public void Quit()
    {
        if (LevelManager.Instance.GetCurrentLevel() != "Menu_Main")
        {
            LevelManager.Instance.LoadNextLevel("Menu_Main");
        }
        else
        {
            Application.Quit();
        }
    }
}
