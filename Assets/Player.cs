﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public enum player_type {
		ai,
		player_1,
		player_2
	}

	public player_type player;

	public int speed;
	public float xvelocity;
	public float yvelocity;
	public Vector2 movement;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 0.1f;

	public int hp = 100;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		speed = 100;

		xvelocity = 0;
		yvelocity = 0;

		bullettime = 0;

		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		xvelocity = GetTurning ();//Input.GetAxis ("Horizontal");
		yvelocity = GetVelocity ();//Input.GetAxis ("Vertical");

		bullettime += Time.deltaTime;

		if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY) {
			//I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
			Instantiate (this.bullet, this.transform.position + new Vector3(0.5f, 0, 0), this.transform.rotation);
			bullettime = 0;
		}
	}

	float GetTurning() {
		if (player == player_type.player_1)
			return Input.GetAxis ("Horizontal");
		else if (player == player_type.player_2)
			return Input.GetAxis ("Horizontal2");
		// or something like this, not yet sure how we should implement AI. Maybe as a separate class? 
//		else if (player == players.ai)
//			return AI.GetTurning()
		else return 0; // error
	}

	float GetVelocity() {
		if (player == player_type.player_1)
			return Input.GetAxis ("Vertical");
		else if (player == player_type.player_2)
			return Input.GetAxis ("Vertical2");
		// or something like this, not yet sure how we should implement AI. Maybe as a separate class? 
//		else if (player == players.ai)
//			return AI.GetTurning()
		else return 0; // error
	}

	void FixedUpdate() {
		rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
	}
}
