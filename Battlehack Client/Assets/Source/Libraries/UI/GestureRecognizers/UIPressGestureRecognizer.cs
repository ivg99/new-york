using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum InputDetectionMode{None, Left, Right, All}

public class UIPressGestureRecognizer : UIGestureRecognizer {

	//[SerializeField] private InputDetectionMode inputDetectionMode = InputDetectionMode.Left;

	//protected override bool HoverDetection{
	//	get{ return true;}	
	//}
	
	//private bool keyDownInElement = false;
	
	// private bool[] keyDownInElement = new bool[3];

	protected override void OnDisable(){
		// for(int i=0; i<keyDownInElement.Length; i++){
		// 	keyDownInElement[i] = false;
		// }
		state = GestureState.Possible;
		base.OnDisable();
	}

	protected override void Sample(){
		
		sendStateChangeMessage = false;

		if(state != GestureState.Possible){
			state = GestureState.Possible;
			sendStateChangeMessage = true;
			//SignalState();
		}
		

		for(int i=0; i<InputHandling.inputs.Length; i++){
			if(InputHandling.inputs[i].KeyDown){
				if(inputInsideTarget[i]){
					if(!onlyIfElementIsTarget || currentTargets[i] == this){
						// keyDownInElement[i] = true;
						state = GestureState.Recognized;
						sendStateChangeMessage = true;
					}
					//Debug.Log("keydown");
				}
			}
			
		}

		
	}
	
	protected override void OverrideGesture(){

	 	
	
	}
	


	//protected override void GestureComplete(){
	//	base.GestureComplete();
		//Debug.Log("click me baby" + targetNode);	
	//}
	
}
