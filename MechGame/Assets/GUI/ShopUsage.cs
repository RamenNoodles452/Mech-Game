using UnityEngine;
using System.Collections;

public class ShopUsage : MonoBehaviour {
	
	private bool armoryAccess;
	private bool usingArmory;
	
	public Rect armoryWindowRect = new Rect(0, 0, 500, 500);
	private Vector2 armoryScrollViewVector = Vector2.zero;
	private string innerText = "Can't drag me";
	
	private Rect exitButton = new Rect (473, 2, 25, 25);
	private Rect scrollWindowSize = new Rect(2, 35, 497, 463);
	
	
	private Vector2 scrollViewVector = Vector2.zero;
	
	// Use this for initialization
	void Start () 
	{
		//gets the correct possition of the shop depending on the window size
		float shopLeftPos = (Screen.width - armoryWindowRect.width) / 2;
		float shopTopPos = (Screen.height / 10);
		armoryWindowRect.x = shopLeftPos;
		armoryWindowRect.y = shopTopPos;
		
		armoryAccess = false;
		usingArmory = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Interaction"))
		{
			if(armoryAccess == true)
			{
				if(usingArmory == false)
				{
					usingArmory = true;
				}
				else
				{
					usingArmory = false;	
				}
				Debug.Log("OPEN SHOP MENU!");
			}
			else
			{
				Debug.Log("nope.");	
			}
		}
	}
	
	void OnGUI()
	{
		if(usingArmory == true)
		{
			armoryWindowRect = GUI.Window (0, armoryWindowRect, WindowFunction, "My Window");
		}
		
		// Begin the ScrollView
		scrollViewVector = GUI.BeginScrollView (new Rect (25, 25, 100, 100), scrollViewVector, new Rect (0, 0, 400, 400));

		// Put something inside the ScrollView
		innerText = GUI.TextArea (new Rect (0, 0, 400, 400), innerText);

		// End the ScrollView
		GUI.EndScrollView();
	}
	void WindowFunction (int windowID) 
	{
		// Draw any Controls inside the window here
		GUI.Button (exitButton, "X ");
		GUI.DragWindow(new Rect(0,0, 10000, 20));
		armoryScrollViewVector = GUI.BeginScrollView(scrollWindowSize, armoryScrollViewVector, new Rect(0, 20, 400, 1000));
		
		//put something inside the scroll view
		innerText = GUI.TextArea(new Rect(0, 20, 100, 100), innerText);
		
		//end the scrollview
		GUI.EndScrollView();
	}
	
	public void ProvideArmoryAccess(bool access)
	{
		armoryAccess = access;
	}
}
