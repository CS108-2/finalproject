using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

	public Player[] players;
	public Text victory_text;

	private GameObject victory_menu;
	private GameObject pause_menu;
	private GameObject controls_menu;

	// Use this for initialization
	void Start () {
		victory_menu = GameObject.Find ("Victory Menu");
		pause_menu = GameObject.Find ("Pause Menu");
		controls_menu = GameObject.Find ("Controls Menu");
		victory_menu.SetActive (false);
		pause_menu.SetActive (false);
		controls_menu.SetActive (false);
	}

	void Update() {
		if (!players [0].gameObject.activeSelf && !players [1].gameObject.activeSelf) {
			victory_text.text = "It was a tie!";
			victory_menu.SetActive (true);
		}
		else if (!players [0].gameObject.activeSelf) {
			victory_text.text = "Player 1 wins!";
			victory_menu.SetActive (true);
		}
		else if (!players [1].gameObject.activeSelf) {
			victory_text.text = "Player 2 wins!";
			victory_menu.SetActive (true);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Time.timeScale = 0;
			pause_menu.SetActive (true);
		}
	}

	public void PlayAgain() {
		SceneManager.LoadScene ("game");
	}

	public void ToMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("mainmenu");
	}

	public void ShowVictoryMenu() {
		victory_menu.SetActive (true);
	}

	public void CloseVictoryMenu() {
		victory_menu.SetActive (false);
	}

	public void ShowPauseMenu() {
		pause_menu.SetActive (true);
		controls_menu.SetActive (false);
	}

	public void ShowControlsMenu() {
		controls_menu.SetActive (true);
		pause_menu.SetActive (false);
	}

	public void Resume() {
		pause_menu.SetActive (false);
		Time.timeScale = 1;
	}
}
