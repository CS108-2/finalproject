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
	public int acceleration;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 0.1f;

	public int hp = 100;
	public bool shield = false;

	public const float MAX_BOOST = 100f;
	public float boost = MAX_BOOST;

	private Rigidbody2D rbody;


	private GameObject movement_particles;
	private GameObject collision_particles;
	private ParticleSystem movement_particle_system;
	private ParticleSystem collision_particle_system;
	public int COLLISION_PARTICLE_COUNT;
	
	private Camera camera;

	private const int HEALTHBAR_WIDTH = 30;
	private const int HEALTHBAR_HEIGHT = 5;
	private const int HEALTHBAR_YOFFSET = 30;

	public GUISkin healthbar_skin;
	public GUISkin boostbar_skin;

	// Use this for initialization
	void Start () {
		turning_speed = 220;
//		speed = 100;

		turning = 0;
		velocity = 0;
		MAX_VELOCITY = 2f;
		acceleration = 45;

		bullettime = 0;

		rbody = gameObject.GetComponent<Rigidbody2D> ();

		foreach (Transform t in transform) {
			switch (t.name) {
				case "movement particles":
					movement_particles = t.gameObject;
					break;
				case "collision particles":
					collision_particles = t.gameObject;
					break;
			}
		}

		movement_particle_system = (ParticleSystem)movement_particles.GetComponentInChildren<ParticleSystem> ();
		collision_particle_system = (ParticleSystem)collision_particles.GetComponentInChildren<ParticleSystem> ();

		camera = Camera.main;
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
//			return AI.GetVelocity()
		else return 0; // error
	}

	float GetBoost() {
		if (player == player_type.player_1)
			return Input.GetAxis ("Boost");
		if (player == player_type.player_2)
			return Input.GetAxis ("Boost2");
		// or something like this, not yet sure how we should implement AI. Maybe as a separate class?
//		else if (player == players.ai)
//			return AI.GetBoost()
		else return 0; // error
	}

	void FixedUpdate() {
		float turning_input = GetTurning ();
		float velocity_input = GetVelocity ();
		float boost_input = GetBoost ();

		// increase boost meter
		boost += 10 * Time.deltaTime;
		if (boost >= MAX_BOOST)
			boost = MAX_BOOST;

		// define boost multiplier
		float boost_mult = 1f;

		if (boost >= 0 && boost_input > 0) {
			boost -= 40 * Time.deltaTime;
			boost_mult = 2f;
		}

		// define damage multiplier
		float damage_mult = 1f;

		if (hp <= 100)
			damage_mult = .5f + (hp / 100f * .5f);

		// add force forward / backwards
		rbody.AddForce(transform.up * velocity_input * acceleration);
		if (rbody.velocity.sqrMagnitude > MAX_VELOCITY * MAX_VELOCITY)
			rbody.velocity = rbody.velocity.normalized * MAX_VELOCITY * boost_mult * damage_mult;
		
		// check if input needs to be reversed
		if (velocity_input >= 0)
			turning_input *= -1;

		if (rbody.velocity.magnitude != 0)
			movement_particles.SetActive (true);
		else
			movement_particles.SetActive (false);

		movement_particle_system.startSpeed = rbody.velocity.magnitude / MAX_VELOCITY * 20;

		// add torque to turn left / right
		rbody.AddTorque (turning_input * turning_speed * 0.01f);

		if (this.hp <= 0)
			gameObject.SetActive (false);
		// old code for reference, will remove later
//		transform.Rotate(0.0f, 0.0f, turning_input * Time.deltaTime * turning_speed);
//		rbody.velocity = this.transform.up * velocity * Time.deltaTime * speed;
	}

	void OnGUI() {
		// health bar
		GUI.skin = healthbar_skin;
		Vector3 healthbar_position = camera.WorldToScreenPoint (transform.position);
		GUI.HorizontalScrollbar(new Rect (healthbar_position.x - (HEALTHBAR_WIDTH / 2), Screen.height - healthbar_position.y - HEALTHBAR_YOFFSET, HEALTHBAR_WIDTH, HEALTHBAR_HEIGHT), 0, hp, 0, 100, "horizontalscrollbar");

		// boost bar
		GUI.skin = boostbar_skin;
		Vector3 boostbar_position = camera.WorldToScreenPoint (transform.position);
		GUI.HorizontalScrollbar(new Rect (boostbar_position.x - (HEALTHBAR_WIDTH / 2), Screen.height - boostbar_position.y - HEALTHBAR_YOFFSET + HEALTHBAR_HEIGHT, HEALTHBAR_WIDTH, HEALTHBAR_HEIGHT), 0, boost, 0, 100, "horizontalscrollbar");
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "damaging"){
			int damageTaken = 0;
			if (this.GetComponent<EdgeCollider2D> ().IsTouching (other.gameObject.GetComponent<BoxCollider2D> ())) {
				collision_particle_system.Emit (COLLISION_PARTICLE_COUNT);
				Player player = other.gameObject.GetComponent<Player> ();
				if (player && !player.shield) {
					damageTaken = (int)Mathf.Round (other.relativeVelocity.magnitude / (2 * MAX_VELOCITY) * 5);
				}
			}
			other.gameObject.GetComponent<Player>().hp -= damageTaken;
			rbody.AddForce (-(other.rigidbody.position - rbody.position) * (other.relativeVelocity.magnitude*40));
		}
		else if (other.gameObject.tag == "obstacle") {
			int damageTaken = 0;
			damageTaken = (int)Mathf.Round ((rbody.velocity.magnitude / (2*MAX_VELOCITY))* 3);
			hp -= damageTaken;
		}
	}
}
