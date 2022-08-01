using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCatchController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Kolizja");
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            player.collectedCoins++;
            if (!player.duringBoost)
            {
                player.currentCoinsForUpgrade--;
                if (player.currentCoinsForUpgrade == 0)
                {
                    Debug.Log("Dostajesz boosta");
                    player.SetBoost();
                    player.currentCoinsForUpgrade = player.coinsForUpgrade;
                }
            }
        }
    }
}
