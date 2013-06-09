using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {
	
	public GameObject[] satelites;
	public float rotateSpeed;
	private float height;
	
	// Use this for initialization
	void Start () {
	
		height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
	
		for(int i=0;i < satelites.Length; i++)
		{
			satelites[i].transform.RotateAround(transform.position,new Vector3(Random.value,1,0),rotateSpeed*Time.deltaTime);
		}
		
		transform.Translate(0,Mathf.Sin(Time.time)/75,0);
	}
}
