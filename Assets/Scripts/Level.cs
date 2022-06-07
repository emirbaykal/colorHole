using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int objectInScene;
    public int totalObjeect;

    [SerializeField] private Transform objectParent;
    void Start()
    {
        CountObjects();
    }

    void CountObjects()
    {
        totalObjeect = objectParent.childCount;
        objectInScene = totalObjeect;
    }
    
    public void LoadNextLevel ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("startScene");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("level1");
    }
}
