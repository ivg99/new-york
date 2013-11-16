using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour {

	public UITextNode text;
	
	private float displayTimer;
	//TODO - calc average	
//	private float averageFPS;
	
	void Update () {
		displayTimer += Time.deltaTime;
		if(displayTimer > 1){
			displayTimer = 0;
			int fps = (int)(1 / Time.deltaTime);
			text.Text = fps.ToString() + " fps";
		}
	}
}
