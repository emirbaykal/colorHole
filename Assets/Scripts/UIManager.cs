using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [Header("Level Progress UI")] 
    [SerializeField]
    private int sceneOffset;
    [SerializeField] 
    private TMP_Text nextLevelText;
    [SerializeField] 
    private TMP_Text currentLevelText;
    [SerializeField]
    private Image progressFillImage;
    [SerializeField] 
    public GameObject levelCompletedText;

    public ParticleSystem confetti;
    void Start()
    {
        levelCompletedText.SetActive(false);
        progressFillImage.fillAmount = 0f;
        SetLevelProgressText();
        confetti = GetComponent<ParticleSystem>();
    }

    void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
        if (level == 4)
        {
            level = 1;
        }
    }
    
    public void UpdateLevelProgress()
    {
        float value = 1f - ((float) Level.instance.objectInScene / Level.instance.totalObjeect);
        progressFillImage.fillAmount = value;
    }
}
