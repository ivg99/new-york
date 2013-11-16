/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UILongPressGestureRecognizer : UIGestureRecognizer {

	private float longPressTime =0.5f;
	
	
	
	private bool keyDownInElement = false;
	
	protected override GestureState Sample(){
		
		
		
		if(InputHandling.LeftMouse.KeyDown){
			if(TargetNode.HitTest(Input.mousePosition)){
				keyDownInElement = true;
			}
		}
		if(!InputHandling.LeftMouse.KeyHeld){
			keyDownInElement = false;
		}
		if(keyDownInElement && InputHandling.LeftMouse.KeyHeldDuration(longPressTime) && TargetNode.HitTest(Input.mousePosition)){
			keyDownInElement = false;
//			Debug.Log("long" + targetNode);	
			return GestureState.Complete;
			
		}
			
		
		
		return GestureState.None;
	}
	
	protected override void OverrideGesture(){
		keyDownInElement = false;
	}
	
	//protected override void GestureComplete(){
	//	base.GestureComplete();
		//Debug.Log("its a long one" + targetNode);	
	//}
	
}*/

