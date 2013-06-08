using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
	
	//Objects
	public GameObject cube;
	public Camera camera1;
	
	//Character Control
		//Movement
		public int moveSpeed;
		public int rotateSpeed;
		public Vector3 moveDirection;
	
		//Jumping
		public int jumpSpeed;
		public int gravity;
		private bool isGrounded;
	
		//Throwing
		public int heldBulbs;
	
	//Camera Control
		public float cameraRotationX = 2.0F;
    	public float cameraRotationY = 2.0F;
		private float cameraInit;
		public float cameraSpeed;
		public float yRot;
        public float zRot;

	
	// Use this for initialization
	void Start () {
		cameraInit = camera1.transform.rotation.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		CameraControl();
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
			transform.Translate(new Vector3(-moveSpeed*.8f,0,0)*Time.deltaTime);
		}	
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(0,0,moveSpeed*.7f)*Time.deltaTime);
		}			
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(0,0,-moveSpeed*.7f)*Time.deltaTime);
		}
		if(Input.GetMouseButtonDown(0))
		{
			if(heldBulbs > 0)
			{
				//Stop moving
				//Draw decal	
			}
			else
			{
				//Display message saying no plants held
				//Play error sound
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			if(heldBulbs > 0)
			{
				//Shoot Projectile
				//Play soun
			}
		}
	}
	
	void CameraControl()
	{
		yRot = Input.GetAxis("Mouse X");
        zRot = Input.GetAxis("Mouse Y");
        camera1.transform.RotateAround(cube.transform.position,new Vector3(0, yRot, 0),cameraSpeed);
		Ray cameraDirection = new Ray(camera1.transform.position,camera1.transform.eulerAngles);
		cube.transform.LookAt(cameraDirection.direction,Vector3.zero);

	}
}
