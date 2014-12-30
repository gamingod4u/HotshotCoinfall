using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour 
{
	public delegate void UIPressed();
	public static event UIPressed OnPause;
	public static event UIPressed OnStore;
	public static event UIPressed OnFreethrow;
	
	public delegate void InputTap(Vector3 position);
	public static event InputTap OnTap;
	
	public delegate void InputFlick(Vector3 angle, float time);
	public static event InputFlick OnFlick;
	

	private Ray 			ray;
	private RaycastHit 		rayHit;
	
	private Vector3 		startPosition = Vector3.zero;
	private Vector3 		endPosition = Vector3.zero;
	private Vector3			flickAngle = Vector3.zero;
	private float			flickTime = 0;
	private bool 			onTap = false;
	private bool 			onFlick = false;
	private bool 			onPause = false;
	private bool 			getFlickTime = false;
	
	private void Awake()
	{
		GameManager.OnUpdate += new GameManager.GameUpdate(InputUpdate);
	
	}
	
	private void OnDestroy()
	{
		GameManager.OnUpdate -= new GameManager.GameUpdate(InputUpdate);
	}
	
	
	public InputManager Init()
	{
		return this;
	}
	
	private void InputUpdate()
	{
		if(Input.touchCount > 0)
		{
			GetTouch ();
		}
	
		if(getFlickTime)
			flickTime+=Time.deltaTime;
	}
	
	private void GetAngle()
	{
		flickAngle = Camera.main.ScreenToWorldPoint(new Vector3(endPosition.x, endPosition.y + 800.0f, ((camera.nearClipPlane - 100f) * 1.8f)));
	}
	
	private void GetTouch()
	{
		Touch touch = Input.GetTouch(0);
		

		switch(touch.phase)
		{
			case TouchPhase.Began:
			{
				startPosition = touch.position;
				flickTime = 0;
				getFlickTime = true;
				break;
			}
			
			case TouchPhase.Ended:
			{
				if(getFlickTime)
				{
					endPosition = touch.position;
					getFlickTime = false;
					GetAngle();
					
					float distance = (endPosition.y - startPosition.y);
									
					if(distance > 40.0f && OnFlick != null)
					{
						OnFlick(flickAngle, flickTime);
						flickAngle = Vector3.zero;
						flickTime = 0f;
					}
					else if(distance < 1.0f)
					{
						
						getFlickTime = false;
						ray = Camera.main.ScreenPointToRay(touch.position);
						if(Physics.Raycast(ray, out rayHit))
						{
							SwitchTap(rayHit.transform.name, rayHit.point);
						}
					}
				}
				break;
			}
		}		
	}
		
	private void SwitchTap(string name, Vector3 position)
	{
		switch(name)
		{
			case "Freethrow": 
			{
				if(OnFreethrow != null)
					OnFreethrow();
				
				break;
			}
			case "Store": 
			{
				onPause = true;
				
				if(OnStore != null)
					OnStore();
				
				break;
			}
			case "Pause": 
			{
				onPause = true;
				
				if(OnPause != null)
					OnPause();
				
				break;
			}
			case "Close":
			{
				onPause = false;
				break;
			}
			
			default:
			{
				if(OnTap != null)
					OnTap(position);

					break;
			}	
		}
	}
}		