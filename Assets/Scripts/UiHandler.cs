using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _menuScoreText;
    [SerializeField] private TextMeshProUGUI _gameScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    [SerializeField] private TextMeshProUGUI _menuCoinsText;
    [SerializeField] private TextMeshProUGUI _gameCoinsText;
    [SerializeField] private TextMeshProUGUI _gameOverCoinsText;
    [SerializeField] private TextMeshProUGUI _newHighScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuPanelUpdate(GameManager.Instance.HighScore, GameManager.Instance.Coins);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MenuPanelUpdate(int score, int coins)
    {
        _menuScoreText.text = score.ToString();
        _menuCoinsText.text = coins.ToString();
    }

    public void ScoreUpdate(int score)
    {
        _gameScoreText.text= score.ToString();
    } 
    public void CoinsUpdate(int coins)
    {
        _gameCoinsText.text= coins.ToString();
    }
    public void StartGame()
    {
        GameManager.Instance.IsGameStarted = true;
        _menuPanel.SetActive(false);
        _gamePanel.SetActive(true);
    }
    public void GameOver(int score, int highScore,int coins)
    {
        if (score > highScore)
        {
            GameManager.Instance.HighScore = score;
        }
        _newHighScoreText.gameObject.SetActive(score > highScore);
        _gameOverScoreText.text = score.ToString();
        _gameOverCoinsText.text = coins.ToString();
        GameManager.Instance.Coins += coins;
        GameManager.Instance.GameplayCoins = 0;
        GameManager.Instance.IsGameStarted = false;
        _gamePanel.SetActive(false);
        _gameOverPanel.SetActive(true);
    }
    public void MenuEnter()
    {
        GameManager.Instance.IsGameStarted = false;
        MenuPanelUpdate(GameManager.Instance.HighScore, GameManager.Instance.Coins);
        _gameOverPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _menuPanel.SetActive(true);
        GameManager.Instance.ResetGame();
    }
}
