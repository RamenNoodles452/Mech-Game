using UnityEngine;
using System.Collections;

public class StraightShooterBlue : MonoBehaviour {
	
	public int weight = 1;
	public float bulletSpeed = 1.0f;
	public float bulletDamage = 1.0f;
	public float bulletRange = 30.0f;
	private Vector3 startPosition;
	
	Color targetedColor = new Color(0.0f, 0.0f, 1.0f);
	
	// Use this for initialization
	void Start () 
	{
		//renderer.material.color = targetedColor;
	}
	
	// Update is called once per frame
	void Update () 
	{
		renderer.material.color = targetedColor;
		//Debug.Log(renderer.material.color);
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
	
	public int GetWeight()
	{
		return weight;
	}
}
