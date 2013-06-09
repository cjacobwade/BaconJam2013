using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	private bool isPressed = false;
	public GameObject lighter;
	public GameObject target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isPressed && !animation["Press"].enabled)
			animation.Play("Pressed");
	}
	
	public void IsPressed()
	{
		if(!isPressed)
			animation.Play("Press");
		lighter.light.enabled = true;
		isPressed = true;
		if(target.tag == "Door")
			target.GetComponent<Door>().OpenDoor();
	}
}
