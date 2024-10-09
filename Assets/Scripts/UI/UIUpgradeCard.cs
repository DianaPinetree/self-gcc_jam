using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpgradeCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] private RectTransform shineTransform;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _body;

    private PowerUp currentPower;
    public bool Selected { get; set; }
    public RectTransform rectTrans => (transform as RectTransform);
    public Vector2 anchoredPosition
    {
        get
        {
            return (transform as RectTransform).anchoredPosition;
        }
        set
        {
            (transform as RectTransform).anchoredPosition = value;
        }
    }

    public CanvasGroup canvasGroup;
    public event Action<PowerUp> onSelect;

    private void OnDisable()
    {
        onSelect = null;
    }

    private float shineY;
    private void Awake()
    {
        shineY = shineTransform.anchoredPosition.y;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Selected) return;
        
        Debug.Log("Apply power: " + currentPower);
        currentPower?.SetPowerUp(GameController.instance.player.gameObject);
        
        Selected = true;
        onSelect?.Invoke(currentPower);
    }

    public void SetPower(PowerUp powerUp)
    {
        currentPower = powerUp;
        _body.text = powerUp.description;
        _title.text = powerUp.name;
    }

    public void Shine()
    {
        var sequence = LeanTween.sequence();
        sequence.append(LeanTween.moveY(shineTransform, 150, 0.3f)
            .setOnComplete(() => shineTransform.anchoredPosition = new Vector2(0, shineY)).setIgnoreTimeScale(true));
        sequence.append(0.05f);
        sequence.append(LeanTween.moveY(shineTransform, 150, 0.3f)
            .setOnComplete(() => shineTransform.anchoredPosition = new Vector2(0, shineY)).setIgnoreTimeScale(true));
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Shine();
    }
}