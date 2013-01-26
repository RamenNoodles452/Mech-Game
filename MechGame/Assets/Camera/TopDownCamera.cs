using UnityEngine;
using System.Collections;

public class TopDownCamera : MonoBehaviour {
	
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 5.0f;
	// the height we want the camera to be above the target
	public float height = 20.0f;
	
	//Camera position
	private Vector3 cameraPosition = Vector3.zero;
	
	//Variables dealing with camera lean
	private float mouseDistance;
	public float maxLeanDistance = 4.0f;
	
	StandardTorso torso;
	
	// Use this for initialization
	void Start () 
	{
		if(!target)
		{
			Debug.Log("Please assign a target to the camera");
			return;	
		}
		cameraPosition = target.transform.position + new Vector3(distance, height, 0.0f);
		transform.position = cameraPosition;
		//transform.rotation = cameraRotation;
		transform.LookAt(target);
		
		torso = target.GetComponent<StandardTorso>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		mouseDistance = Mathf.Sqrt(Mathf.Pow(torso.mousePosition.x, 2) + Mathf.Pow(torso.mousePosition.z, 2));
		//Debug.Log(Mathf.Sqrt(Mathf.Pow(torso.mousePosition.x, 2) + Mathf.Pow(torso.mousePosition.z, 2)));
		
		cameraPosition = target.transform.position + new Vector3(distance, height, 0.0f);
		cameraPosition = cameraPosition + 
			new Vector3(Mathf.Clamp((torso.mousePosition.x / 100.0f), -maxLeanDistance, maxLeanDistance), 
				0.0f, Mathf.Clamp((torso.mousePosition.z / 100.0f), -maxLeanDistance, maxLeanDistance));

		transform.position = cameraPosition;
	}
	
}

