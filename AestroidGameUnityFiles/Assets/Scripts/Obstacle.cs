using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int obstaclePower;
    public int obstacleScore;

    public GameObject ai_BulletObj;

    [SerializeField]
    [Range(100, 500)]
    private int obstacleSpeed;
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
            // iTween.MoveTo(gameObject, GameManager.Instance.currentPlayerObject.transform.position, 8f);
        }

        if (_obstacleType != ObstacleType.AI_SPACESHIP)
        {
            InvokeRepeating("fireBulletAISpaceShip", 0, 1f);
        }
    }

    void fireBulletAISpaceShip()
    {
        GameObject bulletClone = Instantiate(ai_BulletObj, new Vector3(ai_BulletObj.transform.position.x, ai_BulletObj.transform.position.y, 0f), ai_BulletObj.transform.rotation);
        bulletClone.transform.localScale = new Vector3(0.3f, 0.5f, 1.5f);
        bulletClone.SetActive(true);
    }

    private void FixedUpdate()
    {
        switch (_obstacleType)
        {
            case ObstacleType.AI_SPACESHIP:
                transform.GetComponent<Rigidbody>().velocity = (new Vector3(1f, randomNumberY, 0) * obstacleSpeed * Time.deltaTime);
                break;

            case ObstacleType.SPECIAL_AESTROID:
                transform.position = Vector3.Lerp(transform.position, GameManager.Instance.currentPlayerObject.transform.position, Time.deltaTime * 0.5f);

                break;

            default:
                transform.Rotate(new Vector3(0, 0, randomNumberX) * randomRotationSpeed * Time.deltaTime, Space.Self);

                if (randomNumberX % 2 == 0)
                {
                    transform.GetComponent<Rigidbody>().velocity = (new Vector3(1f, randomNumberY, 0) * obstacleSpeed * Time.deltaTime);
                }
                else
                {
                    transform.GetComponent<Rigidbody>().velocity = (new Vector3(-1f, randomNumberY, 0) * obstacleSpeed * Time.deltaTime);
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
                obstaclePower = obstaclePower - collision.transform.GetComponent<Bullet1Script>().bulletPower;
                Destroy(collision.transform.gameObject);
                if (obstaclePower <= 0)
                {
                    int currentScore = 0;
                    int.TryParse(GameManager.Instance.playerScore, out currentScore);
                    currentScore = currentScore + obstacleScore;
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
        if (_obstacleType == ObstacleType.LARGE_AESTROID)
        {
            GenerateAestroidsOnDestroy(3);
        }
        else if (_obstacleType == ObstacleType.MEDIMAESTROID)
        {
            GenerateAestroidsOnDestroy(2);
        }
        if (_powerUpType == PowerUpType.HEALTH || _powerUpType == PowerUpType.SHIELD)
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
                if (randomNumber >= 7)
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
                            poewerUpClone.transform.GetComponent<PowerUp>()._powerUpType = PowerUpType.HEALTH;
                            poewerUpClone.SetActive(true);
                        }
                        else
                        {
                            poewerUpClone = Instantiate(GameManager.Instance.powerUP3, transform.position, GameManager.Instance.powerUP1.transform.rotation) as GameObject;
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
    void GenerateAestroidsOnDestroy(int countToGenerate)
    {
        for (int i = 0; i < countToGenerate; i++)
        {
            float randomScale = Random.RandomRange(0.5f, 0.8f);
            GameObject aestroidClone = Instantiate(GameManager.Instance.aestroidsType1Obj, new Vector3(transform.position.x, transform.position.y, 0f), GameManager.Instance.aestroidsType1Obj.transform.rotation) as GameObject;
            aestroidClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            aestroidClone.GetComponent<Obstacle>()._obstacleType = ObstacleType.SMALL_AESTROID;
            aestroidClone.GetComponent<Obstacle>().obstaclePower = 5;
            aestroidClone.GetComponent<Obstacle>().obstacleScore = 5;
            aestroidClone.SetActive(true);
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


