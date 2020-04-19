using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float MinLook = -80f;
    public float MaxLook = -170f;

    Rigidbody2D rb;
    Camera Cam;
    Vector2 mousePos;
    HealthBar bar;
    GameObject SaveTarget;
    Flower flower;

    AudioSource audioSource;
    public AudioClip ShootSound;
    public AudioClip RechargeSound;

    public float Health;
    public float MaxHealth = 100f;
    public float HealthRecharge = 10f;
    public float HealthDischarge = 5f;
    float HealthPercent;
    public float BulletNeed = 5f;
    bool Recharging;

    public Transform FirePoint;
    public GameObject BulletPrefab;

    public float BulletForce = 25f;


    public float ShotsPerSecond = 3f;
    float FireRate;
    float TimeSinceShot = 0f;
    bool CanFire;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cam = Camera.main;
        bar = gameObject.transform.parent.GetComponentInChildren<HealthBar>();
        Health = MaxHealth;
        SaveTarget = GameObject.FindGameObjectWithTag("FlowerControl");
        flower = SaveTarget.GetComponent<Flower>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        FireRate = 1 / ShotsPerSecond;

        mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1") && TimeSinceShot >= FireRate&& CanFire && Time.timeScale > 0)
        {
            Shoot();
            TimeSinceShot = 0f;
        }

        if (Input.GetButton("Fire2") && Time.timeScale > 0)
        {
            audioSource.loop = true;
            Health += HealthRecharge * Time.deltaTime;
            Recharging = true;
            audioSource.UnPause();
        }
        else if (!Input.GetButton("Fire2") && Time.timeScale > 0)
        {
            audioSource.loop = false;
            Health -= HealthDischarge * Time.deltaTime;
            Recharging = false;
        }

        if(Input.GetButtonDown("Fire2") && Time.timeScale > 0)
        {
            audioSource.PlayOneShot(RechargeSound);
        }
        if(Input.GetButtonUp("Fire2"))
        {
            audioSource.Stop();
        }

        if (Time.timeScale == 0)
        {
            audioSource.Pause();
        }
    }

    void FixedUpdate()
    {
        TimeSinceShot += Time.fixedDeltaTime;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        angle = Mathf.Clamp(angle, MaxLook, MinLook);
        rb.rotation = angle;

        if(Health > 0 && !Recharging && flower.Health > 0)
        {
            CanFire = true;
        }
        else
        {
            CanFire = false;
        }

        Health = Mathf.Clamp(Health, -5f, MaxHealth);

        HealthPercent = Health / MaxHealth;
        bar.SetSize(HealthPercent);
    }

    void Shoot()
    {
        audioSource.PlayOneShot(ShootSound);
        GameObject Bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D BulletRB = Bullet.GetComponent<Rigidbody2D>();
        BulletRB.AddForce(FirePoint.right * BulletForce, ForceMode2D.Impulse);
        Health -= BulletNeed;
    }
    public void GainEnergy()
    {
        Health += 5;
    }
}
