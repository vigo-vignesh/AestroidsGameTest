using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType _powerUpType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            switch (_powerUpType)
            {
                case PowerUpType.SHIELD:

                    break;

                case PowerUpType.AREABLAST:
                    for (int i = 0; i < GameManager.Instance.aestroidsContainer.transform.childCount; i++)
                    {
                       // GameManager.Instance.aestroidsContainer.transform.GetChild(i).
                    }
                    break;

                case PowerUpType.BULLETPOWER:
                    GameManager.Instance.currentPlayerObject.transform.GetComponent<SpaceShipController>()._powerUpType = PowerUpType.BULLETPOWER;
                    break;

            }
        }
        Destroy(this.gameObject);
    }

}
