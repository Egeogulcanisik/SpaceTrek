using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private PlayerController playerController;

    private bool isDoubleShotActivate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    public void ActivatePowerUp(PowerUp.PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUp.PowerUpType.Health :
                playerHealth.TakeDamage(-1);
                break;
            
            case PowerUp.PowerUpType.Speed :
                StartCoroutine(SpeedBoost(duration));
                break;
            
            case PowerUp.PowerUpType.DoubleShot:
                StartCoroutine(DoubleShot(duration));
                break;
        }
    }

    private System.Collections.IEnumerator SpeedBoost(float duration)
    {
        playerController.moveSpeed *= 2;
        yield return new WaitForSeconds(duration);
        playerController.moveSpeed /= 2;
    }

    private System.Collections.IEnumerator DoubleShot(float duration)
    {
        isDoubleShotActivate = true;
        yield return new WaitForSeconds(duration);
        isDoubleShotActivate = false;
    }

    public bool IsDoubleShotActivate()
    {
        return isDoubleShotActivate;
    }

    }
