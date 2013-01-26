using UnityEngine;
using System.Collections;

public class WeaponsLibrary : MonoBehaviour {
	
	public StraightShooterBlue blueBulletPrefab;
	public StraightShooterYellow yellowBulletPrefab;
	public float bulletHeight =  2.0f;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void FireStraightShooterBlue(Quaternion aimDirection)
	{
		
		Vector3 bulletStartPosition = transform.position;
		bulletStartPosition.y = bulletHeight;
		//StraightShooter clone;
		//clone = (StraightShooter)Instantiate(defaultBullet, bulletStartPosition, aimDirection);
		
		StraightShooterBlue bulletInstance = (StraightShooterBlue)Instantiate(blueBulletPrefab, bulletStartPosition, aimDirection);
		//Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
		bulletInstance.SetStartPos(bulletStartPosition);
		
		//Parent them to us, so this object can receive messages on death
		//bulletInstance.transform.parent = transform;
		
	}
	
	public void FireStraightShooterYellow(Quaternion aimDirection)
	{
		Vector3 bulletStartPosition = transform.position;
		bulletStartPosition.y = bulletHeight;
		//StraightShooter clone;
		//clone = (StraightShooter)Instantiate(defaultBullet, bulletStartPosition, aimDirection);
		
		StraightShooterYellow bulletInstance = (StraightShooterYellow)Instantiate(yellowBulletPrefab, bulletStartPosition, aimDirection);
		//Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
		bulletInstance.SetStartPos(bulletStartPosition);
		
		//Parent them to us, so this object can receive messages on death
		//bulletInstance.transform.parent = transform;
	}
	
	public void Fire(string weaponName, Quaternion aimDirection)
	{
		if(weaponName == "StraightShooterBlue")
		{
			Vector3 bulletStartPosition = transform.position;
			bulletStartPosition.y = bulletHeight;
			//StraightShooter clone;
			//clone = (StraightShooter)Instantiate(defaultBullet, bulletStartPosition, aimDirection);
			
			StraightShooterBlue bulletInstance = (StraightShooterBlue)Instantiate(blueBulletPrefab, bulletStartPosition, aimDirection);
			
			//Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
			bulletInstance.SetStartPos(bulletStartPosition);
			
			//Parent them to us, so this object can receive messages on death
			//bulletInstance.transform.parent = transform;	
		}
		else if(weaponName == "StraightShooterYellow")
		{
			Vector3 bulletStartPosition = transform.position;
			bulletStartPosition.y = bulletHeight;
			//StraightShooter clone;
			//clone = (StraightShooter)Instantiate(defaultBullet, bulletStartPosition, aimDirection);
			
			StraightShooterYellow bulletInstance = (StraightShooterYellow)Instantiate(yellowBulletPrefab, bulletStartPosition, aimDirection);
			//Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
			bulletInstance.SetStartPos(bulletStartPosition);
			
			//Parent them to us, so this object can receive messages on death
			//bulletInstance.transform.parent = transform;
		}
	}
	
	public int GetWeaponWeight(string weaponName)
	{
		int returnedWeight = -1;
		
		if(weaponName == "StraightShooterBlue")
		{
			returnedWeight = blueBulletPrefab.GetWeight();
		}
		else if(weaponName == "StraightShooterYellow")
		{
			returnedWeight = yellowBulletPrefab.GetWeight();	
		}
		
		return returnedWeight;
	}
}

