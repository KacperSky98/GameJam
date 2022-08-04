using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreData
{
    public int collectedCoins;
    public int maxCoins;

    public HighScoreData(PlayerController player) {

        collectedCoins = player.collectedCoins;
        maxCoins = player.maxCoins;
    }
}
