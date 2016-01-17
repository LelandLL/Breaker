using UnityEngine;
using System.Collections;

public class ForMainMenu : MonoBehaviour {

	public void ArcadeGame()
    {
        Application.LoadLevel("Arcade");
    }
    public void LongShot()
    {
        Application.LoadLevel("Long Shot");
    }
    public void EndlessGame()
    {
        Application.LoadLevel("Endless");
    }
    public void Quit()
    {
        Application.LoadLevel("Intro");
    }
}
