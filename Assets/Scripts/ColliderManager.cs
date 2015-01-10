using UnityEngine;
using System.Collections;

public class ColliderManager : MonoBehaviour 
{
	public int COLLIDERTYPE;

	public GameObject levelManagerObject;
	public GameObject coinManagerObject;


	private LevelManager levelManager;
	private CoinManager  coinManager;
	private BonusCoinManger bonusCoinManager;


	void Awake()
	{
		levelManager = levelManagerObject.GetComponent<LevelManager>();
		coinManager = coinManagerObject.GetComponent<CoinManager>();
		bonusCoinManager = coinManagerObject.GetComponent<BonusCoinManger>();

	}

	void Start()
	{

	}

	void OnDestroy()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("Colliding with:" + other.transform.name);
		//if(other.transform.tag != "coin")
			//return;

		switch(other.transform.name)
		{
			case "goldCoin":
			{
				switch(COLLIDERTYPE)
				{
					case 0:
					{
						coinManager.Jackpot++;
						DestroyObject(other.gameObject);
						break;
					}
					case 2:
					{
						coinManager.Coins++;
						coinManager.Combo++;
						levelManager.Experience += ((int)(2 * coinManager.Combo));
						Destroy(other.gameObject);
						break;
					}
					case 3:
					{
						coinManager.Coins++;
						coinManager.Combo++;
						levelManager.Experience += ((int)(3 * coinManager.Combo));
						Destroy(other.gameObject);
						break;
					}
					case 5:
					{
						coinManager.Coins++;
						coinManager.Combo++;
						levelManager.Experience += ((int) (5 * coinManager.Combo));
						Destroy(other.gameObject);
						break;
					}
					break;
				}
				break;
			}
		case "goldCoin(Clone)":
		{
			switch(COLLIDERTYPE)
			{
			case 0:
			{
				coinManager.Jackpot++;
				DestroyObject(other.gameObject);
				break;
			}
			case 2:
			{
				coinManager.Coins++;
				coinManager.Combo++;
				levelManager.Experience += ((int)(2 * coinManager.Combo));
				Destroy(other.gameObject);
				break;
			}
			case 3:
			{
				coinManager.Coins++;
				coinManager.Combo++;
				levelManager.Experience += ((int)(3 * coinManager.Combo));
				Destroy(other.gameObject);
				break;
			}
			case 5:
			{
				coinManager.Coins++;
				coinManager.Combo++;
				levelManager.Experience += ((int) (5 * coinManager.Combo));
				Destroy(other.gameObject);
				break;
			}
				break;
			}
			break;
		}
		case "fenceCoin(Clone)":
			{
				switch(COLLIDERTYPE)
				{
					case 0:
					{	
						DestroyObject(other.gameObject);
						break;
					}
					case 2:
					{
						bonusCoinManager.RaiseFences(10);
						Destroy(other.gameObject);
						break;
					}
					case 3:
					{
						bonusCoinManager.RaiseFences(15);
						Destroy(other.gameObject);
						break;
					}
					case 5:
					{
						bonusCoinManager.RaiseFences(25);
						Destroy(other.gameObject);
						break;
					}
					break;
				}
			break;
		}
		case "15xpCoin(Clone)":
		{
			switch(COLLIDERTYPE)
			{
				case 0:
				{
					DestroyObject(other.gameObject);
					break;
				}
				case 2:
				{
					int exp = (int)(levelManager.Experience/(2 * coinManager.Combo) * .15f);
					Debug.Log(exp);
					levelManager.Experience += exp;
					Destroy(other.gameObject);
					break;
				}
				case 3:
				{
					int exp = (int)(levelManager.Experience/(3 * coinManager.Combo) * .15f);
					Debug.Log (exp);
					levelManager.Experience += exp;
					Destroy (other.gameObject);
					break;
				}
				case 5:
				{
					int exp = (int)(levelManager.Experience/(5 * coinManager.Combo) * .15f);
					Debug.Log(exp);
					levelManager.Experience += exp;
					Destroy(other.gameObject);
					break;
				}
				break;
			}
			break;
		}
		case "30xpCoin(Clone)":
		{
			switch(COLLIDERTYPE)
			{
				case 0:
				{
					DestroyObject(other.gameObject);
					break;
				}
				case 2:
				{
					int exp = (int)(levelManager.Experience/(2 * coinManager.Combo) * .30f);
					Debug.Log(exp);
					levelManager.Experience += exp;
					Destroy(other.gameObject);
					break;
				}
				case 3:
				{
					int exp = (int)(levelManager.Experience/(3 * coinManager.Combo) * .30f);
					Debug.Log (exp);
					levelManager.Experience += exp;
					Destroy (other.gameObject);
					break;
				}
				case 5:
				{
					int exp = (int)(levelManager.Experience/(5 * coinManager.Combo) * .30f);
					Debug.Log(exp);
					levelManager.Experience += exp;
					Destroy(other.gameObject);
					break;
				}
				break;
			}
			break;
		}
		case "50xpCoin(Clone)":
		{
				switch(COLLIDERTYPE)
				{
					case 0:
					{
						DestroyObject(other.gameObject); 
						break;
					}
					case 2:
					{
						int exp = (int)(levelManager.Experience/(2 * coinManager.Combo) * .5f);
						Debug.Log(exp);
						levelManager.Experience += exp;
						Destroy(other.gameObject);
						break;
					}
					case 3:
					{
						int exp = (int)(levelManager.Experience/(3 * coinManager.Combo) * .5f);
						Debug.Log (exp);
						levelManager.Experience += exp;
						Destroy (other.gameObject);
						break;
					}
					case 5:
					{
						int exp = (int)(levelManager.Experience/(5 * coinManager.Combo) * .5f);
						Debug.Log(exp);
						levelManager.Experience += exp;
						Destroy(other.gameObject);
						break;
					}
					break;
				}
				break;
			}
		}
	}
}
