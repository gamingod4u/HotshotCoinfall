using UnityEngine;
using System.Collections;

public class BasketBallRim : MonoBehaviour
{
	private Vector3 startPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;
	
	private float   pushTime = 3f;

	IEnumerator Start () 
	{
		pushTime = Random.Range(3f,6f);
		startPosition = transform.position;
		float theMove = transform.position.x - 2.6f;
		endPosition = new Vector3(theMove, transform.position.y, transform.position.z);
		while (true) 
		{
			yield return StartCoroutine(MoveObject(transform, startPosition, endPosition, pushTime));
			yield return StartCoroutine(MoveObject(transform, endPosition, startPosition, pushTime));
		}
	}
	
	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}
}

