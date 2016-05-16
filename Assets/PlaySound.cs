using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioClip DerbyCrash;
	public AudioClip ObstacleHit;
	public AudioClip CarExplode;
	public AudioClip EngineRun;
	public AudioClip ShieldHit;


	private AudioSource source;

	void Awake () {
		source = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "damaging") {
			if (this.gameObject.GetComponent<Player> ().shield == true)
				source.PlayOneShot (ShieldHit, 1F);
			else
				source.PlayOneShot (DerbyCrash, 1F);
			if (other.gameObject.GetComponent<Player> ().hp <= 0)
				source.PlayOneShot (CarExplode, 1F);
		}
		else if (other.gameObject.tag == "obstacle")
			source.PlayOneShot (ObstacleHit, 1F);
	}

}
