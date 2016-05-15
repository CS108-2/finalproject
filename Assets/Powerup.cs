using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Player player = other.GetComponent<Player> ();
		if (player) {
			this.AddPowerup (player);
			Destroy (this.gameObject);
		}
	}

	// to be overridden by subclasses
	virtual protected void AddPowerup(Player player) {

	}
}
