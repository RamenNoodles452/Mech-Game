using UnityEngine;
using System.Collections;

public class ModuleLibrary : MonoBehaviour {
	
	public AccelThrusters accelThrustersPrefab;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void EquipModule(bool equipState, string moduleName, StandardTorso torsoScript, WalkerMovement movementScript)
	{
		if(moduleName == "AccelThrusters")
		{
			if(equipState == true)
				accelThrustersPrefab.Equip(movementScript);
			else 
				accelThrustersPrefab.Unequip(movementScript);
		}
	}
	
	public int GetModuleWeight(string moduleName)
	{
		int returnedWeight = -1;
		
		if(moduleName == "AccelThrusters")
		{
			returnedWeight = accelThrustersPrefab.GetWeight();
		}

		return returnedWeight;
	}
	
}
