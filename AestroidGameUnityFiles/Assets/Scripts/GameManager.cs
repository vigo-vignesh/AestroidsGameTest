using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlayerObject;

    public string playerScore = "0";
    public Text scoreText;

    public float playerHealth = 1;
    public Image healthBarImage;

    public int highScore;

    public GameObject aestroidsType1Obj;
    public GameObject specialAestroid;
    public GameObject ai_SpaceShip;

    public GameObject powerUP1;
    public GameObject powerUP2;
    public GameObject powerUP3;

    public GameObject aestroidsContainer;

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

public enum ObstacleType
{
    SMALL_AESTROID,
    MEDIMAESTROID,
    LARGE_AESTROID,
    SPECIAL_AESTROID,
    AI_SPACESHIP
}

public enum PowerUpType
{
    NONE,
    HEALTH,
    BULLETPOWER,
    SHIELD
}

public enum LevelManager
{
    LEVEL1,
    LEVEL2,
    LEVEL3,
    LEVEL4
}
