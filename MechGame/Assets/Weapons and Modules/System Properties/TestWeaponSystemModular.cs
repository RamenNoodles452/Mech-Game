using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestWeaponSystemModular : MonoBehaviour {
	
	
	public float bulletHeight =  2.0f;
	
	//private KeyValuePair<int, bool>[] weaponPairList;
	private bool[] weaponList;
	private int primaryWeapon;
	private int secondaryWeapon;
	
	public StraightShooter straightShooterPrefab;
	
	public StraightShooter straightShooterYellowPrefab;
	
	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent("TestStraightShooter");
		gameObject.GetComponent<TestStraightShooter>().bulletPrefab = straightShooterPrefab;
		
		//gameObject.AddComponent("TestStraightShooterYellow");
		//gameObject.GetComponent<Test
		
		//weaponPairList = new KeyValuePair<int, bool>[4] {{1, true}, {2, true}, {3, false}, {4, false}};
		weaponList = new bool[4] {true, true, false, false};
		setWeapon(0, 0);
		setWeapon(1, 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	//weapon type 0 = primary weapon
	//weapon type 1 = secondary weapon
	void setWeapon(int weaponType, int slotSelection)
	{
		if(weaponList[slotSelection] == true)
		{
			if(weaponType == 0)
			{
				primaryWeapon = slotSelection;
			}
			else if(weaponType == 1)
			{
				secondaryWeapon = slotSelection;	
			}
		}
	}
	
	public void FirePrimary(Quaternion aimDirection)
	{
		if(primaryWeapon == 0)
		{
			

			
		}
	}
	public void FireSecondary(Quaternion aimDirection)
	{
		if(secondaryWeapon == 1)
		{

		}
	}
}
