using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeOut = 5f;

    public float Damage = 10f;

    public GameObject Explosion;

    public AudioSource audioSource;
    public AudioClip HitBoom;

    void FixedUpdate()
    {
        TimeOut -= Time.deltaTime;
        if(TimeOut < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(HitBoom);
            GameObject Effect = Instantiate(Explosion, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            Destroy(Effect, 2f);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage);
            Destroy(gameObject, 0.05f);
        }
    }
}
