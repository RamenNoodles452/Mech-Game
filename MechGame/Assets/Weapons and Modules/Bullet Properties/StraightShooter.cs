using UnityEngine;
using System.Collections;

public class StraightShooter : MonoBehaviour {
	
	public float bulletSpeed = 1.0f;
	public float bulletDamage = 1.0f;
	public float bulletRange = 30.0f;
	private Vector3 startPosition;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance( transform.position, startPosition) < bulletRange)
		{
			//Debug.Log(Vector3.Distance(startPosition, transform.position));
			transform.rigidbody.velocity = (transform.forward * 10.0f);

		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	public void SetStartPos(Vector3 startPos)
	{
		startPosition = startPos;	
	}
}
