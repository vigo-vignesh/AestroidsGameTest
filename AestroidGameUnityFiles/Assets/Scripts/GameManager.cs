using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string playerScore = "0";
    public Text scoreText;

    public float playerHealth = 1;
    public Image healthBarImage;

    public int highScore;

    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("Aestroids_HighScore");
    }

    private void Update()
    {
        if (playerScore != scoreText.text)
        {
            scoreText.text = playerScore;
        }
    }
}
