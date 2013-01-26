using UnityEngine;
using System.Collections;

public class AccelThrusters : MonoBehaviour {
	
	public int weight = 1;
	public float accelModValue = 8.0f;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Equip(WalkerMovement movementScript)
	{
		movementScript.accelerationModifier += accelModValue;
	}
	
	public void Unequip(WalkerMovement movementScript)
	{
		movementScript.accelerationModifier -= accelModValue;	
	}
	
	public int GetWeight()
	{
		return weight;
	}
}
