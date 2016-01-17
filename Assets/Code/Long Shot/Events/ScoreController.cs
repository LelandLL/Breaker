using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MHBaseClass
{
    private int totalScore = 0;
    public int scoreTarget;
    public int baseTileScore;
    private bool canScore = true;

    // Use this for initialization
    void Start()
    {
        eventBus.AddListener<Timer.OnTimePassed>(stopScoring);
        eventBus.AddListener<Score.OnScoring>(addToScore);
        modifyScoreText();
    }

    private void addToScore(Score.OnScoring evendData)
    {
        if (canScore)
        {
            totalScore += evendData.destroyedTiles * baseTileScore;
            modifyScoreText();
        }
    }

    private void modifyScoreText()
    {
        GetComponent<Text>().text = totalScore.ToString() + "/" + scoreTarget.ToString();
    }

    private void stopScoring(Timer.OnTimePassed evendData)
    {
        Debug.Log("Will Stop scoring");
        canScore = false;
        eventBus.Publish(new EndGame.OnEndGame(totalScore >= scoreTarget));
    }

    void OnDestroy()
    {
        eventBus.RemoveListener<Timer.OnTimePassed>(stopScoring);
        eventBus.RemoveListener<Score.OnScoring>(addToScore);
    }

}