using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    private float verticalInput;

    [Range(10f, 15f)]
    public float playerSpeed;

    private Vector3 target;

    public GameObject bulletObject;

    private void Update()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector3 difference = target - transform.GetChild(0).position;
        float rotationZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, rotationZ);

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
        if (verticalInput > 0 )
        {
            Vector2 mousePointerPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            playerRigidBody.AddForce(mousePointerPosition * playerSpeed);
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

        if (transform.GetChild(0).position.x > sceneRightEdge)
        {
            transform.GetChild(0).position = new Vector2(sceneLeftEdge, transform.GetChild(0).position.y);
        }
        if (transform.GetChild(0).position.x < sceneLeftEdge)
        {
            transform.GetChild(0).position = new Vector2(sceneRightEdge, transform.GetChild(0).GetChild(0).position.y);
        }
        if (transform.GetChild(0).position.y > sceneTopEdge)
        {
            transform.GetChild(0).position = new Vector2(transform.GetChild(0).position.x, sceneBottomEdge);
        }
        if (transform.GetChild(0).position.y < sceneBottomEdge)
        {
            transform.GetChild(0).position = new Vector2(transform.GetChild(0).position.x, sceneTopEdge);
        }

    }

    void BulletFire()
    {
        GameObject bulletClone = Instantiate(bulletObject, new Vector2(bulletObject.transform.position.x, bulletObject.transform.position.y), bulletObject.transform.rotation);
        bulletClone.transform.localScale = new Vector3(0.1f, .5f, 0.1f);
        bulletClone.SetActive(true);
    }
}
