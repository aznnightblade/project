using UnityEngine;
using System.Collections;

public class EnemyStatistics : MonoBehaviour {

	// Enemy Stats
	[SerializeField]
	int strength = 1;
	[SerializeField]
	int dexterity = 1;
	[SerializeField]
	int intelligence = 1;
	[SerializeField]
	int agility = 1;
	[SerializeField]
	int endurance = 1;
	[SerializeField]
	int luck = 1;
	[SerializeField]
	int moneyDropped = 100;
	[SerializeField]
	int experienceWorth = 10;
	[SerializeField]
	int maxHealth = 0;
	[SerializeField]
	int currHealth = 0;
	[SerializeField]
	int damage = 0;
	[SerializeField]
	int critChance = 0;
	[SerializeField]
	int defense = 0;

	// Use this for initialization
	void Start () {
		maxHealth = currHealth = 10 + 10 * endurance;
		damage = 5 + 3 * strength;
		critChance = 5 + 5 * luck;
		defense = 1 + 1 * endurance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Accessors
	public int Strength { get {return strength;} }
	public int Dexterity { get {return dexterity;} }
	public int Intelligence { get {return intelligence;} }
	public int Agility { get {return agility;} }
	public int Endurance { get {return endurance;} }
	public int Luck { get {return luck;} }
	public int MoneyDropped { get {return moneyDropped;} }
	public int ExperienceWorth { get { return experienceWorth; } }
	public int Health { get { return currHealth; } 
						set { currHealth = value; } }
	public int Damage { get { return damage; } }
}
