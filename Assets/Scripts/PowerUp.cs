using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Health, Speed, DoubleShot }
    public PowerUpType powerUpType;

    public float duration = 5f;
public AudioClip powerUpSound;


void Start()
{
    Debug.Log("PowerUp scripti başladı: " + gameObject.name);
}

void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("PowerUp'a bir şey çarptı: " + other.name + ", Tag: " + other.tag);
    
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player çarptı! PowerUp aktif ediliyor...");
        
        PlayerPowerUp playerPU = other.GetComponent<PlayerPowerUp>();
        
        if (playerPU != null)
        {
if(powerUpSound != null){
AudioSource.PlayClipAtPoint(powerUpSound,transform.position);
 }
            Debug.Log("PlayerPowerUp scripti bulundu!");
            playerPU.ActivatePowerUp(powerUpType, duration);
        }
        else
        {
            Debug.Log("PlayerPowerUp scripti bulunamadı!");
        }
        
        Destroy(gameObject);
    }
    else
    {
        Debug.Log("Çarpan obje Player değil!");
    }
}
}

