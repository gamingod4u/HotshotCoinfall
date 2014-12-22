using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour 
{
	public static SaveManager instance;

	public GameObject 		coinHolder;
	
	private float 			playerXP = 0f;
	private int 			playerLevel = 0;
	private int 			coinCount = 0;
	private int 			freeThrows = 0;
	private int 			jackpotCount = 0;
	
	public bool   		isQuiting = false;
	
	public float 		playerPoints = 0f;
	
	public GameObject [] coinsInScene;
	
	public float PlayerXP
	{
		get{return playerXP;}
		set{playerXP = value;}
	}
	
	public int CoinCount
	{
		get{ return coinCount;}
		set{ coinCount = value;}
	}
	
	public int FreeThorws
	{
		get{return freeThrows;}
		set{freeThrows = value;}
	}
	
	public int JackPot
	{
		get{return jackpotCount;}
		set{jackpotCount = value;}
	}
	
	void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad(this);
		instance = this;
		
		
		
	}
	
	// Update is called once per frame
	void QuitGame () 
	{
		if(isQuiting)
		{
			coinsInScene = GameObject.FindGameObjectsWithTag("coin");
			Vector3 [] coinPositions = new Vector3[coinsInScene.Length];
			
			for(int i = 0; i < coinsInScene.Length; i++)
			{
				coinPositions[i] = coinsInScene[i].transform.position;
			}
		
			PlayerPrefs.SetFloat("Time", Time.time);
			PlayerPrefs.SetFloat("PlayerXP", PlayerXP);
			PlayerPrefs.SetInt("Coins", CoinCount);
			PlayerPrefs.SetInt("Freethrows", FreeThorws);
			PlayerPrefs.SetInt("Jackpot", JackPot);
			PlayerPrefsX.SetVector3Array("CoinPositions", coinPositions);
			Application.Quit();
		}
	}
}
