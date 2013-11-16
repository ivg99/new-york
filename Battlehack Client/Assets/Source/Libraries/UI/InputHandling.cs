using UnityEngine;
using System.Collections;

//NOTES: 

//Script execution order requires all gestures to be included



public class InputHandling : MonoBehaviour{

	public const float STANDARD_DPI = 96;  //72 for mac?  this is bs anyway?

	public static readonly PrimaryInput[] inputs = new PrimaryInput[]{
		new PrimaryInput(KeyCode.Mouse0),
		new PrimaryInput(KeyCode.Mouse1),
		new PrimaryInput(KeyCode.Mouse2),

	};

	public static PrimaryInput LeftMouse{
		get{ return inputs[0];}
	}
	public static PrimaryInput RightMouse{
		get{ return inputs[1];}
	}
	public static PrimaryInput MiddleMouse{
		get{ return inputs[2];}
	}
	public static bool AnyMouseHeld{
		get{
		
			for(int i=0; i< inputs.Length; i++){
				if(inputs[i].KeyHeld){
					return true;
				}
			}

			return false;
		}
	}

	public static int PrimaryInputCount{
		get{ 
			 #if UNITY_EDITOR || UNITY_STANDALONE
			 	return 1;
			 #else

			if(Input.multiTouchEnabled){
				return Input.touchCount;
			}
			return 1;
			#endif
		}
	}

	

	public static float DPIRatio{
		get{
			float dpi = Screen.dpi;
			if(dpi > 0){
				return Screen.dpi / STANDARD_DPI;
			}
			return 1;
		}
	}

	public static float InchesToPixels(float inches){
		float dpi = Screen.dpi;
		if(dpi > 0){
			return inches * dpi;
		}
		else{
			return inches * STANDARD_DPI;
		}
	}
	public static float PixelsToInches(float pixelSize){
		float dpi = Screen.dpi;
		if(dpi > 0){
			return pixelSize / dpi;
		}
		else{
			return pixelSize / STANDARD_DPI;
		}
	}

	//public static readonly PrimaryInput leftMouse = new PrimaryInput(KeyCode.Mouse0);
	//public static readonly PrimaryInput rightMouse = new PrimaryInput(KeyCode.Mouse1);
	//public static readonly PrimaryInput middleMouse = new PrimaryInput(KeyCode.Mouse2);
	//public static readonly PrimaryInput mouse3 = new PrimaryInput(KeyCode.Mouse3);
	//public static readonly PrimaryInput mouse4 = new PrimaryInput(KeyCode.Mouse4);

	void LateUpdate () {

		for(int i=0; i< inputs.Length; i++){
			inputs[i].UpdateKey();
		}
		UIGestureRecognizer.UpdateGestures();
	}
	
	///Gestures
	//Tap
	//Double Tap
	//Pinch / Expand
	//Long Press
	//Swipe
	//Drag
	//Rotate
}



public class KeyInput{
	
	public KeyInput(KeyCode k){
		keyCode = k;	
	}
	
	public bool KeyHeld{
		get{
			UpdateKey();
			return keyDown;
		}
	}
	public bool KeyDown{
		get{
			UpdateKey();
			return keyDownThisFrame;
		}
	}
	public bool KeyUp{
		get{
			UpdateKey();
			return keyUpThisFrame;
		}
	}
	
	public float HeldTime{
		get{
			return keyHeldTime;
		}
	}
	
	///Convienence function for checking if the key has been held longer than the specified duration
	public bool KeyHeldDuration(float duration){
		UpdateKey();
		return(keyDown && keyHeldTime > duration);
	}
	
	public readonly KeyCode keyCode;
	
	private int lastUpdateFrame = -1;
	
	private bool keyDown = false;
	
	private bool keyDownThisFrame = false;
	private bool keyUpThisFrame = false;
	
	private float keyChangeTime = 0;
	
	private float keyHeldTime = 0;

	
	public void UpdateKey(){
		if(lastUpdateFrame != Time.frameCount){
			lastUpdateFrame = Time.frameCount;
			
			keyDownThisFrame = false;
			keyUpThisFrame = false;
			
			if(TestKey()){
				if(!keyDown){
					keyDown = true;
					keyDownThisFrame = true;
					
					keyHeldTime = 0;
					keyChangeTime = Time.realtimeSinceStartup;
					
					OnKeyDown();
				}
				else{

					keyHeldTime = Time.realtimeSinceStartup - keyChangeTime;
					
					OnKeyHeld();
					
				}
			}
			else{
				if(keyDown){
					keyDown = false;
					keyUpThisFrame = true;
					

					keyHeldTime = Time.realtimeSinceStartup - keyChangeTime;
					keyChangeTime = Time.realtimeSinceStartup;
					
					OnKeyUp();
				}	
				else{
					keyHeldTime = 0;

					OnKeyNeutral();
				}
				
			}
		}	
	}
	
	protected virtual bool TestKey(){
		
			return Input.GetKey(keyCode);
		
	}

	protected virtual void OnKeyDown(){}
	protected virtual void OnKeyHeld(){}
	protected virtual void OnKeyUp(){}
	protected virtual void OnKeyNeutral(){}
}

public sealed class PrimaryInput : KeyInput{
	
	public PrimaryInput(KeyCode k) : base(k){
		switch(k){
			case KeyCode.Mouse0:
				inputSource = 0;
				break;
			case KeyCode.Mouse1:
				inputSource = 1;
				break;
			case KeyCode.Mouse2:
				inputSource = 2;
				break;
			case KeyCode.Mouse3:
				inputSource = 3;
				break;
			case KeyCode.Mouse4:
				inputSource = 4;
				break;	
			default:
				throw new System.ArgumentOutOfRangeException("Key code must be one of the 5 touches");
				
				
		}
	}
	
	public readonly int inputSource;

	private Vector2 keyLastPosition = Vector2.zero;
	
	private Vector2 keyChangePosition = Vector2.zero;
	private Vector2 keyDragDistance = Vector2.zero;
	private Vector2 keyDeltaPosition = Vector2.zero;
	///The current velocity of key dragging
//	private Vector2 keyDragVelocity = Vector2.zero;
	
	const int SAMPLE_COUNT = 5;
	private Vector2[] keyVelocitySamples = new Vector2[SAMPLE_COUNT];
	private int sample = 0;
	

	
	///The position at the last key change (up / down) event
	public Vector2 KeyChangePosition{
		get{ 
			UpdateKey();
			return keyChangePosition;
			
		}
	}

	public Vector2 KeyDeltaPosition{
		get{ 
			UpdateKey();
			return keyDeltaPosition; 
		}
	}
	public Vector2 KeyDragDistance{
		get{ 
			UpdateKey();
			return keyDragDistance; 
		}
	}
	
	public Vector2 KeyVelocity{
		get{
			UpdateKey();
			float averageX = 0;
			float averageY = 0;
			for(int i=0; i<SAMPLE_COUNT; i++){
				averageX += keyVelocitySamples[i].x;
				averageY += keyVelocitySamples[i].y;
			}
			
			return new Vector2(averageX / (float)SAMPLE_COUNT,averageY / (float)SAMPLE_COUNT);
		}	
	}
	
	public Vector2 KeyVelocityInches{
		
		get{
			float dpi = Screen.dpi;
			if(dpi > 0){
				return KeyVelocity / dpi;
			}
			else{
				return KeyVelocity / InputHandling.STANDARD_DPI;
			}	
				
		}	
	}
	
	protected override bool TestKey(){
		// #if UNITY_ANDROID
		// 	if(AndroidInput.secondaryTouchEnabled){
		// 	 	if(AndroidInput.touchCountSecondary > inputSource){
		// 			TouchPhase phase = AndroidInput.GetSecondaryTouch(inputSource).phase;
		// 			return  (phase != TouchPhase.Canceled && phase != TouchPhase.Ended);
		// 		}
		// 		else{
		// 			return false;
		// 		}
		// 	}

		// #endif
		#if UNITY_IPHONE || UNITY_ANDROID
		if( (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) && Input.multiTouchEnabled){
			for(int i=0; i<Input.touchCount; i++){
				Touch t = Input.GetTouch(i);
				if(t.fingerId == inputSource){
					if(t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary){
						return true;
					}
					else{
						return false;
					}
				}
			}
			return false;
		}
		#endif

		return base.TestKey();
	}
	
	public Vector2 InputPosition{
		get{
			#if UNITY_IPHONE || UNITY_ANDROID
			if( (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) && Input.multiTouchEnabled){
				//Touch t = Input.GetTouch(inputSource)

				for(int i=0; i<Input.touchCount; i++){
					Touch t = Input.GetTouch(i);
					if(t.fingerId == inputSource){
						// if(t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary){
						// 	return true;
						// }
						// else{
						// 	return false;
						// }
						return t.position;
					}
				}

				return new Vector2(0,0);
			}
			#endif
			// #if UNITY_ANDROID
			// 	else if(AndroidInput.secondaryTouchEnabled && AndroidInput.touchCountSecondary > inputSource){
			// 		Vector2 rawInput = AndroidInput.GetSecondaryTouch(inputSource).position;
			// 		rawInput = new Vector2(rawInput.x * Screen.width / AndroidInput.secondaryTouchWidth, 
			// 					rawInput.y * Screen.height / AndroidInput.secondaryTouchHeight);
			// 		return rawInput;
			// 	}
			// #endif
			// else{
			//#else
			
			return Input.mousePosition;
			// }
			//#endif	
				
		}	
	}
	
	
	protected override void OnKeyDown(){
		keyLastPosition= InputPosition;
	
		keyDragDistance.x = keyDeltaPosition.x = 0;
		keyDragDistance.y = keyDeltaPosition.y = 0;
		
		sample = 0;
		for(int i=0; i<SAMPLE_COUNT; i++){
			keyVelocitySamples[i].x = 0;
			keyVelocitySamples[i].y = 0;	
		}
		
		//this happens last
		keyChangePosition = InputPosition;
	}

	protected override void OnKeyHeld(){
		keyDeltaPosition.x = InputPosition.x - keyLastPosition.x;
		keyDeltaPosition.y = InputPosition.y - keyLastPosition.y;

		keyDragDistance.x = InputPosition.x - keyChangePosition.x;
		keyDragDistance.y = InputPosition.y - keyChangePosition.y;
		
		keyVelocitySamples[sample] = (InputPosition - keyLastPosition) / Time.deltaTime;
		sample = (sample + 1)%SAMPLE_COUNT;
		
		keyLastPosition = InputPosition;
	}
	protected override void OnKeyUp(){

		keyDeltaPosition.x = 0;
		keyDeltaPosition.y = 0;
		
		keyDragDistance.x = InputPosition.x - keyChangePosition.x;
		keyDragDistance.y = InputPosition.y - keyChangePosition.y;
		


		keyVelocitySamples[sample] = (InputPosition - keyLastPosition) / Time.deltaTime;
		sample = (sample + 1)%SAMPLE_COUNT;
		
		//this happens last
		keyChangePosition = InputPosition;
	}
	protected override void OnKeyNeutral(){
		keyDragDistance.x = 0;
		keyDragDistance.y = 0;

		keyDeltaPosition.x = 0;
		keyDeltaPosition.y = 0;
	}	
}
