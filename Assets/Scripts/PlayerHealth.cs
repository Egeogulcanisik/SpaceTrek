using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Game Over’da sahneyi yeniden yüklemek için

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        // Eğer UIManager sahnede yoksa hata almamak için kontrol ekledik
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(currentHealth);
    }

    // Hasar alma
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(currentHealth);

        // Kamera titretme
        ScreenShake shake = FindObjectOfType<ScreenShake>();
        if (shake != null)
            StartCoroutine(shake.shake(0.2f, 0.2f));

        if (currentHealth <= 0)
        {
            Die();
        }
    }



    // Can yenileme (Power-Up kullanımı)
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(currentHealth);
    }

    void Die()
    {
        Time.timeScale = 0f;

        if (UIManager.Instance != null)
            UIManager.Instance.ShowGameOver();
    }
}

