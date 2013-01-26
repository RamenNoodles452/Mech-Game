using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestWeaponSystem : MonoBehaviour {
	
	public WalkerMovement movementSystem;
	public StandardTorso chassisSystem;
	
	public StraightShooter bulletPrefab;
	public float bulletHeight =  2.0f;
	public int maximumWeightCapacity = 20;
	public int currentWeight;
	
	
	public string[] weaponList; // the list of weapon slots,null value means weapon is not assigned. The string value is the name of the weapon
	public string[] moduleList; // the list of module slots, null value means no module is assigned. The string value is the name of the module
	public int primaryWeaponIndex; // the currently equipped primary weapon, -1 indicates no primary weapon available
	public int moduleIndex; // the currently equiped module. -1 indicates no module available
	
	//associate the Weapon Library
	private WeaponsLibrary weaponLibrary;
	private ModuleLibrary moduleLibrary;
	
	// Use this for initialization
	void Start () 
	{
		//set the starting weight at 0
		currentWeight = 0;
		
		//instantiate the weaponLibrary
		weaponLibrary = GetComponent<WeaponsLibrary>();
		moduleLibrary = GetComponent<ModuleLibrary>();
		
		//weaponPairList = new KeyValuePair<int, bool>[4] {{1, true}, {2, true}, {3, false}, {4, false}};
		weaponList = new string[4] {null, null, null, null};
		moduleList = new string[6] {null, null, null, null, null, null};
		
		//add 2 basic weapons for testing
		AddWeaponToInventory(-1, "StraightShooterBlue");
		AddWeaponToInventory(-1, "StraightShooterYellow");
		//RemoveWeaponFromInventory("StraightShooterBlue");
		
		//add a module for testing
		AddModuleToInventory(-1, "AccelThrusters");
		//RemoveModulesFromInventory("AccelThrusters");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// increment the index if the weapon doesn't exist in the inventory anymore.
		// -1 is no weapon at all.
		if(primaryWeaponIndex == 0 && weaponList[0] == null)
			primaryWeaponIndex++;	
		if(primaryWeaponIndex == 1 && weaponList[1] == null)
			primaryWeaponIndex++;	
		if(primaryWeaponIndex == 2 && weaponList[2] == null)
			primaryWeaponIndex++;	
		if(primaryWeaponIndex == 3 && weaponList[3] == null)
			primaryWeaponIndex = -1;
		
		// increment the index if the module doesn't exist in the inventory anymore.
		// -1 is no module at all
		if(moduleIndex == 0 && moduleList[0] == null)
			moduleIndex++;
		if(moduleIndex == 1 && moduleList[1] == null)
			moduleIndex++;
		if(moduleIndex == 2 && moduleList[2] == null)
			moduleIndex++;
		if(moduleIndex == 3 && moduleList[3] == null)
			moduleIndex++;
		if(moduleIndex == 4 && moduleList[4] == null)
			moduleIndex++;
		if(moduleIndex == 5 && moduleList[5] == null)
			moduleIndex = -1;
		
		//input for swapping weapons!
		if(Input.GetButtonDown("Weapon Slot 1"))
			SetWeapon(0);
		if(Input.GetButtonDown("Weapon Slot 2"))
			SetWeapon(1);
		if(Input.GetButtonDown("Weapon Slot 3"))
			SetWeapon(2);
		if(Input.GetButtonDown("Weapon Slot 4"))
			SetWeapon(3);
		
		//input for swapping modules!
		if(Input.GetButtonDown("Module Slot 1"))
			SetModule(0);
		if(Input.GetButtonDown("Module Slot 2"))
			SetModule(1);
		if(Input.GetButtonDown("Module Slot 3"))
			SetModule(2);
		if(Input.GetButtonDown("Module Slot 4"))
			SetModule(3);
		if(Input.GetButtonDown("Module Slot 5"))
			SetModule(4);
		if(Input.GetButtonDown("Module Slot 6"))
			SetModule(5);

	}
	
	// -1 is default weaponSlotNumber value. It will add the weapon to the first slot available
	// any other number assigns the weapon to the slot assigned. failing if a weapon already exists in the specified slot.
	void AddWeaponToInventory(int weaponSlotNumber, string weaponName)
	{
		int weaponWeight = weaponLibrary.GetWeaponWeight(weaponName);
		
		if(weaponWeight == -1)
		{
			Debug.LogError("WEAPON HAS NO WEIGHT");	
		}

		if(weaponWeight + currentWeight < maximumWeightCapacity)
		{
			//the defauly behavior of adding a weapon
			if(weaponSlotNumber == -1)
			{
				int weaponSlotIndex;
				for(weaponSlotIndex = 0; weaponSlotIndex < weaponList.Length; weaponSlotIndex++)
				{
					if(weaponList[weaponSlotIndex] == null)
					{
						weaponList[weaponSlotIndex] = weaponName;
						currentWeight = currentWeight + weaponWeight;
						return;
					}
				}
				if(weaponSlotIndex == weaponList.Length)
				{
					Debug.Log("No available Weapon Slot for " + weaponName);
				}
			}
			//specifically choosing a slot
			else
			{
				if(weaponList[weaponSlotNumber] == null)
				{
					weaponList[weaponSlotNumber] = weaponName;
					currentWeight = currentWeight + weaponWeight;
				}
				else
				{
					Debug.Log("A WEAPON EXISTS IN THE SLOT ALREADY");	
				}
			}
		}
		else
		{
			Debug.Log("Not enough capacity to purchase weapon!");	
		}
	}
	
	void RemoveWeaponFromInventory(string weaponName)
	{
		int weaponSlotIndex;
		for(weaponSlotIndex = 0; weaponSlotIndex < weaponList.Length; weaponSlotIndex++)
		{
			if(weaponList[weaponSlotIndex] == weaponName)
			{
				weaponList[weaponSlotIndex] = null;
				currentWeight = currentWeight - weaponLibrary.GetWeaponWeight(weaponName);
				return;
			}
		}
		Debug.Log("The weapon " + weaponName + " you're trying to remove doesn't exist in the inventory!");
	}
	
	void AddModuleToInventory(int moduleSlotNumber, string moduleName)
	{
		int moduleWeight = moduleLibrary.GetModuleWeight(moduleName);
		
		if(moduleWeight == -1)
		{
			Debug.LogError("MODULE HAS NO WEIGHT");	
		}
		if(moduleWeight + currentWeight < maximumWeightCapacity)
		{
			//the defauly behavior of adding a weapon
			if(moduleSlotNumber == -1)
			{
				int moduleSlotIndex;
				for(moduleSlotIndex = 0; moduleSlotIndex < moduleList.Length; moduleSlotIndex++)
				{
					if(moduleList[moduleSlotIndex] == null)
					{
						moduleList[moduleSlotIndex] = moduleName;
						moduleLibrary.EquipModule(true, moduleName, chassisSystem, movementSystem);
						currentWeight = currentWeight + moduleWeight;
						return;
					}
				}
				if(moduleSlotIndex == moduleList.Length)
				{
					Debug.Log("No available Module Slot for " + moduleName);
				}
			}
			//specifically choosing a slot
			else
			{
				if(moduleList[moduleSlotNumber] == null)
				{
					moduleList[moduleSlotNumber] = moduleName;
					currentWeight = currentWeight + moduleWeight;
					moduleLibrary.EquipModule(true, moduleName, chassisSystem, movementSystem);
				}
				else
				{
					Debug.Log("A MODULE EXISTS IN THE SLOT ALREADY");	
				}
			}
		}
		else
		{
			Debug.Log("Not enough capacity to purchase weapon!");
		}
	}
	
	void RemoveModulesFromInventory(string moduleName)
	{
		int moduleSlotIndex;
		for(moduleSlotIndex = 0; moduleSlotIndex < moduleList.Length; moduleSlotIndex++)
		{
			if(moduleList[moduleSlotIndex] == moduleName)
			{
				moduleList[moduleSlotIndex] = null;
				currentWeight = currentWeight - moduleLibrary.GetModuleWeight(moduleName);
				moduleLibrary.EquipModule(false, moduleName, chassisSystem, movementSystem);
				return;
			}
		}
		Debug.Log("The weapon " + moduleName + " you're trying to remove doesn't exist in the inventory!");
	}
	
	void SetWeapon(int slotSelection)
	{
		if(weaponList[slotSelection] != null)
			primaryWeaponIndex = slotSelection;
		else
			Debug.Log("There's no weapon in Weapon Slot " + slotSelection + "!");
	}
	
	void SetModule(int slotSelection)
	{
		if(moduleList[slotSelection] != null)
			moduleIndex = slotSelection;	
		else
			Debug.Log("There is no module in Module Slot " + slotSelection + "!");
	}
	
	public void FirePrimary(Quaternion aimDirection)
	{
		if(primaryWeaponIndex != -1 && weaponList[primaryWeaponIndex] != null)
		{
			weaponLibrary.Fire(weaponList[primaryWeaponIndex], aimDirection);
			
			//incredibly inefficient method
			//gameObject.SendMessage("Fire" + weaponList[primaryWeaponIndex], aimDirection);			
		}
	}
	public void FireSecondary(Quaternion aimDirection)
	{

		Vector3 bulletStartPosition = transform.position;
		bulletStartPosition.y = bulletHeight;
		
		StraightShooter bulletInstance = (StraightShooter)Instantiate(bulletPrefab, bulletStartPosition, aimDirection);
		bulletInstance.SetStartPos(bulletStartPosition);
		
		//Parent them to us, so this object can receive messages on death
		//LOL JK this makes it so that the bullets move with the player
		//bulletInstance.transform.parent = transform;
	}
}
