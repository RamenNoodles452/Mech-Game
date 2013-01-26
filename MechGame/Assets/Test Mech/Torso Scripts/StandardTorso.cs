using UnityEngine;
using System.Collections;

public class StandardTorso : MonoBehaviour {
	
	public float turretRotateSpeed = 500.0f;
	
	//private variables dealing with turret rotation
	private float mouseX;
	private float mouseY;
	public Vector3 mousePosition = Vector3.zero;
	private Vector3 turretDirection = Vector3.zero;
	
	//variables dealing with weapons
	private bool firePrimary;
	private bool fireSecondary;
	
	public Transform upperBody;
	private TestWeaponSystem weaponSystem;
	
	// Use this for initialization
	void Start () 
	{
		weaponSystem = GetComponent<TestWeaponSystem>();
		if(!upperBody)
		{
			Debug.LogError("Please assign an upperBody for the mech.");
			return;	
		}
		if(!weaponSystem)
		{
			Debug.LogError("Please assign a weapon system for the mech");
			return;
		}
		firePrimary = false;
		fireSecondary = false;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//turret rotation calculations
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
		mouseX -= Screen.width/2;
		mouseY -= Screen.height/2;
		mousePosition.x = -mouseY;
		mousePosition.z = mouseX;
		
		//check if you're firing stuff
		if(Input.GetButton("Fire Primary"))
		{
			firePrimary = true;
		}
		else
		{
			firePrimary = false;	
		}
		if(Input.GetButton("Fire Secondary"))
		{
			fireSecondary = true;
		}
		else
		{
			fireSecondary = false;	
		}
		
		if(firePrimary)
		{
			//weaponSystem.FirePrimary(turretDirection);
			weaponSystem.FirePrimary(upperBody.rotation);
		}
		if(fireSecondary)
		{
			weaponSystem.FireSecondary(upperBody.rotation);	
		}
		
		//Debug.Log(upperBody.rotation);
		
	}
	
	void LateUpdate()
	{
		turretDirection = Vector3.RotateTowards(turretDirection, mousePosition, turretRotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
		
		turretDirection = turretDirection.normalized;
		
		upperBody.rotation = Quaternion.LookRotation(turretDirection);
		
		turretDirection *= 3.0f;
		Debug.DrawLine(upperBody.position, upperBody.position + turretDirection);
		//Debug.Log(upperBody.position + mousePosition);
		
	}
}