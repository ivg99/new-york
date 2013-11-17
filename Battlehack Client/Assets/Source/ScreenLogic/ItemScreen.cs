using UnityEngine;
using System.Collections;

public class ItemScreen : ApplicationScreen {

	public override void Activate(bool immediate){
		base.Activate(immediate);
		gameObject.SetActive(true);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(0, 0);
			//SetCameraX(0);
			animating = false;
		}
		else{
			time = 0;
			uiTransform.RelativePosition = new Vector2(1, 0);
			xStart = 1;
			xTarget = 0;
			animating = true;
			activating = true;
		}
	}

	public override void Deactivate(bool immediate){
		base.Deactivate(immediate);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(0, 0);
			gameObject.SetActive(false);
			animating = false;
		}
		else{
			time = 0;
			xStart = 0;
			xTarget = 1;
			animating = true;
			activating = false;
		}
	}

	// void SetCameraX(float val){
	// 	Rect r = customizerCamera.pixelRect;
	// 		r.x = val;
	// 		r.width = Screen.width;
	// 		r.height = Screen.height*0.5f;
	// 		r.y = 162 /1136f;
	// 		customizerCamera.pixelRect = r;
	// }
	const string PARAMETER_URL = Config.BASE_URL+"/parameters.fetch.php";

	float xStart;
	float xTarget;
	float time;
	bool animating = false;
	bool activating = false;

	[SerializeField] UITapGestureRecognizer back;
	[SerializeField] UIScrollGestureRecognizer scroller;
	[SerializeField] Camera customizerCamera;
	[SerializeField] Transform cameraParent;
	[SerializeField] UITextureNode touchArea;
	[SerializeField] UITextNode title;
	[SerializeField] UITextNode description;
	[SerializeField] UITextNode merchant;
	[SerializeField] UITextureNode icon;

	[SerializeField] UITextNode parameterOne;
	[SerializeField] UITextNode parameterTwo;
	[SerializeField] UITextNode parameterThree;

	[SerializeField] GameObject[] objCache;

	int selectedParameter = 0;

	float targetYaw;
	float targetPitch;
	float pitch;
	float pitchVelocity;
	float yaw;
	float yawVelocity;

	int item = 0;
	ItemEntity entity;
	public void SetItem(int item){
		icon.Texture = null;
		this.item = item;
		entity = null;
		selectedParameter = 0;
	}

	void Start(){
		
		back.OnGestureRecognized += Home;
		scroller.OnGestureChanged += Scroll;
	}

	void Scroll(int idx){
		targetYaw = (targetYaw + 0.4f*scroller.RawScrollAmount.x);
		targetPitch = Mathf.Clamp(targetPitch + 0.3f*scroller.RawScrollAmount.y,-89,89);

		//invalidate button presses
		downParam = -1;

	}

	void Home(int idx){
		
		StateMachine.Instance.GotoHomeScreen();

	}

	int downParam =-1;

	void Update(){
		if(activated){
			if(entity == null){
				entity = ItemManagement.Instance.GetItem(item);
				if(entity != null){
					DrawItem();
				}
			}
			else{
				if(Input.GetKey(KeyCode.Mouse0)){
					Ray ray = customizerCamera.ScreenPointToRay(Input.mousePosition);
					 RaycastHit hit;
				    if(Physics.Raycast(ray, out hit, 100)){
				        string name = hit.collider.name;
				        for(int i=0; i<entity.Parameters.Count; i++){
				        	if(name == entity.Parameters[i].name){
				        		if(i < 4){
				        			downParam = i;
				        			
				        			// hit.collider.gameObject.renderer.material.color = new Color(1,1,0.5f,1);
				        		}
				        		
				        		
				        	}
				        }
				        
				    }
				}
				else{
					if(downParam != -1){

						selectedParameter = downParam;
	        			if(objCache[entity.Id] != null){
	        				SetObjectColors(objCache[entity.Id].transform);
	        			}
	        			downParam = -1;
					}
					
				}
				 if(icon.Texture == null){
					icon.Texture = PhotoManager.Instance.GetImage(entity.Id);
				}
			}

			


		}
		if(animating){
			time = Mathf.Clamp01(2f*Time.deltaTime + time);
			float val = Smoothing.QuinticEaseOut(time);
			val = val*xTarget + (1-val)*xStart;
			uiTransform.RelativePosition = new Vector2(val, 0);
			//SetCameraX(Screen.width*val);
			if(time == 1){
				animating = false;
				if(!activating){
					gameObject.SetActive(false);
				}
			}
		}
		pitch = Smoothing.SpringSmooth(pitch, targetPitch, ref pitchVelocity, 0.2f, Time.deltaTime);
		float tYaw = Mathf.DeltaAngle(yaw, targetYaw);
		yaw = Smoothing.SpringSmooth(yaw, targetYaw, ref yawVelocity, 0.2f, Time.deltaTime);
		cameraParent.rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
	}
	Color baseColor = new Color(1,1,1,1);
	Color selectColor = new Color(0.7f,0.68f,1,1);
	void SetObjectColors(Transform trans){
		foreach(Transform t in trans){
			if(t.name == entity.Parameters[selectedParameter].name){
				t.gameObject.renderer.material.color = selectColor;
				// Debug.Log(t.name);
			}
			else{
				// Debug.Log("norm" + t.name);
				t.gameObject.renderer.material.color = baseColor;
			}
			SetObjectColors(t);
		}
	}

	void DrawItem(){
		title.Text = entity.Name;
		description.Text = entity.Description;
		merchant.Text = entity.MerchantName;
		PhotoManager.Instance.LoadImage(entity.Id, entity.PhotoLargeURL);

		//oh god haxxxxx
		if(entity.Id < objCache.Length){
			objCache[entity.Id].SetActive(true);
		}
		for(int i=0; i<objCache.Length; i++){
			if(i != entity.Id){
				if(objCache[i] != null){
					objCache[i].SetActive(false);
				}
			}
		}

		for(int i=0; i<entity.Parameters.Count; i++){
			if(i==0){
				parameterOne.gameObject.SetActive(true);
				parameterOne.Text = entity.Parameters[i].name;
			}
			else if(i==1){
				parameterTwo.gameObject.SetActive(true);
				parameterTwo.Text = entity.Parameters[i].name;
			}
			else if(i==2){
				parameterThree.gameObject.SetActive(true);
				parameterThree.Text = entity.Parameters[i].name;
			}
		}
		if(entity.Parameters.Count < 3){
			parameterThree.gameObject.SetActive(false);
		}
		if(entity.Parameters.Count < 2){
			parameterTwo.gameObject.SetActive(false);
		}
		if(entity.Parameters.Count < 1){
			parameterOne.gameObject.SetActive(false);
		}
		if(objCache[entity.Id] != null){
			SetObjectColors(objCache[entity.Id].transform);
		}
		//StartCoroutine(LoadModel());
		Debug.Log("hell yeah");

		// StartCoroutine(GetParameters());
	}

	IEnumerator LoadModel(){

		WWW www = new WWW( entity.ModelURL );

        yield return www;
        if(www.error != null){
			Debug.LogError("ERROR ON MODEL LOAD: "+ www.error);
		}
		else{
			///Debug.Log(www.data);
			//yikes, we get obj data, but the parser is broken, so we'll hack it... :(


		}

		yield return 0;
	}

}
