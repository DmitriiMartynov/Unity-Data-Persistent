using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[DefaultExecutionOrder(-1)]
public class Persistent : MonoBehaviour
{
    public static Persistent Instance {get; private set;}

    [System.Serializable]
    struct Player
    {
        public string name;
        public int bestScore;
    };

    Player currentPlayer;
    Player bestPlayer;

    string filename;
 
    void Awake()
    {
       if (Instance != null)
       {
            Destroy(gameObject);
            return;
       }


       Instance = this;

       filename = Application.persistentDataPath + "/BestPlayer.json";
       LoadBestScore();

       DontDestroyOnLoad(gameObject);
    }

    public void LoadBestScore()
    {
        if (File.Exists(filename))
        {
            string jsonString = File.ReadAllText(filename);
            bestPlayer = JsonUtility.FromJson<Player>(jsonString);
        }
    }

    public void SaveBestScore(int score)
    {
        if (score > currentPlayer.bestScore)
        {
            currentPlayer.bestScore = score;
        }

        if (currentPlayer.bestScore > bestPlayer.bestScore)
        {
            bestPlayer = currentPlayer;

            string jsonString = JsonUtility.ToJson(bestPlayer);
            File.WriteAllText(filename, jsonString);
        }
    }

    public void InitCurrentPlayer(string name)
    {
        currentPlayer.name = name;
        currentPlayer.bestScore = 0;
        if (bestPlayer.name == name)
        {
            currentPlayer.bestScore = bestPlayer.bestScore;
        }
    }

    public string GetDecsription(bool isCurrent)
    {
        Player player;

        if (isCurrent)
        {
            player = currentPlayer;
        }
        else
        {
            player = bestPlayer;
        }
        return "Best Score: " + player.name + " : " + player.bestScore;
    }
}
