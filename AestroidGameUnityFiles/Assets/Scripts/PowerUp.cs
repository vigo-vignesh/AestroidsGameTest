using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public PowerUpType _powerUpType;

    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            switch (_powerUpType)
            {
                case PowerUpType.SHIELD:
                  GameManager.Instance.currentPlayerObject.transform.GetComponent<SpaceShipController>().ActivateShield();
                    break;

                case PowerUpType.HEALTH:
                    if (GameManager.Instance.playerHealth < 1f)
                    {
                        GameManager.Instance.playerHealth = 1f;
                        GameManager.Instance.healthBarImage.GetComponent<Image>().fillAmount = GameManager.Instance.playerHealth;
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
