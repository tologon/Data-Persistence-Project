using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public class HighScore
    {
        public string User { get; set; }
        public int Value { get; set; }
    }

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private string _currentUsername;
    [SerializeField] private string _highScoreKey;
    [SerializeField] private HighScore _highScore;
    private JsonSerializerSettings _jsonSettings;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _jsonSettings = new JsonSerializerSettings
        { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };

        _highScore = new HighScore();
        LoadHighScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void SetUsername()
    {
        _currentUsername = _inputField.text;
    }

    public string GetUsername()
    {
        return _currentUsername;
    }

    public void LoadHighScore()
    {
        string json = PlayerPrefs.GetString(_highScoreKey);
        _highScore = JsonConvert.DeserializeObject<HighScore>(json, _jsonSettings);
    }

    public void SaveHighScore()
    {
        string json = JsonConvert.SerializeObject(_highScore, _jsonSettings);
        PlayerPrefs.SetString(_highScoreKey, json);
        PlayerPrefs.Save();
    }

    public HighScore GetHighScore()
    {
        return _highScore;
    }

    public void SetHighScore(HighScore highScore)
    {
        _highScore = highScore;
    }
}
