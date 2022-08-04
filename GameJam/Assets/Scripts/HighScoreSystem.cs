using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class HighScoreSystem
{
    public static void SaveHighScore(PlayerController player) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/highscore.high";
        FileStream stream = new FileStream(path, FileMode.Create);

        HighScoreData highscore = new HighScoreData(player);

        formatter.Serialize(stream, highscore);
        stream.Close();
    }

    public static HighScoreData LoadHighScore() { 
        string path= Application.persistentDataPath + "/highscore.high";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighScoreData data = formatter.Deserialize(stream) as HighScoreData;
            stream.Close();

            return data;
        }
        else {
            return null;
        }
    }
}
