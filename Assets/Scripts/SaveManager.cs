using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour 
{
	public static SaveManager instance;

	public GameObject 		coinHolder;
	
	private int 			playerXP = 0;
	private int 			playerLevel = 0;
	private int 			coinCount = 0;
	private int 			freeThrows = 0;
	private int 			jackpotCount = 0;
	
	public 	bool   			isQuiting = false;
	
	public 	float 			playerPoints = 0f;
	
	public 	GameObject [] 	coinsInScene;
	
	public int PlayerXP
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
	void OnApplicationQuit () 
	{
		coinsInScene = GameObject.FindGameObjectsWithTag("coin");
		string  [] coinType = new string[coinsInScene.Length];
		Vector3 [] coinPositions = new Vector3[coinsInScene.Length];
		Vector3 [] coinsRotations = new Vector3[coinsInScene.Length];
		for(int i = 0; i < coinsInScene.Length; i++)
		{
			coinType[i] = coinsInScene[i].transform.name;
			coinPositions[i] = coinsInScene[i].transform.position;
			coinsRotations[i] = new Vector3(	coinsInScene[i].transform.rotation.x, 
		                               		coinsInScene[i].transform.rotation.y, 
		                               		coinsInScene[i].transform.rotation.z);
		}
	
		PlayerPrefs.SetFloat("Time", Time.time);
		PlayerPrefs.SetInt("PlayerXP", PlayerXP);
		PlayerPrefs.SetInt("Coins", CoinCount);
		PlayerPrefs.SetInt("Freethrows", FreeThorws);
		PlayerPrefs.SetInt("Jackpot", JackPot);
		PlayerPrefsX.SetStringArray("CoinTypes", coinType);
		PlayerPrefsX.SetVector3Array("CoinPositions", coinPositions);
		PlayerPrefsX.SetVector3Array("CoinRotations", coinsRotations);
	}
	void OnApplicationPause(bool paused)
	{
		if(paused)
			Application.Quit();
	}
}
