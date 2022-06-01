using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreenController : MonoBehaviour
{
    public Text WelcomeLabel;

    private void Awake() {
        WelcomeLabel.text = $"Olá, {GlobalVars.Instance.currentUser.username}.";
    }

    public void StartGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void GoToUserRankingScreen() {
        SceneManager.LoadScene("UserRankingScreen", LoadSceneMode.Single);
    }

    public void GoToRankingScreen() {
        SceneManager.LoadScene("RankingScreen", LoadSceneMode.Single);
    }

    public void GoToUserDataScreen() {
        SceneManager.LoadScene("UserDataScreen", LoadSceneMode.Single);
    }

    public void GoToLoginScreen() {
        SceneManager.LoadScene("LoginScreen", LoadSceneMode.Single);
    }
}
