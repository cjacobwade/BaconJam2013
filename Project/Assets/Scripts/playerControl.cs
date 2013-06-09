using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
	
	//Objects
	public Camera camera1;
	public GameObject model;
	public GameObject bulb;
	public GameObject hand;
	public GameObject handOrb;
	public GameObject[] clone;
		
	//Animations
	public float[] animSpeed;
	
	#region Camera Control
		public float cameraSpeed;//How fast does the cam rotate
		public float minCameraX;//Max camera vertical rotation
		public float maxCameraX;//Min camera vertical rotation
		public float cameraRotationX = 0;//What is the camera's rotation right now (relative to 0)
		private float yRot;
        private float zRot;
	#endregion
	
	#region Character Control
		//Movement
		public int moveSpeed;
		public int rotateSpeed;
		private Vector3 moveDirection;
	
		//Jumping
		public int jumpSpeed;
		public int gravity;
		private bool isGrounded;
	
		//Throwing
		public float throwHeight;
		public int heldBulbs;
		public int orbIndex = 0;
		public int orbNumber;
		private Ray aim;
		private RaycastHit hit;
		int layerMask = 1 << 9;
		public float throwSpeed;
		private bool isAiming = false;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(transform.position.y < -50)
			Application.LoadLevel(0);
		moveDirection.y -= gravity * Time.deltaTime;
		model.animation[ "Walk" ].speed = animSpeed[0];
		aim = Camera.main.ScreenPointToRay(Input.mousePosition);
		CameraControl();
		PlayerControl();
//		if(heldBulbs > 0)
//			handOrb.gameObject.renderer.enabled = true;
//		else
//			handOrb.gameObject.renderer.enabled = false;
		if (isGrounded)
		{
			Grounded();
		}
		else
			
		transform.Translate(moveDirection*Time.deltaTime);
	}
	
	void CameraControl()//Controls camera view
	{
		yRot = Input.GetAxis("Mouse X");
		
		//Rotate Player Controller
		transform.Rotate(new Vector3(0,Time.deltaTime*yRot*rotateSpeed,0));
		transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
		
		//Rotate Player Model
		model.transform.Rotate(new Vector3(0,Time.deltaTime*yRot*rotateSpeed,0));
		model.transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
		
		CameraMinMax();//Vertical axis controls
	}
	
	void CameraMinMax()//Correct vertical camera to stay within bounds
	{
		if(Input.GetAxis("Mouse Y")>.5)
			zRot=.5f;
		else if(Input.GetAxis("Mouse Y")<-.5)
			zRot=-.5f;
		else
			zRot = Input.GetAxis("Mouse Y");
		
		if(cameraRotationX >= minCameraX && cameraRotationX <= maxCameraX)
			{
				cameraRotationX += zRot;
				camera1.transform.Rotate(new Vector3(Time.deltaTime*zRot*-rotateSpeed,0,0));
			}
		if(cameraRotationX < minCameraX)//if lower than min rotation, correct
			{
				camera1.transform.Rotate(new Vector3(Time.deltaTime*.6f*-rotateSpeed,0,0));
				cameraRotationX += Time.deltaTime*-.3f*-rotateSpeed;
			}
		if(cameraRotationX > maxCameraX)//if higher than max rotation, correct
			{
				cameraRotationX += Time.deltaTime*.3f*-rotateSpeed;
				camera1.transform.Rotate(new Vector3(Time.deltaTime*-.6f*-rotateSpeed,0,0));
			}	
	}
	
	void PlayerControl()//Control Player
	{
		if(!isAiming)
		{
			if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
			{
				if(isGrounded)
				{
					model.animation.Play("Walk");
					if(heldBulbs > 0)
						{
							handOrb.gameObject.renderer.enabled = true;
							handOrb.gameObject.light.enabled = true;
						}
				}
			}
			else
			{
				if(isGrounded)
				{
					if(model.animation["Walk"].enabled||model.animation["JumpPose"].enabled||!model.animation.isPlaying)
					{
						model.animation.Play("Idle");
						if(heldBulbs > 0)
						{
							handOrb.gameObject.renderer.enabled = true;
							handOrb.gameObject.light.enabled = true;
						}
					}
				}
				if(!isGrounded)
				{
					isAiming = false;
					if(!model.animation.isPlaying)
						model.animation.Play("JumpPose");
				}
			}
			#region WASD
			if(Input.GetKey(KeyCode.W))
				transform.Translate(new Vector3(moveSpeed,0,0)*Time.deltaTime);
			
			if(Input.GetKey(KeyCode.S))
			{
				transform.Translate(new Vector3(-moveSpeed*.8f,0,0)*Time.deltaTime);
				model.animation["Walk"].speed = -animSpeed[0];
			}
			
			if(Input.GetKey(KeyCode.A))
				transform.Translate(new Vector3(0,0,moveSpeed*.7f)*Time.deltaTime);	
			
			if(Input.GetKey(KeyCode.D))
				transform.Translate(new Vector3(0,0,-moveSpeed*.7f)*Time.deltaTime);
			#endregion
		}
		
		#region Mouse
		
		if(Input.GetMouseButton(0))
		{
			rigidbody.velocity = Vector3.zero;
			Windup();
		}

		else
		{
			if(isAiming)
				ThrowBulb();
		}
		#endregion
	}
	
	void Windup()
	{
		if(heldBulbs > 0)
		{
			
			handOrb.gameObject.renderer.enabled = true;
				handOrb.gameObject.light.enabled = true;
			if(!isAiming)
				model.animation.Play("Windup");
			isAiming = true;
			if(throwHeight < 10)
				throwHeight += .13f;
			//Stop moving
			if(model.animation["JumpPose"].enabled||!model.animation.isPlaying)
				model.animation.Play("WindupPose");
			
			//Draw decal	
		}
		else
		{
			//Display message saying no plants held
			//Play error sound
		}
	}
	
	void ThrowBulb()
	{
		if(orbIndex >= orbNumber-1)
			orbIndex = 0;
		else if(orbIndex < orbNumber-1)
			orbIndex++;
		if(Physics.Raycast(aim,out hit,Mathf.Infinity,layerMask))//For this to land, there needs to be colliders on the other objects
			{
				print(Input.mousePosition);
			}
		if(heldBulbs > 0)
		{
			isAiming = false;
			//Shoot Projectile
			model.animation[ "Throw" ].speed = animSpeed[3];
			model.animation.Play("Throw");
			Destroy(clone[orbIndex]);
			clone[orbIndex] = Instantiate(bulb,hand.transform.position,transform.rotation) as GameObject;
			if(throwHeight < .7f)
				clone[orbIndex].rigidbody.velocity = transform.TransformDirection(new Vector3(1,0,0));
			else
				clone[orbIndex].rigidbody.velocity = transform.TransformDirection(new Vector3(1,throwHeight/10,0) *throwSpeed);
			handOrb.gameObject.renderer.enabled = false;
			handOrb.gameObject.light.enabled = false;
			throwHeight = .001f;
			Physics.IgnoreCollision(clone[orbIndex].collider, this.collider);
			heldBulbs--;
			//Play sound
		}
	}
	
	void Grounded() //When on the ground
	{
		//print("Is Grounded");
        moveDirection = new Vector3(0, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
        if (Input.GetKey(KeyCode.Space))
		{
			isGrounded = false;
            moveDirection.y = jumpSpeed;
			model.animation.Play("Jump");
		}
	}
	
	#region COLLISIONS
	void OnTriggerEnter(Collider other)//On collision with stuff
	{
		if(other.gameObject.tag == "Ground")//if the ground
			isGrounded = true;
		if(other.gameObject.tag == "Wall")
			moveDirection.y -= gravity * Time.deltaTime;
		
		if(other.gameObject.tag == "Bush")
		{
			if(heldBulbs < 5)
				heldBulbs++;
		}
	}
	
	void OnTriggerExit(Collider other)//If leaving collision with stuff
	{
		if(other.gameObject.tag == "Ground")
			isGrounded = false;
		moveDirection.y -= gravity * Time.deltaTime;
		rigidbody.velocity = Vector3.zero;
	}	
	#endregion
}
