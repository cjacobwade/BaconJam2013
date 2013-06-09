using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {
	
	public GameObject player;
	public GameObject bulb;
	
	public AudioSource voice;
	public AudioClip[] sound;
	
	public float maxDistance;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		print(Vector3.Distance(transform.position,player.transform.position));
		bulb.transform.Rotate(Vector3.forward,.7f);
		if(Vector3.Distance(transform.position,player.transform.position) < maxDistance)
		{
			voice.clip = sound[0];
			if(!voice.audio.isPlaying)
				voice.Play();
		}
		
	}
}
