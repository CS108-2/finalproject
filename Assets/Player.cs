using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public enum player_type {
		ai,
		player_1,
		player_2
	}

	public player_type player;

	public int turning_speed;

	public int speed;
	public float turning;
	public float velocity;
	public Vector2 movement;
	public int acceleration;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 0.1f;

	public int hp = 100;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		turning_speed = 20;
		speed = 100;

		turning = 0;
		velocity = 0;
		acceleration = 10;

		bullettime = 0;

		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
//		float turning_input = GetTurning ();
//		float velocity_input = GetVelocity ();

		// if velocity is too high, player must slow down
//		if (velocity > max_velocity)
//		if (velocity > 100)
//			velocity_input = 0;

//		if (velocity_input == 0)
//			velocity -= acceleration;

//		else
//			velocity += acceleration;
		
//		if (velocity > max_velocity)
//			velocity = max_velocity;

		bullettime += Time.deltaTime;

		if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY) {
			//I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
			Instantiate (this.bullet, this.transform.position + transform.up * 0.5f, this.transform.rotation);
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

		float turning_input = GetTurning ();
		float velocity_input = GetVelocity ();

		// if velocity is too high, player must slow down
		//		if (velocity > max_velocity)
//		if (velocity > 100)
//			velocity_input = 0;

//		if (velocity_input == 0)
//			velocity -= acceleration;
//			rbody.AddRelativeForce(transform.up * -acceleration);
//		else
//			velocity += acceleration;
		rbody.AddForce(transform.up * velocity_input * acceleration);
		if (rbody.velocity.sqrMagnitude > 5 * 5)
			rbody.velocity = rbody.velocity.normalized * 5;

		if (velocity_input >= 0)
			turning_input *= -1;

		rbody.AddTorque (turning_input * turning_speed * 0.01f);
//		transform.Rotate(0.0f, 0.0f, turning_input * Time.deltaTime * turning_speed);
//		rbody.velocity = this.transform.up * velocity * Time.deltaTime * speed;
	}
}
