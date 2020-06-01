using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aestroid : MonoBehaviour
{
    public int aestroidPower = 10;

    [SerializeField]
    [Range(0, 15)]
    private int AestroidSpeed;
    [SerializeField]
    private int randomNumberX;
    [SerializeField]
    private float randomNumberY;

    private void Start()
    {
        randomNumberX = Random.Range(1, 10);
        randomNumberY = Random.Range(-1f, 1f);

        DestroyThisAestroid();
    }
    private void Update()
    {
        transform.Rotate(Vector3.zero, Space.Self);

        if (randomNumberX % 2 == 0)
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(1f, randomNumberY, 0) * AestroidSpeed);
        }
        else
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(-1f, randomNumberY, 0) * AestroidSpeed);
        }
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
        if (collision.transform.tag == "Bullet")
        {
            aestroidPower = aestroidPower - collision.transform.GetComponent<Bullet1Script>().bulletPower;

            if (aestroidPower <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
