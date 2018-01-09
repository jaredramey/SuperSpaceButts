using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

//TODO: work on level manager
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (LevelManager)FindObjectOfType(typeof(LevelManager));
                if (instance == null)
                {
                    instance = (new GameObject("LevelManager")).AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    public void LoadNextLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
}
