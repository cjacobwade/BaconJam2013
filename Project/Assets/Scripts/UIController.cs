using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	
	public Texture[] splashImages;
	public GUIStyle[] buttonStyle;
	public float[] buttonY;
	public float[] buttonHeight;
	public float[] buttonWidth;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnGUI()
	{
	
		if (GUI.Button(new Rect(Screen.width/2-splashImages.Length/2,Screen.height/buttonY[0],Screen.width/buttonWidth[0],Screen.height/buttonHeight[0]),splashImages[0]))
		{
			Application.LoadLevel("Level1");
		}
		//if (GUI.Button(new Rect(Screen.width/2-splashImages.Length/2,Screen.height/buttonY[0],Screen.width/buttonWidth[0],Screen.height/buttonHeight[0]),splashImages[0]))
		{
			
		}
	}
}
