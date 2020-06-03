using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{

    public Rigidbody playerRigidBody;
    private float verticalInput;

    [Range(10f, 250f)]
    public float playerSpeed;

    private Vector3 target;

    public GameObject bulletObject;
    public GameObject shieldObject;

    public Transform[] powerUpBulletPos;
    public PowerUpType _powerUpType;

    private void Start()
    {
        GameManager.Instance.spaceShipHitWarning.SetActive(false);
        shieldObject.SetActive(false);
        _powerUpType = PowerUpType.NONE;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            BulletFire();
        }
    }

    private void FixedUpdate()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        SpaceShipMovement();
        CheckPosition();
    }

    void SpaceShipMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0)
        {
            playerRigidBody.AddForce(new Vector3(target.x, target.y, 0) * playerSpeed);

            //playerRigidBody.velocity = (new Vector3(target.x, target.y, 0) * playerSpeed * Time.deltaTime);
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
        if (_powerUpType != PowerUpType.BULLETPOWER)
        {
            GameObject bulletClone = Instantiate(bulletObject, new Vector3(bulletObject.transform.position.x, bulletObject.transform.position.y, 0f), bulletObject.transform.rotation);
            bulletClone.transform.localScale = new Vector3(0.3f, 0.5f, 1.5f);
            bulletClone.SetActive(true);
        }
        else
        {
            for (int i = 0; i < powerUpBulletPos.Length; i++)
            {
                GameObject bulletClone = Instantiate(bulletObject, new Vector3(powerUpBulletPos[i].transform.position.x, powerUpBulletPos[i].transform.position.y, 0f), bulletObject.transform.rotation);
                bulletClone.transform.localScale = new Vector3(0.3f, 0.5f, 1.5f);
                bulletClone.SetActive(true);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!shieldObject.activeInHierarchy)
        {
            int playerScore = 0;
            switch (collision.transform.tag)
            {
                case "Aestroid":
                    if (_powerUpType != PowerUpType.NONE)
                    {
                        _powerUpType = PowerUpType.NONE;
                    }

                    StartCoroutine(ShowWarningImage());

                    GameManager.Instance.playerHealth = GameManager.Instance.playerHealth - 0.35f;
                    GameManager.Instance.healthBarImage.GetComponent<Image>().fillAmount = GameManager.Instance.playerHealth;
                    if (GameManager.Instance.playerHealth <= 0)
                    {
                        int.TryParse(GameManager.Instance.playerScore, out playerScore);

                        if (playerScore > GameManager.Instance.highScore)
                        {
                            PlayerPrefs.SetInt("Aestroids_HighScore", playerScore);
                        }

                        SceneManager.LoadScene(0);
                    }

                    break;

                case "SpecialAestroid":

                    int.TryParse(GameManager.Instance.playerScore, out playerScore);

                    if (playerScore > GameManager.Instance.highScore)
                    {
                        PlayerPrefs.SetInt("Aestroids_HighScore", playerScore);
                    }

                    SceneManager.LoadScene(0);
                    break;

                case "AIBullet":
                    if (_powerUpType != PowerUpType.NONE)
                    {
                        _powerUpType = PowerUpType.NONE;
                    }

                    StartCoroutine(ShowWarningImage());

                    GameManager.Instance.playerHealth = GameManager.Instance.playerHealth - 0.35f;
                    GameManager.Instance.healthBarImage.GetComponent<Image>().fillAmount = GameManager.Instance.playerHealth;
                    if (GameManager.Instance.playerHealth <= 0)
                    {
                        int.TryParse(GameManager.Instance.playerScore, out playerScore);

                        if (playerScore > GameManager.Instance.highScore)
                        {
                            PlayerPrefs.SetInt("Aestroids_HighScore", playerScore);
                        }

                        SceneManager.LoadScene(0);
                    }
                    break;


            }
        }
    }

    IEnumerator ShowWarningImage()
    {
        GameManager.Instance.spaceShipHitWarning.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.spaceShipHitWarning.SetActive(false);
    }

    public void ActivateShield()
    {
        shieldObject.SetActive(true);
        Invoke("DeactivateShield", 5f);
    }
    void DeactivateShield()
    {
        shieldObject.SetActive(false);
    }
}
