using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserScoreScreen : MonoBehaviour
{
    public Text RankingText;

    private void Start() {
        StartCoroutine(GetUserScores());
    }

    IEnumerator GetUserScores() {
        int userId = GlobalVars.Instance.currentUser.id;

        UnityWebRequest request = new UnityWebRequest($"http://localhost:5000/v1/users/{userId}/scores", "GET");

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string scoresJson = $"{{\"scores\": {request.downloadHandler.text} }}";

            ScoreListModel scores = JsonUtility.FromJson<ScoreListModel>(scoresJson);

            int position = 1;

            scores.scores.ForEach(score => {
                this.RankingText.text += $"# {position} | {score.username} | Pontos: {score.scoreAmount} \n";
                position++;
            });
        }
    }

    public void OnBackPress() {
        SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single);
    }
}
