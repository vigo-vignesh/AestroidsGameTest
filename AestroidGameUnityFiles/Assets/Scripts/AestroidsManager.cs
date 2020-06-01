using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AestroidsManager : MonoBehaviour
{
    //x= 50   y=25
    public GameObject aestroidsType1Obj;

    private void Start()
    {
        InvokeRepeating("generateAestroids", 0, 2f);
    }

    void generateAestroids()
    {
        int randomNumber = Random.Range(-25, 25);
        float randomScale = Random.Range(0.5f, 1.5f);

        if (transform.childCount <= 5)
        {
            if (randomNumber % 2 == 0)
            {
                GameObject aestroidClone = Instantiate(aestroidsType1Obj, new Vector3(-50f, randomNumber, 0), aestroidsType1Obj.transform.rotation) as GameObject;
                aestroidClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                SetAestroidProperties(aestroidClone, randomScale);
            }
            else
            {
                GameObject aestroidClone = Instantiate(aestroidsType1Obj, new Vector3(50f, randomNumber, 0), aestroidsType1Obj.transform.rotation) as GameObject;
                aestroidClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                SetAestroidProperties(aestroidClone, randomScale);
            }
        }
    }

    void SetAestroidProperties(GameObject aestroidClone, float randomScaleValue)
    {
        aestroidClone.transform.parent = transform;
        aestroidClone.SetActive(true);

        if (randomScaleValue < -1.2f)
        {
            aestroidClone.GetComponent<Aestroid>().aestroidPower = 5;
        }
        else if (randomScaleValue > 1.2f)
        {
            aestroidClone.GetComponent<Aestroid>().aestroidPower = 15;
        }
        else
        {
            aestroidClone.GetComponent<Aestroid>().aestroidPower = 10;
        }
    }
}