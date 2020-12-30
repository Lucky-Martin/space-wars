using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] int healt = 50;
    [SerializeField] float speed = 11.5f;
    [SerializeField] TextMeshProUGUI healtText;

    [Header("Laser")]
    [SerializeField] float padding = 0.5f;
    [SerializeField] float laserFirePeriod = 1f;
    [SerializeField] float laserSpeed = 15f;
    [SerializeField] GameObject laserPrefab;

    [Header("Audio")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip fireSound;

    Coroutine fireCoroutine;
    AudioSource audioSource;
    SceneLoader sceneLoader;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        healtText.text = healt.ToString();
        audioSource = GetComponent<AudioSource>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Move();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine =  StartCoroutine(FireNow());
        } 
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireNow()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            audioSource.PlayOneShot(fireSound);
            yield return new WaitForSeconds(laserFirePeriod);
        }
    }

    private void Move()
    {
        //Input.getAxis("axis") returns a float between -1 and 1 
        //we multiply it by the time it took to render the last frame and multiply it by the preffered speed

        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void InitBounds()
    {
        //Get the game camera
        Camera gameCamera = Camera.main;

        //Calculate to bounds to word units
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer && damageDealer.GetAttackPlayer())
        {
            healt -= damageDealer.GetDamage();
            if (healt <= 0)
            {
                audioSource.PlayOneShot(deathSound);
                healtText.text = "You died!";
                Destroy(gameObject);
                sceneLoader.LoadGameOver();
            }
            else
            {
                healtText.text = healt.ToString();
            }
            damageDealer.Hit();
        }
    }
}
