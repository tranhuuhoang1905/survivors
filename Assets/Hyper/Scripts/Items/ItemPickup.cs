using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int scoreType = 1;
    [SerializeField] int pointsForCoinPickup = 5;
    bool wasCollected = false;
    bool isMovingToPlayer = false;
    protected Transform player;
    [SerializeField] protected float moveSpeed = 5f;

    void FixedUpdate()
    {
        if (isMovingToPlayer)
        {
            MoveToPlayer();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            isMovingToPlayer = true;
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CollectItem();
        }
    }
    private void MoveToPlayer()
    {
        Vector2 direction = (player.position - transform.position ).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    private void CollectItem()
    {
        wasCollected = true;
        ScoreEntry scoreEntry = new ScoreEntry(scoreType, pointsForCoinPickup);
        ScoreSignal.RaiseScore(scoreEntry); // 🔥 Gửi Signal khi quái chết
        // AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
