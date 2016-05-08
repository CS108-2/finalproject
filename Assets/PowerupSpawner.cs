using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {

	public GameObject[] powerups;
	public float spawn_time;
	public float time_since_last_spawn;
//	public float spawned_item_taken = true;
	private GameObject current_powerup = null;

	// Use this for initialization
	void Start () {
		time_since_last_spawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!current_powerup) {
			time_since_last_spawn += Time.deltaTime;
		} else {
			return;
		}

		if (time_since_last_spawn >= spawn_time) {
			time_since_last_spawn = 0;
			current_powerup = (GameObject) Instantiate(powerups[Random.Range(0, powerups.Length - 1)], this.transform.position, this.transform.rotation);
		}
	}
}
