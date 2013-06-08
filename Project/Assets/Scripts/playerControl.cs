using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
	
	public int moveSpeed;
	public int rotateSpeed;
	
	public GameObject cube;
	public Camera camera1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(new Vector3(moveSpeed,0,0)*Time.deltaTime);
			
			
		}
			
		if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(new Vector3(-moveSpeed,0,0)*Time.deltaTime);
			
		}
				
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(0,0,moveSpeed)*Time.deltaTime);
			//transform.Rotate(Vector3.up,-rotateSpeed*Time.deltaTime); //Rotate character
		}
					
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(0,0,-moveSpeed)*Time.deltaTime);
			//transform.Rotate(Vector3.up,rotateSpeed*Time.deltaTime);
		}
	}
}
