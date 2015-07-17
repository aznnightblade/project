﻿using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	[SerializeField]
	Transform target = null;

	[SerializeField]
	float distance = 10;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.position.x, target.position.y + distance, target.position.z);
	}
}
