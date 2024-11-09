using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PlateNum
{
    one,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine
}

public class dish : MonoBehaviour, IPointerClickHandler
{
    public PlateNum plate;
    private RectTransform rectTransform;
    public RectTransform parentRectTransform;
    public delegate void DishDestroyed();
    public event DishDestroyed OnDishDestroyed;
    public string dishName;
    public Sprite dishImage;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = gameObject.AddComponent<RectTransform>();
        }
    }

    public void SetDishInfo(dish dishData)
    {
        dishName = dishData.dishName;
        dishImage = dishData.dishImage;
        GetComponent<Image>().sprite = dishImage;
    }

    private void Start()
    {
        SetPosition(plate);
    }

    public void SetPosition(PlateNum platenum)
    {
        Vector2 anchoredPosition = Vector2.zero;
        switch (platenum)
        {
            case PlateNum.one:
                anchoredPosition = new Vector2(-700,250);
                break;
            case PlateNum.two:
                anchoredPosition = new Vector2(-250, 250);
                break;
            case PlateNum.three:
                anchoredPosition = new Vector2(200, 250);
                break;
            case PlateNum.four:
                anchoredPosition = new Vector2(-700, -10);
                break;
            case PlateNum.five:
                anchoredPosition = new Vector2(-250, -10);
                break;
            case PlateNum.six:
                anchoredPosition = new Vector2(200, -10);
                break;
            case PlateNum.seven:
                anchoredPosition = new Vector2(-700, -270);
                break;
            case PlateNum.eight:
                anchoredPosition = new Vector2(-250, -270);
                break;
            case PlateNum.nine:
                anchoredPosition = new Vector2(200, -270);
                break;
        }
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.SetParent(parentRectTransform, false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
        {
            GameManager.Instance.OnDishClick(this);
            OnDishDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}

