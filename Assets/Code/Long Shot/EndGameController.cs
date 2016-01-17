using UnityEngine;
using System.Collections;

public class EndGameController : MHBaseClass {

    void Start()
    {
        eventBus.AddListener<EndGame.OnEndGame>(endGame);
    }

    private void endGame(EndGame.OnEndGame eventData)
    {
        Debug.Log("Ending the game");
        PlayerPrefs.SetInt("gameStatus", eventData.gameStatus ? 1 : 0);
        Application.LoadLevel("EndGameScene");
    }

    void OnDestroy()
    {
        eventBus.RemoveListener<EndGame.OnEndGame>(endGame);
    }
}
