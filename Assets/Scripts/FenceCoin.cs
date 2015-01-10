using UnityEngine;
using System.Collections;

public class FenceCoin : MonoBehaviour 
{	

	private BonusCoinManger bonusCoinManager;
	private GameObject coinholder;


	
	// Use this for initialization
	void Start () 
	{
		coinholder = GameObject.FindGameObjectWithTag("coinHolder");
		bonusCoinManager = coinholder.GetComponent<BonusCoinManger>();
		transform.parent = coinholder.transform;
	}


	void OnCollisionEnter(Collision other)
	{

		switch(other.transform.name)
		{
			case "Zero":
			{	
				Destroy(this.gameObject);
				break;
			}
			case "Two" :
			{
				bonusCoinManager.RaiseFences(10);
				Destroy(this.gameObject);
				break;
			}
			case "Three":
			{
				bonusCoinManager.RaiseFences(15);
				Destroy(this.gameObject);
				break;
			}
			case "Five":
			{
				bonusCoinManager.RaiseFences(25);
				Destroy(this.gameObject);
				break;
			}
		}
	}
}
