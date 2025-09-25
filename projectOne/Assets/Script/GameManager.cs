using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int vite = 3;
    public int score = 0;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // Impedisci che venga distrutto quando si cambia scena
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    private void Update()
    {
        // Debug solo per controllo
        Debug.Log("Score: " + score);
        Debug.Log("Vite: " + vite);
    }

    public void UpdateScore()
    {
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void ResetGame()
    {
        vite = 3;
        score = 0;
        UpdateScoreUI();
    }
}
