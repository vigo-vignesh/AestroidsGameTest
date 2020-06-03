using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AestroidsManager : MonoBehaviour
{
  
    int playerScore = 0;
    private void Start()
    {
        InvokeRepeating("generateAestroids", 0, 5f);
    }

    void generateAestroids()
    {
        for (int i = 0; i <= 4; i++)
        {
            int randomNumber = Random.Range(-25, 25);

            float randomScale = Random.Range(0.5f, 1.5f);

            if (transform.childCount <= 10)
            {
                if (randomNumber % 2 == 0)
                {
                    GameObject aestroidClone = Instantiate(GameManager.Instance.aestroidsType1Obj, new Vector3(-50f, randomNumber, 0f), GameManager.Instance.aestroidsType1Obj.transform.rotation) as GameObject;
                    aestroidClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                    SetAestroidProperties(aestroidClone, randomScale);
                }
                else
                {
                    GameObject aestroidClone = Instantiate(GameManager.Instance.aestroidsType1Obj, new Vector3(50f, randomNumber, 0f), GameManager.Instance.aestroidsType1Obj.transform.rotation) as GameObject;
                    aestroidClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                    SetAestroidProperties(aestroidClone, randomScale);
                }
            }
        }

        int.TryParse(GameManager.Instance.playerScore, out playerScore);

        if (playerScore > 100)
        {
            int randomNumber = 0;
            randomNumber = Random.Range(1, 11);
         //   randomNumber = 9;
            if (randomNumber >= 8)
            {
                if (randomNumber % 2 == 0)
                {
                    GameObject aestroidClone = Instantiate(GameManager.Instance.specialAestroid, new Vector3(75f, randomNumber, 0f), GameManager.Instance.specialAestroid.transform.rotation) as GameObject;
                    aestroidClone.transform.parent = transform;
                    aestroidClone.GetComponent<Aestroid>()._obstacleType = ObstacleType.SPECIAL_AESTROID;
                    aestroidClone.SetActive(true);
                }
                else
                {
                    GameObject aestroidClone = Instantiate(GameManager.Instance.specialAestroid, new Vector3(-75f, randomNumber, 0f), GameManager.Instance.specialAestroid.transform.rotation) as GameObject;
                    aestroidClone.transform.parent = transform;
                    aestroidClone.GetComponent<Aestroid>()._obstacleType = ObstacleType.SPECIAL_AESTROID;
                    aestroidClone.SetActive(true);
                }
            }
        }
    }

    void SetAestroidProperties(GameObject aestroidClone, float randomScaleValue)
    {
        aestroidClone.transform.parent = transform;
        aestroidClone.SetActive(true);

        if (randomScaleValue < 0.8f)
        {
            aestroidClone.GetComponent<Aestroid>()._obstacleType = ObstacleType.SMALL_AESTROID;
            aestroidClone.GetComponent<Aestroid>().aestroidPower = 5;
            aestroidClone.GetComponent<Aestroid>().aestroidScore = 5;
        }
        else if (randomScaleValue > 1.2f)
        {
            aestroidClone.GetComponent<Aestroid>()._obstacleType = ObstacleType.LARGE_AESTROID;
            aestroidClone.GetComponent<Aestroid>().aestroidPower = 15;
            aestroidClone.GetComponent<Aestroid>().aestroidScore = 15;
        }
        else
        {
            aestroidClone.GetComponent<Aestroid>()._obstacleType = ObstacleType.MEDIMAESTROID;
            aestroidClone.GetComponent<Aestroid>().aestroidPower = 10;
            aestroidClone.GetComponent<Aestroid>().aestroidScore = 10;
        }
    }
}