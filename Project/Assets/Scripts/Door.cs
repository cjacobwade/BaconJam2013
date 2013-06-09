using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	public bool open = false;
	public GameObject lighter;
	public GameObject openColliders;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(open && !animation["Open"].enabled)
		{
			collider.enabled = false;
			openColliders.SetActive(true);
			rigidbody.isKinematic = false;
			animation.Play("StayOpen");
			
		}
	}
	
	public void OpenDoor()
	{
		if(!open)
		{	
			open = true; 
			animation["Open"].speed = 1;
			animation.Play("Open");
			audio.Play();
			lighter.light.enabled = true;
			
		}
	}
	
	public void CloseDoor()
	{
		if(open)
		{
			animation["Open"].speed = -1;
			animation.Play("Open");
			lighter.light.enabled = false;
			open = true; 
		}
	}
}
