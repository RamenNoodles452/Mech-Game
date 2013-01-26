using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissleTurret : MonoBehaviour {
	
	//aiming variables
	public float errorAmount = 0.00f;
	public float reloadTime = 1.0f;
	private float reloadTimer = 0.0f;
	public float rocketStartHeight = 6.0f;
	
	public float turretRotateSpeed = 500.0f;
	public float idleRotateSpeed = 50.0f;
	
	public int turretHealth;
	
	public Transform turret;
	private Vector3 turretDirection;
	private Vector3 targetPosition;
	private float aimError;
	
	public List<GameObject> targetsInRange;
	private GameObject currentTarget;
	
	public TowerHomingRocket towerRocketPrefab;
	
	// Use this for initialization
	void Start () 
	{
		targetsInRange = new List<GameObject>();
		currentTarget = null;
		turretHealth = 100;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//reaquire a new target!
		if(targetsInRange.Count > 0 && currentTarget == null)
		{
			float closestDistance = -1.0f;
			for(int i = 0; i < targetsInRange.Count; i++)
			{
				float distance = Vector3.Distance(transform.position, targetsInRange[i].transform.position);
				if(currentTarget == null)
				{
					currentTarget = targetsInRange[i];
					closestDistance = distance;
					reloadTimer = 0.0f;
				}
				else
				{
					if(distance < closestDistance)
					{
						currentTarget = targetsInRange[i];
						closestDistance = distance;	
						reloadTimer = 0.0f;
					}
				}
			}
			
			//error out if there is still no current target by this point cuz there should be one
			if(currentTarget == null)
				Debug.LogError("There are targets within range of the turret, but the turret doesn't have a CurrentTarget");
		}
		else if(currentTarget)
		{
			//fire rockets!
			reloadTimer = reloadTimer + Time.deltaTime;
			if(reloadTimer > reloadTime)
			{
				Vector3 rocketStartPosition = transform.position;
				rocketStartPosition.y = rocketStartHeight;
				
				TowerHomingRocket rocketInstance = (TowerHomingRocket)Instantiate(towerRocketPrefab, rocketStartPosition, turret.rotation);
				rocketInstance.SetRocketTarget(currentTarget.gameObject);
				reloadTimer = 0.0f;
			}
		}
		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" || other.tag == "Minion")
		{
			if(!currentTarget && targetsInRange.Count == 0)
			{
				currentTarget = other.gameObject;
				reloadTimer = 0.0f;
			}
			targetsInRange.Add(other.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player" || other.tag == "Minion")
		{
			if(other.gameObject == currentTarget)
				currentTarget = null;
			targetsInRange.Remove(other.gameObject);
		}	
	}
	
	//specifically dealing with rotating the turret
	void LateUpdate()
	{
		if(currentTarget)
		{
			CalculateAimPos(currentTarget.transform.position);
			turretDirection = Vector3.RotateTowards(turretDirection, targetPosition, turretRotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
			turretDirection = turretDirection.normalized;
			
			turret.rotation = Quaternion.LookRotation(turretDirection);	
		}
		else
		{
			turret.Rotate(Vector3.up * idleRotateSpeed * Time.deltaTime, Space.Self);
			//turretDirection = turret.transform.TransformPoint(lookPosition);
		}
	}
	void CalculateAimPos(Vector3 targetPos)
	{
		//Vector3 aimPoint = new Vector3(targetPos.x + aimError, targetPos.y + aimError, targetPos.z + aimError);
		targetPos.y = transform.position.y;
		targetPosition = targetPos - transform.position;	
	}
	void CalculateAimError()
	{
		aimError = Random.Range(-errorAmount, errorAmount);	
	}

}

