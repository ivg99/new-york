// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class LocalListView : MonoBehaviour {

// 	[SerializeField] LocalListElement[] elementPool;
// 	[SerializeField] UITransformNode scrollArea;

// 	[SerializeField] UIScrollGestureRecognizer scroller;

// 	[SerializeField] UITapGestureRecognizer createButton;

// 	float scroll = 0;
// 	float elementHeight = 150;

// 	int offset = 0;

// 	float previousTarget;
// 	float scrollTarget;
// 	float scrollVelocity;

// 	float momentum;

// 	float scrollClampVelocity;
// 	bool wasScrolling = false;
	
// 	public Camera camAR;
// 	public Camera camReg;
	
// 	public CloudEventHandler ceh;

// 	void UpdateSize(){
// 		if(DataStore.LocalDropData == null){
// 			offset = 0;
// 			scroll = 0;
// 		}
// 		else if(DataStore.LocalDropData.Count < 5){
// 			offset = 0;
// 			scroll = 0;

// 		}
// 		else{

// 		}
// 	}

// 	void RegisterButtons(){
// 		for(int i=0; i<elementPool.Length; i++){
// 			elementPool[i].button.Id = i;
// 			elementPool[i].button.OnGestureRecognized += OnSelectElement;
// 		}
// 	}

// 	void OnSelectElement(int id){
		
		
// 	}

// 	void UpdateData(){
// 		UpdateSize();
// 		UpdateOffset();
// 	}

// 	float TotalHeight{
// 		get{
// 			float val = DataStore.LocalDropData.Count * 150 - 600;
// 			if(val < 0){
// 				return 0;
// 			}
// 			return val;
// 		}
// 	}

// 	void UpdateScroll(){



// 		//if(scroller.){
// 		//	wasScrolling = true;
// 		///	momentum = 0;

			
// 		//}
// 		//else{
// 		if(!wasScrolling){
// 				//wasScrolling = false;
// 			//	if(scrollTarget < 0 || scrollTarget > TotalHeight){
// 			//		momentum = 0;
// 				//}
// 				//else{
// 				//	momentum = scrollVelocity;
// 				//}

				
			

// 			scrollTarget += momentum *Time.deltaTime;
// 			momentum *= Mathf.Clamp01(1-10*Time.deltaTime);
// 			if(scrollTarget < 0){
// 				scrollTarget = Smoothing.SpringSmooth(scrollTarget,0, ref scrollClampVelocity, 0.2f, Time.deltaTime);
// 			}
// 			else if(scrollTarget > TotalHeight){
// 				scrollTarget = Smoothing.SpringSmooth(scrollTarget,TotalHeight, ref scrollClampVelocity, 0.2f, Time.deltaTime);
// 			}
// 		}
// 		else{
// 			momentum = scrollVelocity;
// 		}
// 		//}


// 		scroll = Smoothing.SpringSmooth(scroll, scrollTarget, ref scrollVelocity, 0.05f, Time.deltaTime);


		
// 		int newOffset = (int)(scroll / elementHeight);
// 		float scrollOffset = -scroll % 150;

// 		if(offset != newOffset){
// 			offset = newOffset;
// 			UpdateOffset();
// 		}


// 		for(int i=0; i<elementPool.Length; i++){
// 			elementPool[i].root.Position = new Vector2(0,150*i + scrollOffset);
// 			elementPool[i].UpdateTexture();
// 		}
// 	}

// 	//Redraw all the elements
// 	void UpdateOffset(){


// 		for(int i=0; i<elementPool.Length; i++){
// 			int idx = i+offset;
// 			if(idx < DataStore.LocalDropData.Count && idx > -1){
// 				elementPool[i].SetData(DataStore.LocalDropData[idx]);
// 				elementPool[i].root.Transparency = 1;
// 			}
// 			else{
// 				elementPool[i].root.Transparency = 0;
// 			}
// 		}
// 	}

// 	void Start(){
// 		DataStore.OnDataUpdated += UpdateData;
// 		UpdateOffset();
// 		RegisterButtons();
// 		scroller.OnGestureBegan += OnScrollStart;
// 		scroller.OnGestureChanged += OnScroll;
// 		scroller.OnGestureRecognized += OnScrollEnd;
// 		createButton.OnGestureRecognized += OnCreate;
// 	}

// 	void OnCreate(int idx){
// 		Core.Create();
// 	}

// 	void OnScrollStart(int idx){
// 		//previousTarget = scrollTarget;
// 		wasScrolling = true;
// 	}
// 	void OnScrollEnd(int idx){
// 		//previousTarget = scrollTarget;
// 		wasScrolling = false;
// 	}
// 	void OnScroll(int idx){
// 		scrollTarget +=  -scroller.ScrollAmount.y;

// 		if(scrollTarget < 0){
// 				scrollTarget = -Mathf.Pow(-(scrollTarget),0.8f);
// 			}
// 			if(scrollTarget > TotalHeight){
// 				float delta = scrollTarget - TotalHeight;
// 				scrollTarget = TotalHeight + Mathf.Pow((delta),0.8f);
// 			}
// 	//	if(newTarget < 0){
// 	//		newTarget = -Mathf.Pow(-(newTarget),0.9f);
// 		//}
// 		//if(newTarget > TotalHeight){
// 		//	float delta = newTarget - TotalHeight;
// 		//	newTarget = TotalHeight + Mathf.Pow((delta),0.9f);
// 		//}
// 		//scrollTarget = newTarget;
// 	}

	

// 	void LateUpdate(){
// 		UpdateScroll();
// 	}

// }

// [System.Serializable]
// public class LocalListElement{
// 	public UITransformNode root;
// 	public UITapGestureRecognizer button;
// 	public UITextureNode icon;
// 	public UITextNode title;
// 	public UITextNode description; 

// 	private TextureLoader tex;
// 	private bool imageLoaded = false;
// 	public void SetData(DropData d){
// 		title.Text = d.Title;
// 		description.Text = d.LocationDescription;
// 		imageLoaded = false;
// 		tex = DataStore.GetTexture(d.ImageURL);
// 	}

// 	public void UpdateTexture(){
// 		//Debug.Log(tex + " : " + imageLoaded);
// 		if(!imageLoaded && tex != null && tex.LoadStatus == TextureLoadStatus.Loaded){
// 			icon.Texture = tex.Texture;
// 			imageLoaded = true;
// 		}
// 		else{
// 			icon.Texture = null;
// 		}
// 	}
// }
