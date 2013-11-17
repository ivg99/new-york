using UnityEngine;
using System.Collections;

public class LoginScreen : ApplicationScreen {

	public override void Activate(bool immediate, int startPoint){
		base.Activate(immediate, startPoint);
		gameObject.SetActive(true);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(0, 0);
		}
		else{
			time = 0;
			xStart = startPoint;
			xTarget = 0;
			animating = true;
			activating = true;
		}
		
	}

	public override void Deactivate(bool immediate, int endPoint){

		base.Deactivate(immediate, endPoint);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(1, 0);
			gameObject.SetActive(false);
		}
		else{
			time = 0;
			xStart = 0;
			xTarget = endPoint;
			animating = true;
			activating = false;
		}
	}

	float xStart;
	float xTarget;
	float time;
	bool animating = false;
	bool activating = false;

	void Update(){
		if(animating){
			time = Mathf.Clamp01(Time.deltaTime + time);
			float val = Smoothing.ExponentialEaseOut(time);
			val = val*xTarget + (1-val)*xStart;
			uiTransform.RelativePosition = new Vector2(val, 0);
			if(time == 1){
				animating = false;
				if(!activating){
					gameObject.SetActive(false);
				}
			}
		}
	}
	
}
