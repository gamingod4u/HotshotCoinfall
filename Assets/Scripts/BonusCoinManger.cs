using UnityEngine;
using System.Collections;

public class BonusCoinManger : MonoBehaviour 
{
	#region Class Variables
	public GameObject[] bonusCoins;
	public GameObject   fences;


	private CoinManager coinManager;
	private GameObject[] coins;

	private Vector3	    fencesStartPos;
	private Vector3     fencesEndPos; 
	private float 	  	fenceTimer = 0;
	private float 	  	percentTimer = 0;
	private float     	fenceMaxTime = 600;
	private float 	 	percentMaxTime = 360;
	private float 		fenceRaisedTimer = 0;
	private float 		fenceRaisedTime = 0;
	private bool 		fenceUp = false;
	private int 		comboCount = 0;
	#endregion

	#region Unity Functions
	// Use this for initialization
	void Awake()
	{
		GameManager.OnUpdate += new GameManager.GameUpdate(OnUpdate);
	}

	void OnDestroy()
	{
		GameManager.OnUpdate -= new GameManager.GameUpdate(OnUpdate);
	}
	
	void Start () 
	{
		coinManager = this.GetComponent<CoinManager>();
		fencesStartPos = fences.transform.position;
		fencesEndPos = new Vector3(fences.transform.position.x, fences.transform.position.y + 2.7f, fences.transform.position.z);
	}
	
	// Update is called once per frame
	void OnUpdate () 
	{
		if(coinManager.Combo >= 3)
		{
			comboCount = coinManager.Combo;
		}

		if(comboCount != 0 && coinManager.Combo == 0)
		{
			fenceTimer = 0;
			percentMaxTime = 0;
			ComboSpawn();
		}
		else
		{
			GetFenceTime();
			GetPercentTime();
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			RaiseFences(10);
		}
		if(fenceUp && fenceRaisedTimer < fenceRaisedTime)
		{
			fenceRaisedTimer += Time.deltaTime;

			if(fences.transform.position.y < fencesEndPos.y)
			{
				fences.transform.position = Vector3.MoveTowards(fences.transform.position, fencesEndPos, 5 * Time.deltaTime);
			}
		}
		else if( fenceUp && fenceRaisedTimer >= fenceRaisedTime)
		{
			if(fences.transform.position.y > fencesStartPos.y)
			{
				fences.transform.position = Vector3.MoveTowards(fences.transform.position, fencesStartPos, 5 * Time.deltaTime);
			}
			else
			{
				fenceUp = false;
				fences.SetActive(false);
				fenceTimer = 0;
				fenceRaisedTimer = 0;
				fenceRaisedTime =0;
			}
		}
	}
	#endregion

	#region Class Functions
	void ComboSpawn()
	{
		GetCoins();
		if(comboCount == 3)
		{
			comboCount = 0;
			int rand = Random.Range(0, 4);
			int randPos = Random.Range(0, coins.Length);
			
			Instantiate(bonusCoins[rand], coins[randPos].transform.position, coins[randPos].transform.rotation);
			Destroy(coins[randPos].gameObject);
			coins = new GameObject[0];
		}
		else if(comboCount == 4)
		{
			int [] randCoin = new int[2];
			int [] randCoinPos = new int[2];

			for(int i = 0; i < 1; i++)
			{
				randCoin[i] = Random.Range(0,4);
				randCoinPos[i] = Random.Range(0, coins.Length);
				Instantiate(bonusCoins[randCoin[i]], coins[randCoinPos[i]].transform.position, coins[randCoinPos[i]].transform.rotation);
				Destroy (coins[randCoinPos[i]].gameObject);
			}
		
			coins = new GameObject[0];
			comboCount = 0;
		}
		else if (comboCount == 5)
		{

			int [] randCoin = new int[3];
			int [] randCoinPos = new int[3];

			for(int i = 0; i < 2; i++)
			{
				randCoin[i] = Random.Range(0,4);
				randCoinPos[i] = Random.Range(0,coins.Length);
				Instantiate(bonusCoins[randCoin[i]], coins[randCoinPos[i]].transform.position, coins[randCoinPos[i]].transform.rotation);
				Destroy(coins[randCoinPos[i]].gameObject);
			}
			coins = new GameObject[0];
			comboCount = 0;
		}
		else 
		{
			int [] randCoinPos = new int[4];

			for(int i = 0; i < 3; i++)
			{
				randCoinPos[i] = Random.Range(0, coins.Length);
				Instantiate(bonusCoins[i], coins[randCoinPos[i]].transform.position, coins[randCoinPos[i]].transform.rotation);
				Destroy(coins[randCoinPos[i]].gameObject);
			}

			comboCount = 0;
		}
		
	}

	void GetCoins()
	{
		coins = GameObject.FindGameObjectsWithTag("coin");
	}

	void GetFenceTime()
	{
		if(fenceTimer < fenceMaxTime)
		{
			fenceTimer += Time.deltaTime;
		}
		else 
		{
			fenceTimer = 0;
			TimerSpawn(0);	
		}
	}

	void GetPercentTime()
	{
		if(percentTimer < percentMaxTime)
		{
			percentTimer += Time.deltaTime;
		}
		else 
		{
			percentTimer = 0;
			TimerSpawn(1);
		}
	}

	void TimerSpawn(int type)
	{
		GetCoins();
		switch(type)
		{
			case 0:
			{
				int coin = Random.Range(0,coins.Length);
				Instantiate(bonusCoins[0], coins[coin].transform.position, coins[coin].transform.rotation);
				Destroy(coins[coin].gameObject);
				break;
			}
			case 1:
			{
				int coin = Random.Range(0, coins.Length);
				int pick = Random.Range(1,4);
				Instantiate(bonusCoins[pick], coins[coin].transform.position, coins[coin].transform.rotation);
				Destroy (coins[coin].gameObject);
				break;
			}
		}
		coins = new GameObject[0];
	}
	#endregion

	public void RaiseFences(float time)
	{
		fences.SetActive(true);
		fenceRaisedTimer = 0;
		fenceRaisedTime = time;
		fenceUp = true;
	  

	}
}
