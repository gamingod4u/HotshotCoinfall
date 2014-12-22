using UnityEngine;
using System.Collections;

public class PushBlock : MonoBehaviour 
{

	private Vector3 startPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;

	private float   pushTime = 3f;
	
	IEnumerator Start () 
	{
		startPosition = transform.position;
		float theMove = transform.position.z-2f;
		endPosition = new Vector3(transform.position.x, transform.position.y,theMove);
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
