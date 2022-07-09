using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float speed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Player Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float FiringPeriod = 0.1f;
    float xmin, xmax,ymin,ymax;

    Coroutine firingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        SetBoundaries();
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position,deathSoundVolume);
    }

    public int GetHealth()
    {
        return health;
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine=StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    private void projectileFiring()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            projectileFiring();
            yield return new WaitForSeconds(FiringPeriod);
        }
    }
    private void Move()
    {
        var delx = Input.GetAxis("Horizontal")*Time.deltaTime*speed;
        var dely = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        var cx = Mathf.Clamp(transform.position.x + delx,xmin,xmax);
        var cy = Mathf.Clamp(transform.position.y + dely,ymin,ymax);
        transform.position = new Vector2(cx,cy);
    }

    private void SetBoundaries()
    {
        Camera gameCamera = Camera.main;
        xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    
}
