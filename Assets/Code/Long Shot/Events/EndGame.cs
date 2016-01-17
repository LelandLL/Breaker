using UnityEngine;
using System.Collections;

public class EndGame : GameEvent{

    public bool gameStatus;

    public class OnEndGame : EndGame
    {
        public OnEndGame(bool gameStatus)
        {
            this.gameStatus = gameStatus;
        }
    }
}
