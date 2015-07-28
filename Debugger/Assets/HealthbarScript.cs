using UnityEngine;
using System.Collections;

public class HealthbarScript : MonoBehaviour {

    [SerializeField]
    GameObject Health = null;
    [SerializeField]
    GameObject Bar = null;
    [SerializeField]
    GameObject target = null;

	// Use this for initialization
	void Start () {
        Health.GetComponent<Renderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
	    if(target != null)
        {
            EnemyStatistics currTarget = target.GetComponent<EnemyStatistics>();
            transform.position = new Vector3(target.transform.position.x + 0.52f, 0.2f, target.transform.position.z + 3.2f);

            if(currTarget.Health / currTarget.MaxHealth < 1.0f)
            {
                Health.GetComponent<Renderer>().enabled = true;
                Bar.GetComponent<Renderer>().enabled = true;

                Health.transform.localScale = new Vector3(currTarget.Health / (float)currTarget.MaxHealth, Health.transform.localScale.y, Health.transform.localScale.z);
            }
            else
            {
                Health.GetComponent<Renderer>().enabled = false;
                Bar.GetComponent<Renderer>().enabled = false;
            }

            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public GameObject Target { get; set; }
}
