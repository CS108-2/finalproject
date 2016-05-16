using UnityEngine;
using System.Collections;

public class ShieldPickup : Powerup {

	public AudioClip ShieldOn;
	public GameObject shield;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// to be overridden by subclasses
	override protected void AddPowerup(Player player) {
		player.gameObject.GetComponent<AudioSource> ().PlayOneShot (ShieldOn, 1F);
		player.shield = true;
		Shield shield = (Shield)(Instantiate (this.shield, player.transform.position, player.transform.rotation) as GameObject).GetComponent<Shield>();
		shield.transform.parent = player.transform;
		shield.player = player;
	}
}
