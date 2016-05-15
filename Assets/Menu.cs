using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	private GameObject menu;
	private GameObject controls;

	// Use this for initialization
	void Start () {
		menu = GameObject.Find ("Menu");
		controls = GameObject.Find ("Controls");
		menu.SetActive (true);
		controls.SetActive (false);
	}

	public void Play() {
		SceneManager.LoadScene ("game");
	}

	public void Quit() {
		Application.Quit ();
	}

	public void showMenu() {
		menu.SetActive (true);
		controls.SetActive (false);
	}

	public void showControls() {
		menu.SetActive (false);
		controls.SetActive (true);
	}
}
