using UnityEngine;
using System.Collections;

public class TowerHomingRocket : MonoBehaviour {
	
	public float rocketSpeed = 25.0f;
	private float rocketAccel = 0.0f;
	public GameObject rocketTarget;
	private Vector3 startPosition;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//transform.LookAt(rocketTarget.transform);
		//transform.rigidbody.velocity = (transform.forward * 10.0f);
		
		rocketAccel = rocketAccel + Time.deltaTime;
		transform.rotation = Quaternion.LookRotation(rocketTarget.transform.position - transform.position);
		transform.rigidbody.velocity = (transform.forward * (rocketSpeed + rocketAccel));
		//transform.rigidbody.velocity = Vector3.Lerp(transform.rigidbody.velocity, (transform.forward * 10.0f), Time.deltaTime);
		
		
		/*
		Vector3 targetDelta = rocketTarget.position - transform.position;
		
		//get the angle between transform.forward and target delta
		float angleDiff = Vector3.Angle(transform.forward, targetDelta);
                           
		// get its cross product, which is the axis of rotation to
		// get from one vector to the other
		Vector3 cross = Vector3.Cross(transform.forward, targetDelta);
 
		// apply torque along that axis according to the magnitude of the angle.
		rigidbody.AddTorque(cross * angleDiff * force);
		*/

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == rocketTarget)
		{
			Destroy(gameObject);
		}
	}
	
	public void SetStartPos(Vector3 startPos)
	{
		startPosition = startPos;	
	}
	public void SetRocketTarget(GameObject target)
	{
		rocketTarget = target;	
	}
}
