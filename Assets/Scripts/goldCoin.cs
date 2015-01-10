using UnityEngine;
using System.Collections;

public class goldCoin : MonoBehaviour 
{
	private GameObject CoinHolder;
	[HideInInspector]
	public CoinManager coinManager;
	
	private GameObject LevelObject;
	[HideInInspector]
	public LevelManager levelManager;
	
	public int value = 0;
	// Use this for initialization
	void Awake () 
	{
		CoinHolder = GameObject.FindGameObjectWithTag("coinHolder");
		coinManager = CoinHolder.GetComponent<CoinManager>();
		
		LevelObject = GameObject.FindGameObjectWithTag("levelManager");
		levelManager = LevelObject.GetComponent<LevelManager>();

		this.transform.parent = CoinHolder.transform;
	}
	
	void Update()
	{
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		switch(other.transform.name)
		{
			case "Zero":
			{	
				coinManager.Jackpot++;
				Destroy(this.gameObject);
				break;
			}
			case "Two" :
			{
				coinManager.Coins++;
				coinManager.Combo++;
				levelManager.Experience += 2 * coinManager.Combo;
				Destroy(this.gameObject);
				break;
			}
			case "Three":
			{
				coinManager.Coins++;
				coinManager.Combo++;
				levelManager.Experience += 3 * coinManager.Combo;
				Destroy(this.gameObject);
				break;
			}
			case "Five":
			{
				coinManager.Coins++;
				coinManager.Combo++;
				levelManager.Experience += 5 * coinManager.Combo;
				Destroy(this.gameObject);
				break;
			}
		}
	}
}
