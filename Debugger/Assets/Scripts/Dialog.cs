using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    bool Dialoguebox = false;
	public GameObject Bubble;
	public GameObject text;
    public GameObject levelstage1text;
	// Use this for initialization
	void Start () {
      
        levelstage1text.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player" && Dialoguebox==false && GameManager.tutorial==false)
        {
            Dialoguebox = true;
			Bubble.SetActive(true);
			text.SetActive(true);
            levelstage1text.SetActive(false);
           
        }
        if (col.gameObject.tag == "Player" && Dialoguebox == false && GameManager.tutorial == true)
        {
            text.SetActive(false);
            Dialoguebox = true;
            Bubble.SetActive(true);
            levelstage1text.SetActive(true);
        }
       
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && GameManager.tutorial==false)
        {
            Dialoguebox = false;
			Bubble.SetActive(false);
			text.SetActive(false);
            levelstage1text.SetActive(false);
        }
        if (col.gameObject.tag == "Player" && GameManager.tutorial == true)
        {
           // stage1.SetActive(false);
           
            Bubble.SetActive(false);
            levelstage1text.SetActive(false);
            Dialoguebox = false;

        }
    }
}
