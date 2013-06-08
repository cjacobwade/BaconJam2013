using UnityEngine;
using System.Collections;

public class shoot2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
Transform projectile;
Transform BigCube;
float bulletSpeed = 20;
 
void Update () {
    // Put this in your update function
    if (Input.GetMouseButtonDown(0)) {
 
    // Instantiate the projectile at the position and rotation of this transform
     GameObject.Instantiate(projectile,transform.forward,transform.rotation);
 
    // Add force to the cloned object in the object's forward direction
    projectile.rigidbody.AddForce(transform.forward * 20);
    }
}

}
