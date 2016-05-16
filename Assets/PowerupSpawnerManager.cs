using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawnerManager : MonoBehaviour {

	public GameObject[] powerup_spawners;
	public float spawn_time = 10;
	public float time_since_last_spawn;

	// Use this for initialization
	void Start () {
		time_since_last_spawn = 0;
	}

	// Update is called once per frame
	void Update () {
		time_since_last_spawn += Time.deltaTime;
		if (time_since_last_spawn >= spawn_time) {
			time_since_last_spawn = 0;
			List<GameObject> powerup_spawners_list = new List<GameObject> ();
			foreach (GameObject powerup_spawner in powerup_spawners)
				powerup_spawners_list.Add(powerup_spawner);
			Shuffle (powerup_spawners_list);
			foreach (GameObject powerup_spawner in powerup_spawners_list) {
				PowerupSpawner powerup_spawner_script = powerup_spawner.GetComponent<PowerupSpawner> ();
				if (!powerup_spawner_script.HasPowerup ()) {
					powerup_spawner_script.SpawnPowerup ();
					break;
				}
			}
//			current_powerup = (GameObject) Instantiate(powerups[Random.Range(0, powerups.Length)], this.transform.position, this.transform.rotation);
		}
	}

	private static void Shuffle<T>(IList<T> list)  
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = Random.Range(0, n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}
