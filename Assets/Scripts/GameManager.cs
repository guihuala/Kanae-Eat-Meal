using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] dishes;
    public dish targetDish;
    public Image targetDishImage;

    public Hatch hatch;

    public Button startButton;
    public Button pause;
    public Button restart;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public GameObject hands;
    public Button helpButton;

    public Canvas helpCanvas;

    public List<PlateNum> availablePositions;

    public float gameDuration = 60f;
    private bool isGameRunning = false;
    private float timeRemaining;

    private int score = 0;

    public Sprite pauseImage;
    public Sprite resumeImage;

    public Imagemanager imageManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }

        if (pause)
        {
            pause.gameObject.SetActive(false);
        }
        if (restart)
        {
            restart.gameObject.SetActive(false);
        }

        if (targetDishImage)
        {
            targetDishImage.gameObject.SetActive(false);
        }
        if (hands)
        {
            hands.gameObject.SetActive(false);
        }
        if (helpCanvas)
        {
            helpCanvas.gameObject.SetActive(false);
        }

        startButton.onClick.AddListener(StartGame);
    }

    private void Update()
    {
        if (isGameRunning)
        {
            timeRemaining -= Time.deltaTime;
            ChangeImageByScore();
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        timeRemaining = gameDuration;
        isGameRunning = true;
        score = 0;
        availablePositions = new List<PlateNum>();

        UpdateScoreText();

        foreach (PlateNum position in System.Enum.GetValues(typeof(PlateNum)))
        {
            availablePositions.Add(position);
        }

        startButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        targetDishImage.gameObject.SetActive(true);
        helpButton.gameObject.SetActive(false);
        hands.gameObject.SetActive(true);

        SelectRandomDish();

        audioManager.Instance.playSfx("tabetai");
        hatch.StartGenerating();
    }

    private void EndGame()
    {
        isGameRunning = false;

        startButton.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        targetDishImage.gameObject.SetActive(false);
        helpButton.gameObject.SetActive(true);
        hands.gameObject.SetActive(false);

        hatch.ClearAllDishes();
        hatch.StopGenerating();

    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void SelectRandomDish()
    {
        int randomIndex = Random.Range(0, dishes.Length);
        targetDish = dishes[randomIndex].GetComponent<dish>();

        targetDishImage.sprite = targetDish.dishImage;
    }

    public void OnDishClick(dish clickedDish)
    {
        if (clickedDish.dishName == targetDish.dishName)
        {
            score += 10;
            UpdateScoreText();
            SelectRandomDish();
            audioManager.Instance.playSfx("uma");
        }
        else
        {
            score -= 5;
            UpdateScoreText();
            audioManager.Instance.playSfx("nande");
        }
    }


    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public dish GetRandomDish()
    {
        int randomIndex = Random.Range(0, dishes.Length);
        return dishes[randomIndex].GetComponent<dish>();
    }

    public void RestartGame()
    {
        hatch.ClearAllDishes();

        timeRemaining = gameDuration;
        isGameRunning = true;
        score = 0;
        availablePositions = new List<PlateNum>();

        UpdateScoreText();

        foreach (PlateNum position in System.Enum.GetValues(typeof(PlateNum)))
        {
            availablePositions.Add(position);
        }

        SelectRandomDish();

        hatch.StartGenerating();
    }

    public void PsuseOResume()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pause.GetComponentInChildren<Image>().sprite = pauseImage;
        }
        else
        {
            Time.timeScale = 1;
            pause.GetComponentInChildren<Image>().sprite = resumeImage;
        }
    }

    public void toggleCanvas()
    {
        helpCanvas.gameObject.SetActive(!helpCanvas.gameObject.activeSelf);
    }

    private void ChangeImageByScore()
    {
        if (imageManager != null)
        {
            if (score >= 50)
            {
                imageManager.changeImage("excited");
            }
            else if (score >= 20)
            {
                imageManager.changeImage("happy");
            }
            else if (score <= -50)
            {
                imageManager.changeImage("sad");
            }
            else if (score <= -20)
            {
                imageManager.changeImage("cry");
            }
            else
            {
                imageManager.changeImage("normal");
            }
        }
    }
}

