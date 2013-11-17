using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GestureState{None, Possible, Began, Changed, Recognized, Failed, Canceled};

public abstract class UIGestureRecognizer : MonoBehaviour {

	//Class Variables
	private static List<UIGestureRecognizer> recognizers = new List<UIGestureRecognizer>();
	private static int lastUpdateFrame = -1;
	protected static UIGestureRecognizer[] currentTargets = new UIGestureRecognizer[3];
	// private static int[] inputDepth = new int[3];
	private static UIGestureRecognizer currentHover;

	private static UIGestureRecognizer activeGesture;



	//Instance Variables
	[SerializeField] private bool allowSimultaneousRecognition = false;
	[SerializeField] protected bool onlyIfElementIsTarget = false; 
	protected UITransformNode uiTransformNode;
	private int id = 0;
	protected GestureState state;

	private int lastHoverFrame = -1;
	

	protected bool[] inputInsideTarget = new bool[3];
	
	protected bool sendStateChangeMessage = false;

	//input type - none, hover, key down, key held, key up

	public static UIGestureRecognizer CurrentTarget{
		get{ return currentTargets[0]; }
	}


	public event System.Action<int> OnGesturePossible;
	public event System.Action<int> OnGestureFailed;
	public event System.Action<int> OnGestureRecognized;

	public event System.Action<int> OnGestureBegan;  //continuous only
	public event System.Action<int> OnGestureChanged;	//continuous only
	public event System.Action<int> OnGestureCanceled; //continuous only

	public event System.Action<int> OnHoverEnter;
	public event System.Action<int> OnHoverExit;

	public GestureState State{
		get{ return state; }
	}

	public bool Hover{
		get{
			return (lastHoverFrame == lastUpdateFrame);
		}
	}

	public int Id{
		get{ return id;}
		set{ id = value;}
	}

	public bool AllowSimultaneousRecognition{
		get{ return allowSimultaneousRecognition;}
	}

	protected UITransformNode UITransformNode{
		get{ return uiTransformNode; }
	}

	void Awake(){
		uiTransformNode = GetComponent<UITransformNode>();
	}
	
	void OnEnable(){
		recognizers.Add(this);
	}
	
	protected virtual void OnDisable(){
		recognizers.Remove(this);
		for(int i=0; i<inputInsideTarget.Length; i++){
			inputInsideTarget[i] = false;
		}
		OverrideGesture();
		if(sendStateChangeMessage){
			SignalState();
		}

	}
	

	
	public static void UpdateGestures(){
		if(lastUpdateFrame != Time.frameCount){
			UpdateRecognizers();
		}
	}


	void HoverEnter(){
		if(OnHoverEnter != null){
			OnHoverEnter(id);
		}
	}

	void HoverExit(){
		if(OnHoverExit != null){
			OnHoverExit(id);
		}
	}

	private void SignalState(){
		//Debug.Log(state + " : ");
		switch(state){
			case GestureState.None:
			break;
			case GestureState.Possible:
				if(OnGesturePossible != null){
					OnGesturePossible(id);
				}
				break;
			case GestureState.Began:
				if(OnGestureBegan != null){
					OnGestureBegan(id);
				}
				break;
			case GestureState.Changed:
				if(OnGestureChanged != null){
					OnGestureChanged(id);
				}
				break;
			case GestureState.Recognized:
				if(OnGestureRecognized != null){
					OnGestureRecognized(id);
				}
				break;
			case GestureState.Failed:
				if(OnGestureFailed != null){
					OnGestureFailed(id);
				}
				break;
			case GestureState.Canceled:
				if(OnGestureCanceled != null){
					OnGestureCanceled(id);
				}
				break;
		}
	}

	protected abstract void Sample();
	protected abstract void OverrideGesture();

	/*public static int CompareGestureDepth(UIGestureRecognizer x, UIGestureRecognizer y)
    {
        return x.uiTransformNode.WorldDepth.CompareTo(y.uiTransformNode.WorldDepth);
    }*/

	
	/********************************************//**
	*  Update the gestures
 	***********************************************/

 	//find the current target node
 	//update the gestures
 	//gestures report their state, signal events
 	//gestures optionally aren't cleared


 	static void RecognizerSuccess(UIGestureRecognizer winner){
 		for(int i=0; i<recognizers.Count; i++){
 			if(recognizers[i] != winner){
				recognizers[i].OverrideGesture();
			}
		}
 	}


	static void UpdateRecognizers(){
		lastUpdateFrame = Time.frameCount;
		
		
			//recognizers.Sort(CompareGestureDepth);
		

		GetCurrentTarget();
		CheckGestures();
		

	}


	 static void GetCurrentTarget(){



	 	//we cache the current targets for each finger, useful for drag, non-hover
		for(int i=0; i<currentTargets.Length; i++){

			currentTargets[i] = null;
			
				for(int j=0; j<recognizers.Count; j++){
					UIGestureRecognizer r = recognizers[j];
					
					if(i < InputHandling.PrimaryInputCount ){

						r.inputInsideTarget[i] = r.uiTransformNode.HitTest(InputHandling.inputs[i].InputPosition);
						if(r.inputInsideTarget[i]){
							// Debug.Log(r + ":" + r.uiTransformNode.WorldDepth);
							if( currentTargets[i] == null || r.uiTransformNode.WorldDepth > currentTargets[i].uiTransformNode.WorldDepth) {
								currentTargets[i] = r;
							}

						} 

					}
					else{
						r.inputInsideTarget[i] = false;
					}
				}
			


		}

		 // Debug.Log(currentTargets[0]);
		//for desktop, we get the hover
		if(!InputHandling.AnyMouseHeld){
			if(currentHover != currentTargets[0]){
				if(currentHover != null){
					currentHover.HoverExit();
				}

				currentHover = currentTargets[0];
				if(currentHover != null){
					currentHover.lastHoverFrame = Time.frameCount;
				}

				if(currentHover != null){
					currentHover.HoverEnter();
				}
			}
		}
		else if(currentHover != null){
			currentHover.HoverExit();
			currentHover = null;
		}
 	}

 	static void CheckGestures(){
		UIGestureRecognizer highestGesture = null;

		//Update the active gesture first, to see if it is still active
		UIGestureRecognizer lastActiveGesture = activeGesture;

		if(activeGesture != null){
			activeGesture.Sample();
			if(activeGesture.state == GestureState.Possible || activeGesture.state == GestureState.Failed || activeGesture.state == GestureState.Canceled ||activeGesture.state == GestureState.None){
				
				activeGesture = null;
			}
		}

		for(int i=0; i<recognizers.Count; i++){

			//since we sampled the activeGesture already, and it may be null, don't sample it again.

			if(lastActiveGesture != recognizers[i]){
				//If there isn't already an active gesture, we check all, otherwise, we only update gestures that allow simultaneous recognition, however they won't replace the active gesture
				if(activeGesture == null){

					recognizers[i].Sample();

					GestureState sampleState = recognizers[i].state;
					if(highestGesture == null || recognizers[i].uiTransformNode.WorldDepth > highestGesture.uiTransformNode.WorldDepth){
						if(sampleState == GestureState.Began || sampleState == GestureState.Changed || sampleState == GestureState.Recognized){
							highestGesture = recognizers[i];
						}
					}
				}
				else if(recognizers[i].AllowSimultaneousRecognition){
					
					
					
						recognizers[i].Sample();
					
				}
			}
			
		}


		//Assign a new active gesture
		if(highestGesture != null){
			activeGesture = highestGesture;
		}


		for(int i=0; i<recognizers.Count; i++){

			if(activeGesture != null && activeGesture != recognizers[i] && !recognizers[i].AllowSimultaneousRecognition){
				// Debug.Log(activeGesture);
				recognizers[i].OverrideGesture();
			}

			if(recognizers[i].sendStateChangeMessage){
				recognizers[i].SignalState();
			}
		}


	}
}
