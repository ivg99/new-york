using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum InputDetectionMode{None, Left, Right, All}

public class UITapGestureRecognizer : UIGestureRecognizer {

	//[SerializeField] private InputDetectionMode inputDetectionMode = InputDetectionMode.Left;

	//protected override bool HoverDetection{
	//	get{ return true;}	
	//}
	
	//private bool keyDownInElement = false;
	
	private bool[] keyDownInElement = new bool[3];

	public bool Touch{
		get{
			for(int i=0; i<keyDownInElement.Length; i++){
			if(keyDownInElement[i]){
				return true;
			}
		}
		return false;
	}

	}

	protected override void OnDisable(){
		for(int i=0; i<keyDownInElement.Length; i++){
			keyDownInElement[i] = false;
		}
		state = GestureState.Possible;
		base.OnDisable();
	}

	protected override void Sample(){
		
		sendStateChangeMessage = false;

		if(state != GestureState.None && state != GestureState.Possible){
			state = GestureState.None;
			sendStateChangeMessage = true;
			//SignalState();
		}
		

		for(int i=0; i<InputHandling.inputs.Length; i++){
			if(InputHandling.inputs[i].KeyDown){
				if(inputInsideTarget[i]){
					if(!onlyIfElementIsTarget || currentTargets[i] == this){
						keyDownInElement[i] = true;
						if(state != GestureState.Possible){
							state = GestureState.Possible;
							sendStateChangeMessage = true;
							//SignalState();
						}
					}
					//Debug.Log("keydown");
				}
			}
			else if(InputHandling.inputs[i].KeyHeld){

			}
			else { //!keyheld aka keyup
				if(keyDownInElement[i]){
					if(inputInsideTarget[i]){
						state = GestureState.Recognized;
						sendStateChangeMessage = true;
						//SignalState();
						//Debug.Log("key up recognized");
					}
					else{
						state = GestureState.Failed;	
						sendStateChangeMessage = true;
						//SignalState();
						//Debug.Log("key up failed");
					}
				}


				keyDownInElement[i] = false;
			}
		}

		
	}
	
	protected override void OverrideGesture(){
		// Debug.Log("huh?");
	 	for(int i=0; i<keyDownInElement.Length; i++){
			keyDownInElement[i] = false;
		}
		if(state == GestureState.Possible){
			state = GestureState.Failed;
			sendStateChangeMessage = true;
		}
		else if(state == GestureState.Failed){
			sendStateChangeMessage = false;
		}
	
	}
	


	//protected override void GestureComplete(){
	//	base.GestureComplete();
		//Debug.Log("click me baby" + targetNode);	
	//}
	
}
