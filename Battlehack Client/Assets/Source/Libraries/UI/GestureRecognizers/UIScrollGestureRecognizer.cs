using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ScrollMode{Vertical, Horizontal, Both};

public class UIScrollGestureRecognizer : UIGestureRecognizer {

	//[SerializeField] private InputDetectionMode inputDetectionMode = InputDetectionMode.Left;

	[SerializeField] private ScrollMode scrollMode = ScrollMode.Vertical;

	[SerializeField] private bool allowMultipleFingers = false; 
	[SerializeField] private float minimumScrollDistance = 5;


	private bool[] keyDownInElement = new bool[3];
	private int scrollInput = -1;


	public Vector2 InputPosition{
		get{
			if(scrollInput != -1){
				
					return new Vector2(InputHandling.inputs[scrollInput].InputPosition.x/UICamera.UIScale,UICamera.UIScreenHeight-InputHandling.inputs[scrollInput].InputPosition.y/UICamera.UIScale);
			}
			
			return new Vector2(0,0);
		}
	}

	public Vector2 RawScrollAmount{
		get{
			return new Vector2(InputHandling.inputs[scrollInput].KeyDeltaPosition.x/UICamera.UIScale,-InputHandling.inputs[scrollInput].KeyDeltaPosition.y/UICamera.UIScale);
		}
	}

	public Vector2 ScrollAmount{
		get{
			if(scrollInput != -1){
				if(scrollMode == ScrollMode.Vertical){
					return new Vector2(0,-InputHandling.inputs[scrollInput].KeyDeltaPosition.y/UICamera.UIScale);
				}
				else if(scrollMode == ScrollMode.Horizontal){
					return new Vector2(InputHandling.inputs[scrollInput].KeyDeltaPosition.x/UICamera.UIScale,0);
				}
				else if(scrollMode == ScrollMode.Both){
					return new Vector2(InputHandling.inputs[scrollInput].KeyDeltaPosition.x/UICamera.UIScale,-InputHandling.inputs[scrollInput].KeyDeltaPosition.y/UICamera.UIScale);
				}
			}
			return new Vector2(0,0);
		}
	}
	private bool CheckScrollDelta(PrimaryInput inp){
		
			if(scrollMode == ScrollMode.Vertical){
				return (Mathf.Abs(inp.KeyDeltaPosition.y) > 0);
			}
			else if(scrollMode == ScrollMode.Horizontal){
				return (Mathf.Abs(inp.KeyDeltaPosition.x) > 0);
			}
			else if(scrollMode == ScrollMode.Both){
				return (Mathf.Abs(inp.KeyDeltaPosition.x) > 0 || Mathf.Abs(inp.KeyDeltaPosition.y) > 0);
			}
			return false;
	}

	private bool CheckScrollDistance(PrimaryInput inp){
		
			if(scrollMode == ScrollMode.Vertical){
				return (Mathf.Abs(inp.KeyDragDistance.y) > minimumScrollDistance);
			}
			else if(scrollMode == ScrollMode.Horizontal){
				return (Mathf.Abs(inp.KeyDragDistance.x) > minimumScrollDistance);
			}
			else if(scrollMode == ScrollMode.Both){
				return (Mathf.Abs(inp.KeyDragDistance.x) > minimumScrollDistance || Mathf.Abs(inp.KeyDragDistance.y) > minimumScrollDistance);
			}
			return false;
	}

	private float ScrollDelta(PrimaryInput inp){
		
			if(scrollMode == ScrollMode.Vertical){
				return Mathf.Abs(inp.KeyDeltaPosition.y);
			}
			else if(scrollMode == ScrollMode.Horizontal){
				return Mathf.Abs(inp.KeyDeltaPosition.x);
			}
			else if(scrollMode == ScrollMode.Both){
				return inp.KeyDeltaPosition.magnitude;
			}
			return 0;
	}

	protected override void OnDisable(){
		scrollInput = -1;
		for(int i=0; i<keyDownInElement.Length; i++){
			keyDownInElement[i] = false;
		}
		state = GestureState.Possible;
		base.OnDisable();
	}

	protected override void Sample(){
		
		sendStateChangeMessage = false;

		if(state == GestureState.Recognized || state == GestureState.Failed || state == GestureState.Canceled || state == GestureState.None){
			state = GestureState.Possible;
			sendStateChangeMessage = true;
			//SignalState();
		}

		
		float maxDelta = 0;
		int maxDeltaId = -1;

		for(int i=0; i<InputHandling.inputs.Length; i++){


		
			if(InputHandling.inputs[i].KeyDown){
				if(inputInsideTarget[i]){
					if(!onlyIfElementIsTarget || currentTargets[i] == this){
						keyDownInElement[i] = true;
					}
					//Debug.Log("keydown");
				}
			}
			else if(InputHandling.inputs[i].KeyHeld){
				if(keyDownInElement[i]){

					float delta = ScrollDelta(InputHandling.inputs[i]);
					if(delta > maxDelta){
						maxDelta = delta;
						maxDeltaId = i;
					}
				}
			}
			else{
				keyDownInElement[i] = false;
			}
		}

		
		if(state == GestureState.Possible){
			if(maxDeltaId != -1){
				if(CheckScrollDistance( InputHandling.inputs[maxDeltaId] )){
					state = GestureState.Began;
					sendStateChangeMessage = true;
					scrollInput = maxDeltaId;
				}
			}
		}
		else if(state == GestureState.Began || state == GestureState.Changed){
			if(allowMultipleFingers && maxDeltaId != -1){
				state = GestureState.Changed;
				sendStateChangeMessage = true;
				scrollInput = maxDeltaId;
			}
			else{
				if(scrollInput != -1 && CheckScrollDelta(InputHandling.inputs[scrollInput])){
					state = GestureState.Changed;
					sendStateChangeMessage = true;
				}
			}
			
		}
		

		if(scrollInput != -1 && !InputHandling.inputs[scrollInput].KeyHeld){
			//we lost the current touch, check the other touches
			int activeInput = -1;
			for(int i=0; i<keyDownInElement.Length; i++){
				if(keyDownInElement[i]){
					activeInput = i;
				}
			}

			scrollInput = activeInput;
			if(scrollInput == -1 && (state == GestureState.Began || state == GestureState.Changed) ){
				state = GestureState.Recognized;
				sendStateChangeMessage = true;
			}
		}

		


		
	}
	



	protected override void OverrideGesture(){
		scrollInput = -1;
		for(int i=0; i<keyDownInElement.Length; i++){
			keyDownInElement[i] = false;
		}
		if(state == GestureState.Possible){
			state = GestureState.Failed;
			sendStateChangeMessage = true;
			//SignalState();
		}
		else if(state == GestureState.Began || state == GestureState.Changed){
			state = GestureState.Canceled;
			sendStateChangeMessage = true;
			//SignalState();
		}
	}
	
	//protected override void GestureComplete(){
	//	base.GestureComplete();
		//Debug.Log("click me baby" + targetNode);	
	//}
	
}
