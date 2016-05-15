using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public Player player;
	public float shield_time = 10;
	private float current_time;

	// Use this for initialization
	void Start () {
		current_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		current_time += Time.deltaTime;

		if (current_time >= shield_time) {
			player.shield = false;
			Destroy (this.gameObject);
		}
	}
}
