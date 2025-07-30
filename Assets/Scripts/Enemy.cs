using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movespeed = 2f;
    private Transform player;
    public int health = 2;
    public GameObject enemyBulletPrefab;
    public float fireRate = 1.5f;
    private float fireTimer;
    public Transform firePoint;
    public GameObject explosionPrefab; // Burada düzeltme yapıldı: gameObject -> GameObject

    public AudioClip explosionSound;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1f); // 1 saniye sonra patlama efektini yok et
            }

            ScoreManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        fireRate = Random.Range(0.3f, 0.8f);

        // İlk ateşi geciktirmemek için fireTimer'ı rastgele başlat
        fireTimer = Random.Range(0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        // Hedefe yönelme
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * movespeed * Time.deltaTime;

        // Ateş etme
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    void Fire()
    {
        int burstCount = Random.Range(1, 3); // 1 ile 2 mermi arası
        for (int i = 0; i < burstCount; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;
            bullet.GetComponent<Bullet>().SetDirection(direction);
        }
    }

    void ShootAtPlayer(Vector2 direction)
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1); // Can azalt
            Destroy(other.gameObject); // Mermiyi yok et
        }
    }
}