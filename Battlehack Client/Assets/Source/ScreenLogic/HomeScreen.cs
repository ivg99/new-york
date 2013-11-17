using UnityEngine;
using System.Collections;

public class HomeScreen : ApplicationScreen {

	public override void Activate(bool immediate, int startPoint){
		base.Activate(immediate,startPoint);
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

	public override void Deactivate(bool immediate,int endPoint){

		base.Deactivate(immediate,endPoint);
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

	[SerializeField] UITapGestureRecognizer scanButton;
	[SerializeField] UITextureNode scanTexture;

	float xStart;
	float xTarget;
	float time;
	bool animating = false;
	bool activating = false;

	void Start(){
		scanButton.OnGesturePossible += ScanPossible;
		scanButton.OnGestureRecognized += ScanComplete;
		scanButton.OnGestureFailed += ScanFailed;

		InitCodeScanner();
	}

	void Update(){
		if(animating){
			time = Mathf.Clamp01(2f*Time.deltaTime + time);
			float val = Smoothing.QuinticEaseOut(time);
			val = val*xTarget + (1-val)*xStart;
			uiTransform.RelativePosition = new Vector2(val, 0);
			if(time == 1){
				animating = false;
				if(!activating){
					gameObject.SetActive( false );
				}
			}
		}
	}

	 void ScanPossible(int idx){
		ScanSelected();
	}

	 void ScanComplete(int idx){
		ScanNormal();
		#if UNITY_EDITOR
			LoadItem(63);
		#else
			EasyCodeScanner.launchScanner( true, "Scanning...", -1, true);
		#endif
		
	}
	  void ScanFailed(int idx){
		ScanNormal();
	}
	

	void ScanNormal(){
		scanTexture.Color = new Color(144/255f,114/255f,194/255f,1);
		// scanTexture.Color = new Color(0,0,0,1);
	}

	void ScanSelected(){
		scanTexture.Color = new Color(90/255f,64/255f,135/255f,1);
		// title.Color = new Color(1,1,0,1);
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

		int url = data.IndexOf("?get=");
		Debug.Log(url);
		if(url > -1){
			string result = data.Substring(url+5);
			int value;
			bool success = System.Int32.TryParse(result, out value);
			Debug.Log(result + " : " + success);
			if(success){
				LoadItem(value);
			}
		}
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

	void LoadItem(int itemID){
		StateMachine.Instance.LoadItem(itemID);
	}


}
