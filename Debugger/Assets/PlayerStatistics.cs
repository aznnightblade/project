using UnityEngine;
using System.Collections;

public class PlayerStatistics : MonoBehaviour {

	// Players Stats
	[SerializeField]
	int strength = 1;
	[SerializeField]
	int dexterity = 1;
	[SerializeField]
	int intelligence = 1;
	[SerializeField]
	int endurance = 1;
	[SerializeField]
	int luck = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Accessors
	public int Strength { get { return strength; } }
	public int Dexterity { get { return dexterity; } }
	public int Intelligence { get { return intelligence; } }
	public int Endurance { get { return endurance; } }
	public int Luck { get { return luck; } }
}
