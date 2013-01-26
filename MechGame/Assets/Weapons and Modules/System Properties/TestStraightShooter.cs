
using UnityEngine;
using System.Collections;

public class TestStraightShooter : MonoBehaviour {
	
	public StraightShooter bulletPrefab;
	//public Transform bulletPrefab;
	public float bulletHeight =  2.0f;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void Fire(Quaternion aimDirection)
	{
		
		Vector3 bulletStartPosition = transform.position;
		bulletStartPosition.y = bulletHeight;
		//StraightShooter clone;
		//clone = (StraightShooter)Instantiate(defaultBullet, bulletStartPosition, aimDirection);
		
		StraightShooter bulletInstance = (StraightShooter)Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
		//Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
		bulletInstance.SetStartPos(bulletStartPosition);
		
		//Parent them to us, so this object can receive messages on death
		bulletInstance.transform.parent = transform;
		
	}
	
}
