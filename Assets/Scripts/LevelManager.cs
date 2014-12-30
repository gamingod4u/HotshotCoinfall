using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{

	public delegate void LevelUp();
	public static event LevelUp OnLevelUp;

	private const int 	MAXLEVEL  = 50;
	private const float MINBAR = .01f;
	private const float	MAXBAR = 64;
	
	private int [] 		levelXPGoals;
	private float 		newBar = 0f;
	private float 		newGoal = 0f;
	private float 		goalDifference = 0f;
	private int		 	currentXP = 0;
	private int 		gainedXP = 0;
	private int 		currentLevel = 0;
	private int 		lastLevel = 0;
	private int			nextLevelXP = 0;
	
	
	
	public int AddExperience
	{
		get{return currentXP;}
		set{currentXP = value;}
	}
	
	void Awake()
	{
		GameManager.OnUpdate += new GameManager.GameUpdate(UpdateLevel);
		
	}
	
	void OnDestroy()
	{
		GameManager.OnUpdate -= new GameManager.GameUpdate(UpdateLevel);
		
	}
	
	
	// Use this for initialization
	public LevelManager Init () 
	{
	
		currentXP = AddExperience;		
		levelXPGoals = new int[MAXLEVEL];
		for(int i = 0; i < MAXLEVEL; i++)
		{
			levelXPGoals[i] += i * 400;				
		}
		levelXPGoals[0]= 150;
		levelXPGoals[49] = 20000;
		NewLevelBar();
		UIManager.instance.Level= currentLevel;
		
		return this;
	}
	
	void UpdateLevel()
	{
		
	
		if(currentXP >= levelXPGoals[currentLevel])
		{	
			lastLevel = currentLevel;
			currentLevel++;
			UIManager.instance.Level = currentLevel;
			if(OnLevelUp != null)
				OnLevelUp();
		}	
		if(currentLevel != lastLevel)
		{
			NewLevelBar();	
		}
		
		UpdateBar();
	}
	
	
	private void NewLevelBar()
	{	
		newGoal = levelXPGoals[currentLevel];
		
		if(currentLevel > 0)
			goalDifference = newGoal - levelXPGoals[currentLevel-1];
		else 
			goalDifference = newGoal - 0;
			
	}
	
	private void UpdateBar()
	{
		if(currentLevel > 0)
			gainedXP = currentXP - levelXPGoals[currentLevel-1];
		else 
			gainedXP = currentXP - 0;
			
		newBar = (gainedXP*MAXBAR)/goalDifference;	
		
		if(newBar > MAXBAR)
			newBar = MAXBAR;
		else if(newBar < MINBAR)
			newBar = MINBAR;	
			
		UIManager.instance.LevelBar = newBar;
	}
}
