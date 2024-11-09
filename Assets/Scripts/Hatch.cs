using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hatch : MonoBehaviour
{
    public GameObject dishPrefab;
    public RectTransform rootCanvas;
    private bool isGenerating = false;
    private Dictionary<PlateNum, GameObject> occupiedPositions = new Dictionary<PlateNum, GameObject>();

    public void StartGenerating()
    {
        if (!isGenerating)
        {
            isGenerating = true;
            occupiedPositions.Clear();
            StopAllCoroutines();
            StartCoroutine(GenerateDishes());
        }
    }

    private IEnumerator GenerateDishes()
    {
        while (isGenerating)
        {
            if (GameManager.Instance.availablePositions.Count > occupiedPositions.Count)
            {
                CreateDish();
            }
            yield return new WaitForSeconds(Random.Range(.2f, 1f));
        }
    }

    private void CreateDish()
    {
        List<PlateNum> availablePositions = new List<PlateNum>(GameManager.Instance.availablePositions);
        availablePositions.RemoveAll(pos => occupiedPositions.ContainsKey(pos));

        if (availablePositions.Count == 0)
            return;

        int randomIndex = Random.Range(0, availablePositions.Count);
        PlateNum selectedPosition = availablePositions[randomIndex];

        GameObject instance = Instantiate(dishPrefab, rootCanvas);
        dish randomDish = GameManager.Instance.GetRandomDish();
        dish dishComponent = instance.GetComponent<dish>();
        dishComponent.SetDishInfo(randomDish);
        dishComponent.plate = selectedPosition;
        dishComponent.parentRectTransform = rootCanvas;

        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(2, 0.5f, 0.5f);
        rectTransform.DOScale(new Vector3(2, 2.5f, 1f), 0.5f)
            .SetEase(Ease.OutElastic)
            .OnComplete(() =>
            {
                rectTransform.DOScale(new Vector3(2, 2f, 1f), 0.1f);
            });

        occupiedPositions[selectedPosition] = instance;

        dishComponent.OnDishDestroyed += () =>
        {
            occupiedPositions.Remove(selectedPosition);
        };

        float randomDestroyTime = Random.Range(2f, 3f);
        StartCoroutine(DestroyAfterTime(instance, randomDestroyTime, selectedPosition));
    }

    private IEnumerator DestroyAfterTime(GameObject instance, float time, PlateNum position)
    {
        yield return new WaitForSeconds(time);
        if (occupiedPositions.ContainsKey(position) && occupiedPositions[position] == instance)
        {
            occupiedPositions.Remove(position);
        }
        Destroy(instance);
    }

    public void StopGenerating()
    {
        isGenerating = false;
        StopAllCoroutines();
    }

    public void ClearAllDishes()
    {
        foreach (var dish in occupiedPositions.Values)
        {
            Destroy(dish);
        }
        occupiedPositions.Clear();
    }
}


