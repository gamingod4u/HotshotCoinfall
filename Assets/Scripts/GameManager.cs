using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	#region Class Variables and States
	public enum GameState
	{
		Intro,
		Idle,
		Drop,
		Shooting,
		Freethrow,
		Paused		
	};

	public GameState gameState;
	
	public delegate void GameUpdate();
	public static event GameUpdate OnUpdate;

	public GameObject  basketball;	
	public GameObject  coin;
	public GameObject  coinObject;
	public GameObject  levelObject;
	public GameObject  shootingPosition;
		  
	private CoinManager coinManager;
	private LevelManager	levelManager;
	
	private int 	coinCount = 0;
	private int 	freeThrowCount = 0;
	private bool 	freeThrow = false;
	private float 	midScreen = 0f;
	#endregion
	#region Class Getter/Setter Functions
	public int Freethrows
	{
		get{ return freeThrowCount;}
		set{ freeThrowCount = value;}
	}
	#endregion
	#region Unity Functions
	// Use this for initialization
	void Start () 
	{
		gameState = GameState.Intro;
		midScreen = Screen.width *.5f;
		
		if(PlayerPrefs.GetInt("FirstPlay") == 0)
		{
			PlayerPrefs.SetInt("FirstPlay", 1);
			coinManager = coinObject.GetComponent<CoinManager>().Init(40,0f);
			levelManager = levelObject.GetComponent<LevelManager>().Init();
		}
		else
		{
			float lastTime = PlayerPrefs.GetFloat("Time");
			float time = Time.time - lastTime;
			
			//coinManager = coinObject.GetComponent<CoinManager>().Init(PlayerPrefs.GetInt("CoinCount"), time);
			coinManager = coinObject.GetComponent<CoinManager>().Init(40,0f);
			levelManager = levelObject.GetComponent<LevelManager>().Init();
			levelManager.AddExperience = PlayerPrefs.GetInt("PlayerXP");
			coinManager.Jackpot = PlayerPrefs.GetInt("Jackpot");
			freeThrowCount = PlayerPrefs.GetInt("Freethrows");

			//LoadBoard();
		}
	}

	void Awake()
	{
		InputManager.OnFreethrow += new InputManager.UIPressed(ShootingBall);
		InputManager.OnTap += new InputManager.InputTap(DropCoin);
		InputManager.OnFlick += new InputManager.InputFlick(Shooting);
		LevelManager.OnLevelUp += new LevelManager.LevelUp(AddFreethrow);
		
	}
	
	void OnDestroy()
	{
		InputManager.OnFreethrow -= new InputManager.UIPressed(ShootingBall);
		InputManager.OnTap -= new InputManager.InputTap(DropCoin);
		InputManager.OnFlick -= new InputManager.InputFlick(Shooting);
		LevelManager.OnLevelUp -= new LevelManager.LevelUp(AddFreethrow);
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if(OnUpdate != null)
			OnUpdate();
		UpdateAll();	
		
	}
	
	private void UpdateAll()
	{
		coinCount = coinManager.Coins;
		UIManager.instance.Coins = coinCount;
		UIManager.instance.Jackpot = coinManager.Jackpot;
		UIManager.instance.FreeThrows = Freethrows;
	
	}

	#endregion
	#region Class Functions

	private float GetSpeed(float flickTime)
	{
		float flickLength = 90;
		float flickVelocity = flickLength/(flickLength - flickTime);
		
		float flickSpeed = flickVelocity * 30;
		flickSpeed = flickSpeed - (flickSpeed *1.65f);
		
		if(flickSpeed < -33)
		{
			flickSpeed = -33;
		}
		return flickSpeed;		
	}

	private void LoadBoard()
	{
		string [] coinType = PlayerPrefsX.GetStringArray("CoinTypes");
		Vector3[] coinPos = PlayerPrefsX.GetVector3Array("CoinPositions");
		Vector3[] coinRot = PlayerPrefsX.GetVector3Array("CoinRotation");
		
		for (int i = 0; i < coinPos.Length; i++) 
		{
			switch(coinType[i])
			{
				case "goldCoin":
				{
					GameObject Spwnd = Instantiate(coin, coinPos[i], 
					                               new Quaternion(coinRot[i].x, coinRot[i].y, coinRot[i].z,0)) as GameObject;
					break;
				}
			}
		}
	}

	#endregion
	#region Class Event Functions

	private void AddFreethrow()
	{
		Freethrows++;
	}

	private void DropCoin(Vector3 position)
	{
		if(!freeThrow && coinCount > 0)
		{
			coinManager.Coins -= 1;
			float newX = Mathf.Clamp(position.x, -1.75f,1.75f);
			GameObject Spwnd = Instantiate(coin, new Vector3( newX, position.y * .002f, 1.7f), coin.transform.rotation)as GameObject;
			
		}
	}

	private void Shooting(Vector3 angle, float time)
	{		 
		if(time > .035f)
		{
			if(freeThrow && freeThrowCount > 0)
			{
				freeThrowCount -= 1;
				GameObject Spwnd = Instantiate(basketball, shootingPosition.transform.position, Quaternion.identity)as GameObject;
				float speed = GetSpeed(time);
				Spwnd.rigidbody.AddForce(new Vector3((angle.x * speed), (angle.y * speed), (angle.z * speed)));	
			}
			else if(coinCount > 0)
			{
				coinManager.Coins -=1;
				GameObject Spwnd = Instantiate(coin, shootingPosition.transform.position, Quaternion.identity)as GameObject;
				float speed = GetSpeed(time);
				Spwnd.rigidbody.AddForce(new Vector3(((angle.x * speed)*.9f), ((angle.y * speed)*.15f), (angle.z * speed)*.2f));
			}
		}	
	}

	private void ShootingBall()
	{
		freeThrow = true;
	}
	#endregion
}
