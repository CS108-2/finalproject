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
	public float MAX_VELOCITY;
	public Vector2 movement;
	public int acceleration;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 0.1f;

	public int hp = 100;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		turning_speed = 120;
		speed = 100;

		turning = 0;
		velocity = 0;
		MAX_VELOCITY = 2f;
		acceleration = 10;

		bullettime = 0;

		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		bullettime += Time.deltaTime;

		// if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY) {
		// 	//I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
		// 	Instantiate (this.bullet, this.transform.position + transform.up * 0.5f, this.transform.rotation);
		// 	bullettime = 0;
		// }
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

		// add force forward / backwards
		rbody.AddForce(transform.up * velocity_input * acceleration);
		if (rbody.velocity.sqrMagnitude > MAX_VELOCITY * MAX_VELOCITY)
			rbody.velocity = rbody.velocity.normalized * MAX_VELOCITY;

		// check if input needs to be reversed
		if (velocity_input >= 0)
			turning_input *= -1;

		// add torque to turn left / right
		rbody.AddTorque (turning_input * turning_speed * 0.01f);

		if (this.hp <= 0)
			Destroy (this.gameObject);
		// old code for reference, will remove later
//		transform.Rotate(0.0f, 0.0f, turning_input * Time.deltaTime * turning_speed);
//		rbody.velocity = this.transform.up * velocity * Time.deltaTime * speed;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "damaging"){
			int damageGiven;
			damageGiven = (int)Mathf.Round (rbody.velocity.magnitude / MAX_VELOCITY * 20);
			other.gameObject.GetComponent<Player> ().hp -= damageGiven;
		}
	}
}
