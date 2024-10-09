using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradesView : MonoBehaviour
{
    public bool Active { get; private set; }
    public static UIUpgradesView instance;

    [SerializeField] private GameUpgrades _upgrades;

    [Header("UI")] [SerializeField] private UIUpgradeCard[] cardRoots; // ordered
    [SerializeField] private CanvasGroup root;
    [SerializeField] private HorizontalLayoutGroup layout;

    private Vector2[] startPositions;
    private int queue = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startPositions = new Vector2[cardRoots.Length];
        for (int i = 0; i < cardRoots.Length; i++)
        {
            startPositions[i] = cardRoots[i].anchoredPosition;
        }

        root.alpha = 0f;
        root.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (queue > 0 && !Active)
        {
            queue--;
            ShowPanel();
        }
    }

    public void GetUpgrade(GameObject target)
    {
        queue++;
    }

    private void Close(PowerUp selected)
    {
        GameController.instance.CollectedPowerUps.Add(selected);
        Active = false;
        Time.timeScale = 1f;
        Cursor.visible = false;

        for (int i = 0; i < cardRoots.Length; i++)
        {
            cardRoots[i].Selected = true;
        }

        root.LeanAlpha(0f, 0.7f).setEaseOutCubic().setOnComplete(Reset);

        void Reset()
        {
            for (int i = 0; i < cardRoots.Length; i++)
            {
                cardRoots[i].Selected = false;
                cardRoots[i].anchoredPosition = startPositions[i];
            }

            root.gameObject.SetActive(false);
        }
    }

    private void ShowPanel()
    {
        Active = true;
        Time.timeScale = 0f;
        var upgrades = _upgrades.GetRandom(GameController.instance.CollectedPowerUps, 3);
        int visibleCards = upgrades.Count;
        for (int i = 0; i < cardRoots.Length; i++)
        {
            if (i >= visibleCards)
            {
                cardRoots[i].gameObject.SetActive(false);
            }
        }

        Cursor.visible = true;
        // after level up audio
        root.gameObject.SetActive(true);

        StartCoroutine(DisplayCards(upgrades, visibleCards));
        
    }

    private IEnumerator DisplayCards(List<PowerUp> upgrades, int visibleCards)
    {
        root.LeanAlpha(1f, 0.6f).setEaseInCubic().setIgnoreTimeScale(true);
        
        for (int i = 0; i < visibleCards; i++)
        {
            var card = cardRoots[i];
            card.canvasGroup.alpha = 0f;
            card.SetPower(upgrades[i]);
            card.onSelect += Close;
        }
        
        for (int i = 0; i < visibleCards; i++)
        {
            var card = cardRoots[i];
            var graphicsTrans = card.transform.Find("Graphics").gameObject.GetComponent<RectTransform>();
            graphicsTrans.anchoredPosition = new Vector2(graphicsTrans.anchoredPosition.x, graphicsTrans.anchoredPosition.y -150);
            card.canvasGroup.LeanAlpha(1f, 0.34f).setEaseLinear().setIgnoreTimeScale(true).setOnComplete(() => card.Shine());
            LeanTween.moveY(graphicsTrans, graphicsTrans.anchoredPosition.y + 150, 0.4f).setEaseInCubic().setIgnoreTimeScale(true);

            yield return new WaitForSecondsRealtime(0.24f);
        }
    }
}