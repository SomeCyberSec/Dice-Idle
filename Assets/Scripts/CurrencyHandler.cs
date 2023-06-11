using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CurrencyHandler : MonoBehaviour
{
    public static CurrencyHandler instance;
    public int score = 0;

    private TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("More than one CurrencyHandler in the scene!");
        }
    }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        LoadScore();
        scoreText.SetText("Score: " + score);
    }

    public void scoreIncrease(int scoreAdd, int multiplier)
    {
        score += scoreAdd * multiplier;
        scoreText.SetText("Score: " + score);
        SaveScore();
    }

    public void scoreDecrease(int scoreDecrease)
    {
        if (score >= scoreDecrease)
        {
            score -= scoreDecrease;
            scoreText.SetText("Score: " + score);
            SaveScore();
        }
    }

    private string EncryptDecrypt(string str, int key)
    {
        char[] buffer = str.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (char)(buffer[i] ^ key);
        }
        return new string(buffer);
    }

    private void SaveScore()
    {
        ScoreData data = new ScoreData { score = this.score };

        string json = JsonUtility.ToJson(data);
        json = EncryptDecrypt(json, 123); // Encrypt the data
        string path = Application.persistentDataPath + "/scoreData.json";
        File.WriteAllText(path, json);
    }

    private void LoadScore()
    {
        string path = Application.persistentDataPath + "/scoreData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            json = EncryptDecrypt(json, 123); // Decrypt the data
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            this.score = data.score;
        }
    }
}