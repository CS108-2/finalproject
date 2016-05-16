using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {

	public GameObject[] powerups;
	private GameObject current_powerup = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPowerup() {
		current_powerup = (GameObject) Instantiate(powerups[Random.Range(0, powerups.Length)], this.transform.position, this.transform.rotation);
	}

	public bool HasPowerup() {
		if (current_powerup)
			return true;
		else
			return false;
	}
}
