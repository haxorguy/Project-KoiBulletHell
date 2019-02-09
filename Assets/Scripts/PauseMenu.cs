using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public bool paused = false;

	void Start () {
		
	}

	void Update () {

		// Pause function
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.P)) { // ESC or P
			paused = true;

			//Dim background

			//Create Textbox or UI with "- Paused -"

			//Resume button

			//Exit Application button?

			//Pause Sound
		}

	//	if(paused)
			//Pressing resume or ESC or P will continue

	}
}
