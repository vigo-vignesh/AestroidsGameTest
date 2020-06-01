using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float spaceShipHealth = 10f;

    public Rigidbody playerRigidBody;
    private float verticalInput;

    [Range(10f, 25f)]
    public float playerSpeed;

    private Vector3 target;

    public GameObject bulletObject;

    private void Update()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        SpaceShipMovement();
        CheckPosition();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            BulletFire();
        }
    }
    void SpaceShipMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0)
        {
            playerRigidBody.AddForce(new Vector3(target.x, target.y, 0) * playerSpeed);
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
            transform.position = new Vector3(sceneLeftEdge, transform.position.y, 0);
        }
        if (transform.position.x < sceneLeftEdge)
        {
            transform.position = new Vector3(sceneRightEdge, transform.position.y, 0);
        }
        if (transform.position.y > sceneTopEdge)
        {
            transform.position = new Vector3(transform.position.x, sceneBottomEdge, 0);
        }
        if (transform.position.y < sceneBottomEdge)
        {
            transform.position = new Vector3(transform.position.x, sceneTopEdge, 0);
        }

    }

    void BulletFire()
    {
        GameObject bulletClone = Instantiate(bulletObject, new Vector3(bulletObject.transform.position.x, bulletObject.transform.position.y, 0f), bulletObject.transform.rotation);
        bulletClone.transform.localScale = new Vector3(0.3f, 0.7f, 1.5f);
        bulletClone.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Aestroid")
        {
            if (spaceShipHealth <= 0)
            {
                
            }
        }
    }
}
