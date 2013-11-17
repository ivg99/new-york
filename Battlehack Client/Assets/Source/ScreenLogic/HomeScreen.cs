using UnityEngine;
using System.Collections;

public class HomeScreen : ApplicationScreen {

	public override void Activate(bool immediate){
		base.Activate(immediate);
		gameObject.SetActive(true);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(0, 0);
		}
		else{
			time = 0;
			xStart = 1;
			xTarget = 0;
			animating = true;
			activating = true;
		}
		
	}

	public override void Deactivate(bool immediate){

		base.Deactivate(immediate);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(1, 0);
			gameObject.SetActive(false);
		}
		else{
			time = 0;
			xStart = 0;
			xTarget = 1;
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


	void InitCodeScanner(){
		EasyCodeScanner.Initialize();
		
		//Register on Actions
		EasyCodeScanner.OnScannerMessage += OnScannerMessage;
		EasyCodeScanner.OnScannerEvent += OnScannerEvent;
		EasyCodeScanner.OnDecoderMessage += OnDecoderMessage;
	}

	//Callback when returns from the scanner
	void OnScannerMessage(string data){
		Debug.Log("EasyCodeScannerExample - onScannerMessage data=:"+data);
		//dataStr = data;
		
		//Just to show case : get the image and display it on a Plane
		//Texture2D tex = EasyCodeScanner.getScannerImage(200, 200);
		//PlaneRender.material.mainTexture = tex;
		
		//Just to show case : decode a texture/image - refer to code list
		//EasyCodeScanner.decodeImage(-1, tex);
	}
	
	//Callback which notifies an event
	//param : "EVENT_OPENED", "EVENT_CLOSED"
	void OnScannerEvent(string eventStr){
		Debug.Log("EasyCodeScannerExample - onScannerEvent:"+eventStr);
	}
	
	//Callback when decodeImage has decoded the image/texture 
	void OnDecoderMessage(string data){
		Debug.Log("EasyCodeScannerExample - onDecoderMessage data:"+data);
		//dataStr = data;
	}
}
