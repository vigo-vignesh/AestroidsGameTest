using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aestroid : MonoBehaviour
{
    public int aestroidPower;
    public int aestroidScore;

    [SerializeField]
    [Range(10, 50)]
    private int AestroidSpeed;
    [SerializeField]
    private int randomNumberX;
    [SerializeField]
    private float randomNumberY;
    [SerializeField]
    private float randomRotationSpeed;

    private void Start()
    {
        randomNumberX = Random.Range(1, 10);
        randomNumberY = Random.Range(-1f, 1f);
        randomRotationSpeed = Random.Range(-50f, 50f);
        DestroyThisAestroid();
    }
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(1f, randomNumberY, 0f) * randomRotationSpeed * Time.deltaTime, Space.Self);

        if (randomNumberX % 2 == 0)
        {
            transform.GetComponent<Rigidbody>().velocity = (new Vector3(1f, randomNumberY, 0) * AestroidSpeed * 10f * Time.deltaTime);
        }
        else
        {
            transform.GetComponent<Rigidbody>().velocity = (new Vector3(-1f, randomNumberY, 0) * AestroidSpeed * 10f * Time.deltaTime );
        }


        //if (randomNumberX % 2 == 0)
        //{
        //    transform.GetComponent<Rigidbody>().AddForce(new Vector3(1f, randomNumberY, 0) * AestroidSpeed);
        //}
        //else
        //{
        //    transform.GetComponent<Rigidbody>().AddForce(new Vector3(-1f, randomNumberY, 0) * AestroidSpeed);
        //}

        //if (randomNumberX % 2 == 0)
        //{
        //    transform.Translate(new Vector3(1f, randomNumberY, 0f) * Time.deltaTime* AestroidSpeed, Space.Self);
        //}
        //else
        //{
        //    transform.Translate(new Vector3(-1f, randomNumberY, 0f) * Time.deltaTime* AestroidSpeed, Space.Self);
        //}



        CheckPosition();
    }
    void DestroyThisAestroid()
    {
        Destroy(gameObject, 10f);
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
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
                }
                break;

            case "Player":
                Destroy(this.gameObject);
                break;
        }
        
    }
}
