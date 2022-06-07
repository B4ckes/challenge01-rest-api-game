using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public int ScoreAmount = 0;
    public PlayerController PlayerController;
    public Text ScoreLabel;
    public GameObject GameOverScreen;

    private void Awake() {
        PlayerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void Score() {
        ScoreAmount++;
        ScoreLabel.text = $"Pontos: {ScoreAmount}";

        PlayerController.Speed += PlayerController.IncreaseSpeedAmount;
    }

    public void EndGame() {
        GameOverScreen.SetActive(true);
        GameOverScreen.GetComponent<GameOverController>().EndGame(ScoreAmount);

        StartCoroutine(PostScoreAmount());
    }

    IEnumerator PostScoreAmount()
    {
        ScoreAmountModel scoreAmount = new ScoreAmountModel
        {
            UserId = GlobalVars.Instance.currentUser.id,
            ScoreAmount = ScoreAmount,
        };

        string json = JsonUtility.ToJson(scoreAmount);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        UnityWebRequest request = new UnityWebRequest("http://localhost:5000/v1/scores", "POST");
        
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Pontuação enviada com sucesso!");
        }
    }
}
