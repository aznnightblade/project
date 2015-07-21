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
	int agility = 1;
	[SerializeField]
	int endurance = 1;
	[SerializeField]
	int luck = 1;

	[SerializeField]
	int health = 0;
	[SerializeField]
	int maxHealth = 0;
	[SerializeField]
	int initialHealth = 10;
	[SerializeField]
	int healthPerEndurance = 10;
	[SerializeField]
	int damage = 0;
	[SerializeField]
	float chargedDamageScale = 1.5f;
	[SerializeField]
	int initialDamage = 5;
	[SerializeField]
	int damagePerStrength = 3;
	[SerializeField]
	int defense = 0;
	[SerializeField]
	int initialDefense = 1;
	[SerializeField]
	int defensePerEndurance = 1;
	[SerializeField]
	int critChance = 0;
	[SerializeField]
	int initialCrit = 5;
	[SerializeField]
	int critPerLuck = 5;

	[SerializeField]
	int numThreads = 1;

	[SerializeField]
	int money = 0;
	[SerializeField]
	int experience = 0;

	// Use this for initialization
	void Start () {
		StatsUpdate ();
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0){
			Application.LoadLevel("HudWorld");
		}
	}

	// Call this once a stat has been increased to recalculate the players values
	public void StatsUpdate(){
		maxHealth = health = initialHealth + healthPerEndurance * endurance;
		defense = initialDefense + defensePerEndurance * endurance;
		damage = initialDamage + damagePerStrength * strength;
		critChance = initialCrit + critPerLuck * luck;
	}

	// Accessors
	public int Strength { get { return strength; } }
	public int Dexterity { get { return dexterity; } }
	public int Intelligence { get { return intelligence; } }
	public int Agility { get { return agility; } }
	public int Endurance { get { return endurance; } }
	public int Luck { get { return luck; } }
	public int NumThreads { get { return numThreads; } }
	public int Money {
		get { return money; }
		set { money = value; }
	}
	public int Experience {
		get { return experience; }
		set { experience = value; }
	}
	public int Damage { get { return damage; } }
	public float ChargedDamageScale { get { return chargedDamageScale; } }
	public int Defense { get { return defense; } }
	public int Health { get { return health; } 
						set { health = value; } }
}
