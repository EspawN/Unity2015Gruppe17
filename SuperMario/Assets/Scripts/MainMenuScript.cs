using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// Audio Variables
	public AudioClip MainMenuMusic;


	void Awake () {

	}
	
	public void OnePlayer () {
		Application.LoadLevel("Level1");
	}
		
	public void HiScore () {
		Application.LoadLevel("HighScores");
	}

	public void Back () {

		Application.LoadLevel ("MainMenu");
	}

	public void Exit (){
		Application.Quit ();
	}
}
