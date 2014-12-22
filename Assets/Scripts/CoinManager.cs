using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour 
{
	
	private const int 	MAXCOINS = 40;
	private const int 	MINCOINS = 0;
	private const int 	RECOUPMINUTE = 60;
	
	private float 		recoupTime = 0f;
	private float 		comboTime = 0f;
	private bool 		recoupCoins = false;				
	private int 		coinCombo = 1;
	private int 		coinsCount = 0;
	private int 		jackpotCoins = 0;
	
	/// <summary>
	/// Gets and sets the coins.
	/// </summary>
	/// <value>The coins.</value>
	public int Coins
	{
		get{return coinsCount;}
		set{coinsCount = value;}
	}
 	
 	public int Combo
 	{
 		get{return coinCombo;}
 		set{
 				comboTime = 0;
 				coinCombo = value;
 			}
 	}
 	public int Jackpot
 	{
 		get{return jackpotCoins;}
 		set{jackpotCoins = value;}
 	}
	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake()
	{
		GameManager.OnUpdate += new GameManager.GameUpdate(OnUpdate);
	}
	
	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy()
	{
		GameManager.OnUpdate -= new GameManager.GameUpdate(OnUpdate);
	}
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	public CoinManager Init (int count, float lastPlayTime) 
	{	
		coinsCount = count;
		recoupTime = lastPlayTime;
		
		GetRecoupTime();	
		
		return this;	
	}
	
	/// <summary>
	/// Raises the update event.
	/// </summary>
	void OnUpdate () 
	{
		if(coinsCount < MAXCOINS)
			recoupCoins = true;
		else if(coinsCount > MAXCOINS)
			recoupCoins = false;
		
		if(coinsCount <= MINCOINS)
		{
			//TODO: we need to call the coin purchase scene;
			//TODO: print the time till next coin;
		}
		
		if(recoupCoins && recoupTime < RECOUPMINUTE)
		{
			recoupTime += Time.deltaTime;
		}
		else if(recoupTime >= RECOUPMINUTE)
		{
			recoupTime = 0;
			coinsCount++;
		}
		
		if(coinCombo > 1 && comboTime < 3)
			comboTime += Time.deltaTime;
		else
			coinCombo = 1;	
		
					
		}
	/// <summary>
	/// Gets the recoup time.
	/// </summary>
	private void GetRecoupTime()
	{
		int tempCoin = (int)(recoupTime/RECOUPMINUTE);
		
		if(tempCoin > 0)
		{
			int coinsNeeded = MAXCOINS - coinsCount;
			
		 	if(tempCoin > coinsNeeded)
		 	{
		 		coinsCount += coinsNeeded;
		 	}
		 	else
		 	{
		 		coinsCount += tempCoin;
		 	}
		}
			
		
	}
}
