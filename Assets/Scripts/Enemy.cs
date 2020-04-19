using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHealth = 50f;
    public float Health = 50f;
    float healthPercent;
    HealthBar bar;
    GameController controller;

    public float Damage = 10f;
    float TimeSinceAttack;
    public float AttacksPerSecond = 1;
    float AttackSpeed;

    public Transform Target;
    public float MoveSpeed = 0.5f;

    Rigidbody2D rb;
    public GameObject DeathExplosion;

    public AudioClip BiteSound;
    AudioSource audioSource;

    TurretController Player;

    public float WobbleAmount = 1f;
    public float WobbleSpeed = 1f;
    float WobbleTime;

    void Start()
    {
        Player = FindObjectOfType<TurretController>();
        Target = GameObject.FindGameObjectWithTag("Flower").transform;
        rb = GetComponent<Rigidbody2D>();
        bar = GetComponentInChildren<HealthBar>();
        Health = MaxHealth;
        AttackSpeed = AttacksPerSecond / 1;
        Physics2D.IgnoreLayerCollision(8, 8);
        controller = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Flower>())
        {
            TimeSinceAttack += Time.deltaTime;
            Flower kill = collision.gameObject.GetComponent<Flower>();
            if (TimeSinceAttack >= AttackSpeed)
            {
                audioSource.PlayOneShot(BiteSound);
                kill.TakeDamage(Damage);
                TimeSinceAttack = 0;
            }
        }
    }

    void FixedUpdate()
    {
        WobbleTime += Time.deltaTime * WobbleSpeed;
        Vector2 Wobble = new Vector2(Mathf.Sin(WobbleTime) * WobbleAmount / 8 * Mathf.PI, Mathf.Sin(WobbleTime) * WobbleAmount / 2 * Mathf.PI);

        Vector2 TargetPos2D = Target.position;
        Vector2 MoveDir = rb.position - TargetPos2D;
        MoveDir.Normalize();

        MoveDir += Wobble;
        rb.MovePosition(rb.position - MoveDir * MoveSpeed * Time.deltaTime);


        if (bar.BarSize <= 0 || Health < -15f)
        {
            GameObject Effect = Instantiate(DeathExplosion, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            Destroy(Effect, 2f);
            controller.EnemyDeathCouter();
            Player.GainEnergy();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        healthPercent = Health / MaxHealth;
        bar.SetSize(healthPercent);
    }
}
