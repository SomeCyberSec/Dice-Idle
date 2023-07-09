using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Firebase.Extensions;
using Firebase.Firestore;

public class CurrencyHandler : MonoBehaviour
{
    public static CurrencyHandler instance;
    public int score = 0;
    
    private FirestoreDataHandler firestoreScore;
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        // Grab instance reference to database
        firestoreScore = FirestoreDataHandler.Instance;

        // Set "instance" to singleton class, CurrencyHandler (this class)
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("More than one CurrencyHandler in the scene!");
        }
    }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        // Load current JSON score
        //LoadScore();

        scoreText.SetText("Score: " + UserData.Instance.Score);
    }

    public async void scoreIncrease(int scoreAdd, int multiplier)
    {
        UserData.Instance.Score += scoreAdd * multiplier;
        scoreText.SetText("Score: " + UserData.Instance.Score);

        // Save score to JSON
        //SaveScore();
        
        // Update database with new score
        await firestoreScore.UpdateScore(UserData.Instance.Score);
    }

    public async void scoreDecrease(int scoreDecrease)
    {
        if (UserData.Instance.Score >= scoreDecrease)
        {
            UserData.Instance.Score -= scoreDecrease;
            scoreText.SetText("Score: " + UserData.Instance.Score);
            //Save score to JSON
            //SaveScore();

            // Update database with new score
            await firestoreScore.UpdateScore(UserData.Instance.Score);
        }
    }

    // JSON implementation (interferes with Firestore implementation atm)
    /*
    // Encryption/Decryption function for JSON
    private string EncryptDecrypt(string str, int key)
    {
        char[] buffer = str.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (char)(buffer[i] ^ key);
        }
        return new string(buffer);
    }

    // JSON score saving function
    private void SaveScore()
    {
        ScoreData data = new ScoreData { score = this.score };

        string json = JsonUtility.ToJson(data);
        json = EncryptDecrypt(json, 123); // Encrypt the data
        string path = Application.persistentDataPath + "/scoreData.json";
        File.WriteAllText(path, json);
    }

    // JSON score loading function
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
    */
}