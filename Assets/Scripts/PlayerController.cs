using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 moveInput;
    private Vector2 mousePos;

    public GameObject bulletPrefab;
    public Transform firePoint; // Bu karakterin silah ucu olacak
    public Camera cam;

public AudioClip shootSound;
private AudioSource audioSource;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal") ,Input.GetAxisRaw("Vertical")).normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (float.IsInfinity(mousePos.x) || float.IsInfinity(mousePos.y))
            return;


        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = moveInput * moveSpeed;
        Vector2 aimDir = mousePos - _rb.position;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (mousePos - _rb.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(direction);
        
        PlayerPowerUp powerUp = GetComponent<PlayerPowerUp>();
        if (powerUp != null && powerUp.IsDoubleShotActivate())
        {
            float spreadAngle = 15f;
            Quaternion angleOffset = Quaternion.Euler(0,0,spreadAngle);
            Vector2 spreadDir = angleOffset * direction;
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet2.GetComponent<Bullet>().SetDirection(spreadDir);
        }
        if(shootSound != null){
audioSource.PlayOneShot(shootSound);
    }
}
}
