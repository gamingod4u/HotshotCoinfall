using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour 
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
				
	}
		
	// Update is called once per frame
	void Update ()
	{
		
	}
}
