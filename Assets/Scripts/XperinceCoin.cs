using UnityEngine;
using System.Collections;

public class XperinceCoin : MonoBehaviour 
{

	private int lastLevel;	

	private GameObject coinHolderObject;
	private CoinManager coinManager;

	private GameObject levelManagerObject;
	private LevelManager levelManager;

	public int COINTYPE;

	// Use this for initialization
	void Awake () 
	{
		coinHolderObject = GameObject.FindGameObjectWithTag("coinHolder");
		coinManager = coinHolderObject.GetComponent<CoinManager>();
		transform.parent = coinHolderObject.transform;

		levelManagerObject = GameObject.FindGameObjectWithTag("levelManager");
		levelManager = levelManagerObject.GetComponent<LevelManager>();

	}

	void OnDestroy()
	{

	}

	void Start()
	{
		if( COINTYPE == null)
		{
			COINTYPE = 2;
		}

		lastLevel = levelManager.Experience;
	}
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision other)
	{
		switch(other.transform.name)
		{
			case "Zero":
			{	
				Destroy(this.gameObject);
				break;
			}
			case "Two" :
			{
				if(COINTYPE == 0)
					levelManager.Experience += (int)((2 * coinManager.Combo) * .15f);
				else if(COINTYPE == 1)
					levelManager.Experience += (int)((2 * coinManager.Combo) * .3f);
				else if(COINTYPE == 2)
					levelManager.Experience += (int)((2 * coinManager.Combo) * .5f);

				Destroy(this.gameObject);
				break;
			}
			case "Three":
			{
				if(COINTYPE == 0)
					levelManager.Experience += (int)((3 * coinManager.Combo) * .15f);
				else if(COINTYPE == 1)
					levelManager.Experience += (int)((3 * coinManager.Combo) * .3f);
				else if(COINTYPE == 2)
					levelManager.Experience += (int)((3 * coinManager.Combo) * .5f);

				Destroy(this.gameObject);
				break;
			}
			case "Five":
			{
				if(COINTYPE == 0)
					levelManager.Experience += (int)((5 * coinManager.Combo) * .15f);
				else if(COINTYPE == 1)
					levelManager.Experience += (int)((5 * coinManager.Combo) * .3f);
				else if(COINTYPE == 2)
					levelManager.Experience += (int)((5 * coinManager.Combo) * .5f);

				Destroy(this.gameObject);
				break;
			}
		}
	}
}