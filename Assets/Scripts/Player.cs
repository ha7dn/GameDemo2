using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;
    float sfxVolume = 0.7f;
    float laserFiringPeriod = 0.1f;
    float laserSpeed = 10f;

    float moveSpeed = 7f;
    float padding = 0.2f;
    int health = 100;


    float xMin;
    float xMax;
    float yMin;
    float yMax;


    Coroutine firingCoroutine;



    void Start()
    {
        SetMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        DamageDealer damageDealer = collisionObject.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        Destroy(gameObject, 1f);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, sfxVolume);

        FindObjectOfType<SceneLoader>().LoadGameOver();
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(ContinuousFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator ContinuousFire()
    {
        while (true)
        {
            GameObject laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
                       laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);


        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

   
}
