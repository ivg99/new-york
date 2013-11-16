using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ScrollElement : MonoBehaviour {

	public UITransformNode root;
	public UITapGestureRecognizer button;

	public abstract void Redraw(int id);
	
	void Start(){
		button.OnGesturePossible += Possible;
		button.OnGestureRecognized += Complete;
		button.OnGestureFailed += Failed;
	}

	protected abstract void Possible(int idx);
	protected abstract void Complete(int idx);
	protected abstract void Failed(int idx);
	
}



