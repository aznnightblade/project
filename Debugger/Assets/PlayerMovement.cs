using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	float Speed;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate (){
		Vector3 pos = transform.localPosition;

		pos.x = pos.x + Input.GetAxisRaw ("Horizontal") * Speed * Time.deltaTime;
		pos.z = pos.z + Input.GetAxisRaw ("Vertical") * Speed * Time.deltaTime;

		transform.localPosition = pos;
	}
}
