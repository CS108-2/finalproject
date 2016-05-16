using UnityEngine;
using System.Collections;

public class HealthPickup : Powerup {

	public AudioClip HealthUp;
	public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// to be overridden by subclasses
	override protected void AddPowerup(Player player) {
		player.gameObject.GetComponent<AudioSource> ().PlayOneShot (HealthUp, 1F);
		player.hp += health;
	}
}
