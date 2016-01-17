using UnityEngine;
using System.Collections;

public class Score : GameEvent {

    public int destroyedTiles;

    public class OnScoring : Score
    {
        public OnScoring(int destroyedTiles)
        {
            this.destroyedTiles = destroyedTiles;
        }
    }
}
