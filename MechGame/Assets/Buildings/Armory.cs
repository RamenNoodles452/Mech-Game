using UnityEngine;
using System.Collections;

public class Armory : MonoBehaviour {
	
	CapsuleCollider shopPurchaseRadius;
	private int playersInProximity;
	
	public GameObject armoryModel;
	
	Color standardColor = new Color(1.0f, 1.0f, 1.0f);
	Color purchaseColor = new Color(1.0f, 0.0f, 1.0f);
	
	// Use this for initialization
	void Start () 
	{
		shopPurchaseRadius = GetComponent<CapsuleCollider>();
		shopPurchaseRadius.isTrigger = true;
		playersInProximity = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(playersInProximity == 0)
		{
			armoryModel.renderer.material.color = standardColor;
		}
		else
		{
			armoryModel.renderer.material.color = purchaseColor;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.SendMessage("ProvideArmoryAccess", true);
			playersInProximity++;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.SendMessage("ProvideArmoryAccess", false);
			playersInProximity--;
		}	
	}
	
}