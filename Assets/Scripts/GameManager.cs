using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
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
			
			coinManager = coinObject.GetComponent<CoinManager>().Init(PlayerPrefs.GetInt("CoinCount"), time);
			levelManager = levelObject.GetComponent<LevelManager>().Init();
			//TODO: WE HAVE PLAYED BEFORE ACCESS THE DATA FROM OUR LAST SAVE
		}
	}
	void Awake()
	{
		InputManager.OnFreethrow += new InputManager.UIPressed(ShootingBall);
		InputManager.OnTap += new InputManager.InputTap(DropCoin);
		InputManager.OnFlick += new InputManager.InputFlick(Shooting);
		
	}
	
	void OnDestroy()
	{
		InputManager.OnFreethrow -= new InputManager.UIPressed(ShootingBall);
		InputManager.OnTap -= new InputManager.InputTap(DropCoin);
		InputManager.OnFlick -= new InputManager.InputFlick(Shooting);
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
		
	
	}
	
	private void ShootingBall()
	{
		freeThrow = true;
	}
	
	private void DropCoin(Vector3 position)
	{
		if(!freeThrow && coinCount > 0)
		{
			float newX = 0f;
			coinManager.Coins -= 1;
			
			if(position.x < midScreen)
				 newX =  position.x * -.001f;
			else
				newX = position.x * .001f;
					
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
				Spwnd.rigidbody.AddForce(new Vector3(((angle*1.45f).x * speed), ((angle.y * 4f) * speed), ((angle.z*.9f) * speed)));
			}
	 	}	
	}
	
	private float GetSpeed(float flickTime)
	{
		float flickLength = 90;
		float flickVelocity = flickLength/(flickLength - flickTime);
		
		float flickSpeed = flickVelocity * 35;
		flickSpeed = flickSpeed - (flickSpeed *1.65f);
		
		if(flickSpeed < -33)
		{
			flickSpeed = -33;
		}
		return flickSpeed;		
	}
}
