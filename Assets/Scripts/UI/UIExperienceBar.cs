using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExperienceBar : MonoBehaviour

{
    public static UIExperienceBar instance;
    [SerializeField] private Image xpBar;
    [SerializeField] private TextMeshProUGUI levelText;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        xpBar.fillAmount = 0f;
    }

    public void UpdateExperienceBar(float value, float max)
    {
        xpBar.fillAmount = value / max;
    }

    public void SetLevelText(int level)
    {
        levelText.text = $"Level {level}";
    }
}