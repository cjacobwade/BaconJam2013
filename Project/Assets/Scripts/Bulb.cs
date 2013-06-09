using UnityEngine;
using System.Collections;

public class Bulb : MonoBehaviour {
	
	public AudioSource voice;
	public AudioClip[] sound;
	
	
	// Use this for initialization
	void Start () {
	
		transform.Rotate(Random.Range(0,10),Random.Range(0,10),Random.Range(0,10));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Wall"|| other.tag == "Ground")
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.useGravity = false;
			voice.clip = sound[0];
			voice.Play();
		}
	}
}
