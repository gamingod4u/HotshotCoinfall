using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour 
{
	public static UIManager instance;
	public UILabel coins, freeThrow, level, jackpot;
	public GameObject levelBar;
	
	
	
	public int Coins
	{
		set{ coins.text = value.ToString();}
	}
	
	public int FreeThrows
	{
		set{ freeThrow.text = value.ToString();}
	}
	
	public int Level
	{
		set{ level.text = value.ToString();}
	}
	
	public int Jackpot
	{
		set{ jackpot.text = value.ToString();}
	}
	
	public float LevelBar 
	{
		set{ levelBar.transform.localScale = new Vector3(value, levelBar.transform.localScale.y, levelBar.transform.localScale.z); }
	}
	void Awake()
	{
		instance = this;
	}	
	// Use this for initialization
	void Start () 
	{
		GameManager.OnUpdate += new GameManager.GameUpdate(OnUpdate);	
	}
	
	void OnDestroy()
	{
		GameManager.OnUpdate -= new GameManager.GameUpdate(OnUpdate);
		
	}
	// Update is called once per frame
	void OnUpdate () 
	{
		
	}
}
