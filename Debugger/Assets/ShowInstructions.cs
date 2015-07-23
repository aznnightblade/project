using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShowInstructions : MonoBehaviour {
	public GameObject instructions;
	public Sprite open;
	public Sprite close;

	private Button button;

	public void Show(){
		Application.LoadLevel ("Instructions");
		
		
	}
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

	}
}
