using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Script : MonoBehaviour
{
    public int bulletPower = 5;

    [SerializeField]
    [Range(0, 100)]
    private float bulletSpeed;
    [SerializeField]
    [Range(0, 1)]
    private float bulletDestroyTime;
    private void Start()
    {
        Destroy(gameObject, bulletDestroyTime);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.transform.name);
    //}
}
