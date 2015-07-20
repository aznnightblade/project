using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	PlayerStatistics player;
	[SerializeField]
	Transform fireLocation = null;
	[SerializeField]
	Transform bullet = null;
	[SerializeField]
	Transform breakpoint = null;

	[SerializeField]
	float Speed = 5;
	float freezeTimer = 0.0f;

	[SerializeField]
	float ShootDelay = 0.25f;
	[SerializeField]
	float ReductionPerAgility = 0.005f;
	[SerializeField]
	float ReductionPerIntelligence = 0.075f;
	bool bulletFired = false;
	float fireTimer = 0.0f;

	[SerializeField]
	float ChargeSpeed = 0.1f;
	[SerializeField]
	float ChargeDelay = 0.25f;
	float ChargeTimer = 0.0f;
	float ChargeScale = 1.0f;

	bool breakpointFired = false;
	float breakpointCooldown = 0.0f;
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		gameObject.GetComponent<Rigidbody> ().maxAngularVelocity = Speed;
		player = gameObject.GetComponent<PlayerStatistics> ();
		bullet.tag = "Player Bullet";										// Tags bullets created by player as a player bullet.
	}
	
	// Update is called once per frame
	void Update () {
		// Checks to see if either the fire button has been pressed or the charge button was relased to fire a bullet
		if ((Input.GetButton ("Fire1") || Input.GetButtonUp("Fire2")) && fireTimer <= 0.0f) {
			fireTimer = ShootDelay - (ReductionPerAgility * player.Agility);
			bulletFired = true;
		}

		// Checks to see if we are charging a shot
		if (Input.GetButton("Fire2") && ChargeTimer <= 0.0f && !Input.GetButton("Fire1")) {
			if(ChargeScale < 2.0f) {
				ChargeScale += ChargeSpeed;
				ChargeTimer = ChargeDelay - (ReductionPerAgility * player.Agility);

				if(ChargeScale > 2.0f)
					ChargeScale = 2.0f;
			}
		}

		if (Input.GetButton ("Breakpoint") && breakpointCooldown <= 0.0f) {
			breakpointFired = true;
			breakpointCooldown = breakpoint.GetComponent<BreakpointScript>().ShotDelay - (player.Intelligence * ReductionPerIntelligence);
		}

		fireTimer -= Time.deltaTime;
		ChargeTimer -= Time.deltaTime;
		FreezeTimer -= Time.deltaTime;
		breakpointCooldown -= Time.deltaTime;
	}

	void FixedUpdate (){
		Vector3 pos = transform.position;

		if (FreezeTimer <= 0.0f) {
			// Creates a direction vector and then transforms it into a degrees rotation.
			Vector3 target = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
			target.Normalize();
			float rotation = (Mathf.Atan2(-target.y, target.x) * 180 / Mathf.PI) - 90;		// Need to remove 90 from the angle since Unity's forward
			transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));				// facing is 0 degrees

			// Moves player based on input
			pos.x = pos.x + Input.GetAxisRaw ("Horizontal") * Speed * Time.deltaTime;
			pos.z = pos.z + Input.GetAxisRaw ("Vertical") * Speed * Time.deltaTime;

			// Attempts to keep the player from getting moved around after a collision.
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

			// Checks to see if we queued up a bullet to be fired
			if (bulletFired == true)
				FireBullet ();

			// Checks to see if we queued up a breakpoint to be fired
			if (breakpointFired == true)
				FireBreakpoint();

			transform.position = pos;
		}
	}

	void FireBullet(){
		Vector3 pos = fireLocation.position;
		Vector3 offset = transform.rotation.eulerAngles;				// Gets back our current rotation as a vector
		Vector3 rot = transform.rotation.eulerAngles;					// Creates a rotation Quaternion based on the players rotation.
		rot.x = 90;														// Keeps the bullet rotated on the x axis properly

		// Resets our bullet size if we had scaled it up from a charge shot.
		if (bullet.transform.localScale.magnitude > 0.08f)
			bullet.transform.localScale = new Vector3 (0.2f, 0.2f, 0.0f);

		// Checks to see if we had charged up a shot
		if (ChargeScale > 1) {
			bullet.transform.localScale = bullet.transform.localScale * ChargeScale;
			ChargeScale = 1.0f;
		}

		// Calculates the bullet offset if we have more than one thread.
		if (gameObject.GetComponent<PlayerStatistics> ().NumThreads > 1) {
			offset.y = offset.y * (Mathf.PI / 180);
			offset = new Vector3(-Mathf.Cos(offset.y), 0, Mathf.Sin(offset.y));
			offset *= 0.05f;
		}

		// Creates bullets based on how many threads a player has
		switch (gameObject.GetComponent<PlayerStatistics> ().NumThreads) {
		case 1:
			Instantiate (bullet, pos, Quaternion.Euler (rot));		
			break;
		case 2:
			Instantiate (bullet, pos - offset, Quaternion.Euler (rot));
			Instantiate (bullet, pos + offset, Quaternion.Euler (rot));									
			break;
		case 3:
			Instantiate (bullet, pos - offset, Quaternion.Euler (rot.x, rot.y + 10, rot.z));		
			Instantiate (bullet, pos, Quaternion.Euler (rot));									
			Instantiate (bullet, pos + offset, Quaternion.Euler (rot.x, rot.y - 10, rot.z));	
			break;
		case 4:
			Instantiate (bullet, pos, Quaternion.Euler (rot.x, rot.y + 7, rot.z));		
			Instantiate (bullet, pos, Quaternion.Euler (rot.x, rot.y + 2.5f, rot.z));								
			Instantiate (bullet, pos, Quaternion.Euler (rot.x, rot.y - 2.5f, rot.z));	
			Instantiate (bullet, pos, Quaternion.Euler (rot.x, rot.y - 7, rot.z));		
			break;
		default:
			Instantiate (bullet, transform.localPosition, Quaternion.Euler (rot));
			break;
		}

		// Resets so we can fire again.
		bulletFired = false;
	}

	void FireBreakpoint() {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		pos.y = gameObject.transform.position.y;

		Instantiate (breakpoint, pos, Quaternion.identity);
		breakpointFired = false;
	}

	public float FreezeTimer{
		get { return freezeTimer; }
		set { freezeTimer = value; }
	}
}
