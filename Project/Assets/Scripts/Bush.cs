using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {

	public GameObject bulb;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		bulb.transform.Rotate(Vector3.forward,.7f);
		
	}
}
