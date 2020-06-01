using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    private float verticalInput;

    [Range(0f, 5f)]
    public float playerSpeed;

    private Vector3 target;

    private void Update()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector3 difference = target - transform.GetChild(0).position;
        float rotationZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, rotationZ);

        SpaceShipMovement();
    }
    void SpaceShipMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0)
        {
            Vector2 mousePointerPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            playerRigidBody.AddForce(mousePointerPosition * playerSpeed);
        }
    }
}
