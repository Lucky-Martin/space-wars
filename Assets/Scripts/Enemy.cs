using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] int healt = 100;

    [Header("Laser")]
    [SerializeField] GameObject laserBullet;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] float bulletSpeed = 6f;

    [Header("Audio")]
    [SerializeField] AudioClip deathSound;

    float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            GameObject laser = Instantiate(laserBullet, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer && !damageDealer.GetAttackPlayer())
        {
            healt -= damageDealer.GetDamage();
            if (healt <= 0)
            {
                //Particle animation
                Destroy(gameObject);
                GameObject expolision = Instantiate(deathVFX, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
                Destroy(expolision, 1f);
            }
            damageDealer.Hit();
        }
    }
}
