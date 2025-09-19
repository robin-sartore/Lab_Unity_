using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    


    public GameObject catPrefab;
    [SerializeField]
    public int score;
    public TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int x =Random.Range(-440, -395);
        int z = Random.Range(370, 420);
        Instantiate(catPrefab, new Vector3(x, 66.061f, z), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();

    }
}
