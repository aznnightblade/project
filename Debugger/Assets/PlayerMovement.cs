using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	float Speed = 5;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate (){
		Vector3 pos = transform.localPosition;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		Vector3 target = hit.point;
		target.y = transform.localPosition.y;
		transform.LookAt (target);

		pos.x = pos.x + Input.GetAxisRaw ("Horizontal") * Speed * Time.deltaTime;
		pos.z = pos.z + Input.GetAxisRaw ("Vertical") * Speed * Time.deltaTime;

		transform.localPosition = pos;
	}
}
