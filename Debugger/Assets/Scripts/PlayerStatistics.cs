using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public RectTransform healthTransform;
	private float cachedY;
	private float minXvalue;
	private float maxXvalue;
	public Text healthText;
	public Image visualHealth;
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


    Vector2 rectSize = new Vector2(200, 40);
	// Use this for initialization
	void Start () {
		StatsUpdate ();
        Time.timeScale = 1.0f;
		cachedY = healthTransform.position.y;
		maxXvalue = healthTransform.position.x;
		minXvalue = healthTransform.position.x - healthTransform.rect.width;
        HandleHealth();
	}
	
	// Update is called once per frame
	void Update () {
        if (health<= 0)
        {
           Time.timeScale = 0;
        }
	}

    
    void OnGUI()
    {
        if (health <= 0)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - (rectSize.x / 2), Screen.height / 2 - (rectSize.y * 1.5f), 100, 200));
            GUILayout.Label("GameOver", GUILayout.Width(200));
            
            if (GUILayout.Button("Restart"))
            {
                Application.LoadLevel("TestEnvironment");
            }
            if (GUILayout.Button("Quit"))
            {
                Application.LoadLevel("HudWorld");
               
            }
            GUILayout.EndArea();
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
		set { health = value; HandleHealth();} }

    

	private void HandleHealth()
	{
		healthText.text = "HP: " + health;

		float currentxValue = MapValues (health, 0, maxHealth, minXvalue, maxXvalue);

		healthTransform.position = new Vector3 (currentxValue, cachedY);

		if (health > maxHealth / 2) //more than 50% hp
		{
			visualHealth.color = new Color32((byte)MapValues(health,maxHealth/2,maxHealth,255,0),255,0,255);
		} 
		else //less than 50% hp
		{
			visualHealth.color = new Color32(255,(byte)MapValues(health,0,maxHealth/2,0,255),0,255);
		}
	}

	private float MapValues(float x, float inMin, float inMax ,float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

	}


}
 