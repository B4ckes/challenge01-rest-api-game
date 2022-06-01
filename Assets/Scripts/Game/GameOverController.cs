using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Text ScoreLabel;

    public void EndGame(int scoreAmount) {
        ScoreLabel.text = $"Você fez {scoreAmount} pontos!";
    }

    public void OnTryAgainPress() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void OnExitPress() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single);
    }
}
