using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 5;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            ScoreSignal.RaiseScore(pointsForCoinPickup); // 🔥 Gửi Signal khi quái chết
            // AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
