using UnityEngine;

public class Aestroid : MonoBehaviour
{
    public int aestroidPower;
    public int aestroidScore;

    [SerializeField]
    [Range(100, 500)]
    private int AestroidSpeed;
    [SerializeField]
    private int randomNumberX;
    [SerializeField]
    private float randomNumberY;
    [SerializeField]
    private float randomRotationSpeed;

    public ObstacleType _obstacleType;
    public PowerUpType _powerUpType;
    private void Start()
    {
        randomNumberX = Random.Range(1, 10);
        randomNumberY = Random.Range(-1f, 1f);
        randomRotationSpeed = Random.Range(-50f, 50f);
        if (_obstacleType != ObstacleType.SPECIAL_AESTROID)
        {
            Destroy(gameObject, 10f);
        }
        else
        {
            iTween.MoveTo(gameObject, GameManager.Instance.currentPlayerObject.transform.position, 8f);
        }
    }
    private void FixedUpdate()
    {
        switch (_obstacleType)
        {
            case ObstacleType.SPECIAL_AESTROID:

                break;

            default:
                transform.Rotate(new Vector3(0, 0, randomNumberX) * randomRotationSpeed * Time.deltaTime, Space.Self);

                if (randomNumberX % 2 == 0)
                {
                    transform.GetComponent<Rigidbody>().velocity = (new Vector3(1f, randomNumberY, 0) * AestroidSpeed * Time.deltaTime);
                }
                else
                {
                    transform.GetComponent<Rigidbody>().velocity = (new Vector3(-1f, randomNumberY, 0) * AestroidSpeed * Time.deltaTime);
                }
                break;
        }

        CheckPosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Bullet":
                aestroidPower = aestroidPower - collision.transform.GetComponent<Bullet1Script>().bulletPower;
                Destroy(collision.transform.gameObject);
                if (aestroidPower <= 0)
                {
                    int currentScore = 0;
                    int.TryParse(GameManager.Instance.playerScore, out currentScore);
                    currentScore = currentScore + aestroidScore;
                    GameManager.Instance.playerScore = currentScore.ToString();
                    DestroyCurrentAestroid();
                }
                break;

            case "Player":
                DestroyCurrentAestroid();
                break;
        }

    }

    public void DestroyCurrentAestroid()
    {
        if (_powerUpType == PowerUpType.AREABLAST)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (_obstacleType == ObstacleType.SPECIAL_AESTROID)
            {
                int randomNumber = 0;
                randomNumber = Random.Range(0, 10);

               // randomNumber = 9;
                if (randomNumber > 8)
                {
                    randomNumber = Random.Range(0, 10);
                    GameObject poewerUpClone = null;
                    if (randomNumber % 2 == 0)
                    {
                       
                        poewerUpClone = Instantiate(GameManager.Instance.powerUP1, transform.position, GameManager.Instance.powerUP1.transform.rotation) as GameObject;
                        poewerUpClone.transform.GetComponent<PowerUp>()._powerUpType = PowerUpType.BULLETPOWER;
                        poewerUpClone.SetActive(true);
                    }
                    else
                    {
                        if (randomNumber > 8)
                        {
                            poewerUpClone = Instantiate(GameManager.Instance.powerUP2, transform.position, GameManager.Instance.powerUP1.transform.rotation) as GameObject;
                            poewerUpClone.transform.GetComponent<PowerUp>()._powerUpType = PowerUpType.AREABLAST;
                            poewerUpClone.SetActive(true);
                        }
                        else
                        {
                            poewerUpClone = Instantiate(GameManager.Instance.powerUP2, transform.position, GameManager.Instance.powerUP1.transform.rotation) as GameObject;
                            poewerUpClone.transform.GetComponent<PowerUp>()._powerUpType = PowerUpType.SHIELD;
                            poewerUpClone.SetActive(true);
                        }
                    }
                    Destroy(poewerUpClone.gameObject, 3f);
                }
            }
            Destroy(this.gameObject);
        }
    }

    private void CheckPosition()
    {
        float sceneWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        float sceneHeight = Camera.main.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (transform.position.x > sceneRightEdge)
        {
            transform.position = new Vector2(sceneLeftEdge, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge)
        {
            transform.position = new Vector2(sceneRightEdge, transform.position.y);
        }
        if (transform.position.y > sceneTopEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }
        if (transform.position.y < sceneBottomEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }
}


