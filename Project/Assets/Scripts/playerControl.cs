using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
	
	//Objects
	public GameObject cube;
	public Camera camera1;
	
	//Character Control
		public int moveSpeed;
		public int rotateSpeed;
		public Vector3 moveDirection;
	
		//Jumping
		public int jumpSpeed;
		public int gravity;
		private bool isGrounded;

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		PlayerControl();
		if (isGrounded) 
			IsGrounded();
		else
			moveDirection.y -= gravity * Time.deltaTime;
		transform.Translate(moveDirection*Time.deltaTime);
	}
	
	void IsGrounded() //When on the ground
	{
		print("Is Grounded");
        moveDirection = new Vector3(0, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
        if (Input.GetKey(KeyCode.Space))
            moveDirection.y = jumpSpeed;
	}
	
	void OnTriggerEnter(Collider other)//On collision with stuff
	{
		if(other.gameObject.tag == "Ground")//if the ground
			isGrounded = true;
	}
	
	void OnTriggerExit(Collider other)//If leaving collision with stuff
	{
		if(other.gameObject.tag == "Ground")
			isGrounded = false;
	}	
	
	void PlayerControl()//Control Player
	{
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
		}			
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(0,0,-moveSpeed)*Time.deltaTime);
		}
	}	
}
