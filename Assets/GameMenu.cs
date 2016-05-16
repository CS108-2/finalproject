using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

	public Player[] players;
	public Text victory_text;

	private GameObject victory_menu;

	// Use this for initialization
	void Start () {
		victory_menu = GameObject.Find ("Victory Menu");
		victory_menu.SetActive (false);
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
	}

	public void PlayAgain() {
		SceneManager.LoadScene ("game");
	}

	public void ToMenu() {
		SceneManager.LoadScene ("mainmenu");
	}

	public void ShowVictoryMenu() {
		victory_menu.SetActive (true);
	}

	public void CloseVictoryMenu() {
		victory_menu.SetActive (false);
	}
}
