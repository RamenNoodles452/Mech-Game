using UnityEngine;
using System.Collections;

public class BulletShell : MonoBehaviour {
	
	private Vector3 startPosition;
	
	// Use this for initialization
	void Start () 
	{
		transform.rigidbody.AddForce((Vector3.up) * 20.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.rigidbody.AddForce((Vector3.up + Vector3.left) * 10.0f);
	}
}
