using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	[SerializeField]
	int MainMenu = 0;

	Vector2 rectSize = new Vector2 (200, 40);

	bool paused = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;

			if (!paused)
				Time.timeScale = 1.0f;
			else
				Time.timeScale = 0.0f;
		}
	}

	void OnGUI()
	{
		if(paused) {
			GUILayout.BeginArea(new Rect(Screen.width / 2 - (rectSize.x / 2), Screen.height / 2 - (rectSize.y * 1.5f), 100, 200));
			GUILayout.Label("Paused", GUILayout.Width(200));
			if(GUILayout.Button ("Resume")) {
				paused = !paused;
				Time.timeScale = 1.0f;
			}
			if(GUILayout.Button ("Main Menu")) {
			   Application.LoadLevel(MainMenu);
			}
			if(GUILayout.Button ("Quit")) {
				Application.Quit();
			}
			GUILayout.EndArea();
		}
	}
}
