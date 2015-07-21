using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	[SerializeField]
	GameObject Trigger1 = null;
	[SerializeField]
	GameObject Trigger2 = null;
	[SerializeField]
	GameObject Trigger3 = null;
	
	// Update is called once per frame
	void Update () {
		if (Trigger1 == null && Trigger2 == null && Trigger3 == null) {
			Destroy (gameObject);
		}
	}
}
