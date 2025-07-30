using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle -90f);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        Destroy(gameObject, 2f);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer bu mermi düşmana aitse
        if (gameObject.CompareTag("EnemyBullet") && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
        // Eğer bu mermi oyuncuya aitse
        else if (gameObject.CompareTag("PlayerBullet") && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }


}



