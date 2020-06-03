using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private int sideBoundaryValue = 75;
    private int topBoundaryValue = 25;

    public LevelManager _levelManager;
    [SerializeField]
    private int playerScore = 0;
    private void Start()
    {
        InvokeRepeating("LevelCheckManager", 0, 5f);
    }

    void LevelCheckManager()
    {
        int.TryParse(GameManager.Instance.playerScore, out playerScore);

        switch (playerScore)
        {
            case int n when (n >= 0 && n <= 100):
                _levelManager = LevelManager.LEVEL1;
                GenerateAestroids(2, 0.8f);
                break;

            case int n when (n >= 101 && n <= 200):
                _levelManager = LevelManager.LEVEL2;
                GenerateAestroids(3, 0.8f);
                break;

            case int n when (n > 201 && n < 300):
                _levelManager = LevelManager.LEVEL3;
                GenerateAestroids(5, 1f);
                GenerateSpecialAestroids();
                break;

            default:
                _levelManager = LevelManager.LEVEL4;
                GenerateAestroids(5, 1.5f);
                GenerateSpecialAestroids();
                break;
        }
    }

    void GenerateAestroids(int aestroidMaxCount, float MaxScaleSize)
    {
        for (int i = 0; i <= aestroidMaxCount; i++)
        {
            int randomNumber = Random.Range(-topBoundaryValue, topBoundaryValue);

            float randomScale = Random.Range(0.5f, MaxScaleSize);

            if (transform.childCount <= 10)
            {
                if (randomNumber % 2 == 0)
                {
                    sideBoundaryValue = -sideBoundaryValue;
                }
                GameObject aestroidClone = Instantiate(GameManager.Instance.aestroidsType1Obj, new Vector3(sideBoundaryValue, randomNumber, 0f), GameManager.Instance.aestroidsType1Obj.transform.rotation) as GameObject;
                aestroidClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                SetAestroidProperties(aestroidClone, randomScale);
            }
        }
    }

    void GenerateSpecialAestroids()
    {
        int randomNumber = 0;
        randomNumber = Random.Range(1, 10);
        if (randomNumber >= 7)
        {
            GameManager specialObjectToGenerate = null;

            if (randomNumber % 2 == 0)
            {
                sideBoundaryValue = -sideBoundaryValue;
            }

            randomNumber = Random.Range(1, 10);
            if (randomNumber >= 8)
            {
                GameObject specialObject = Instantiate(GameManager.Instance.ai_SpaceShip, new Vector3(sideBoundaryValue, randomNumber, 0f), GameManager.Instance.ai_SpaceShip.transform.rotation) as GameObject;
                specialObject.transform.parent = transform;
                specialObject.GetComponent<Obstacle>()._obstacleType = ObstacleType.AI_SPACESHIP;
                specialObject.SetActive(true);
            }
            else
            {
                GameObject specialObject = Instantiate(GameManager.Instance.specialAestroid, new Vector3(sideBoundaryValue, randomNumber, 0f), GameManager.Instance.specialAestroid.transform.rotation) as GameObject;
                specialObject.transform.parent = transform;
                specialObject.GetComponent<Obstacle>()._obstacleType = ObstacleType.SPECIAL_AESTROID;
                specialObject.SetActive(true);
            }






        }
    }

    void SetAestroidProperties(GameObject aestroidClone, float randomScaleValue)
    {
        aestroidClone.transform.parent = transform;
        aestroidClone.SetActive(true);

        if (randomScaleValue < 0.8f)
        {
            aestroidClone.GetComponent<Obstacle>()._obstacleType = ObstacleType.SMALL_AESTROID;
            aestroidClone.GetComponent<Obstacle>().obstaclePower = 5;
            aestroidClone.GetComponent<Obstacle>().obstacleScore = 5;
        }
        else if (randomScaleValue > 1.2f)
        {
            aestroidClone.GetComponent<Obstacle>()._obstacleType = ObstacleType.LARGE_AESTROID;
            aestroidClone.GetComponent<Obstacle>().obstaclePower = 15;
            aestroidClone.GetComponent<Obstacle>().obstacleScore = 15;
        }
        else
        {
            aestroidClone.GetComponent<Obstacle>()._obstacleType = ObstacleType.MEDIMAESTROID;
            aestroidClone.GetComponent<Obstacle>().obstaclePower = 10;
            aestroidClone.GetComponent<Obstacle>().obstacleScore = 10;
        }
    }
}