using UnityEngine;
using System.Collections;

public class ScrollWindow : MonoBehaviour {

	[SerializeField] UIScrollGestureRecognizer scrollRegion;
	[SerializeField] UITransformNode elementGroup;
	[SerializeField] ScrollElement[] elementPool;


	public event System.Action<int, ScrollElement> OnRedrawElement ;	

	public int contentCount = 10;

	//Fixed parameters
	float elementHeight = 100;


	//temp variables
	float scroll = 0;
	int indexOffset = 0;



	//scroller physics
	float previousTarget;
	float scrollTarget;
	float scrollVelocity;

	float momentum;

	float scrollClampVelocity;
	bool wasScrolling = false;




	void Start(){
		scrollRegion.OnGestureBegan += OnScrollStart;
		scrollRegion.OnGestureChanged += OnScroll;
		scrollRegion.OnGestureRecognized += OnScrollEnd;
		UpdateScroll(true);
		UpdateOffset();
	}


	void OnScrollStart(int idx){
		wasScrolling = true;
	}

	void OnScroll(int idx){
		scrollTarget += -scrollRegion.ScrollAmount.y;

		if(scrollTarget < 0){
			scrollTarget = -Mathf.Pow(-(scrollTarget),0.8f);
		}
		if(scrollTarget > TotalHeight){
			float delta = scrollTarget - TotalHeight;
			scrollTarget = TotalHeight + Mathf.Pow((delta),0.8f);
		}

	}
	void LateUpdate(){
		UpdateScroll();
	}

	public float TotalHeight{
		get{
			float val = contentCount * elementHeight - elementGroup.WorldSize.y;
			if(val < 0){
				return 0;
			}
			return val;
		}
	}

	void OnScrollEnd(int idx){
		wasScrolling = false;
	}

	void InitializeScrollList(){
		// elementHeight = elementPool[0].
		for(int i=0; i<elementPool.Length; i++){

		}
	}

	void UpdateScroll(bool forceLayout = false){


		if(!wasScrolling){


			scrollTarget += momentum *Time.deltaTime;
			momentum *= Mathf.Clamp01(1-10*Time.deltaTime);
			if(scrollTarget < 0){
				scrollTarget = Smoothing.SpringSmooth(scrollTarget,0, ref scrollClampVelocity, 0.2f, Time.deltaTime);
				if(Mathf.Abs(scrollTarget) < 0.1f){
					scrollTarget = 0;
					scrollClampVelocity = 0;
				}
			}
			else if(scrollTarget > TotalHeight){
				scrollTarget = Smoothing.SpringSmooth(scrollTarget,TotalHeight, ref scrollClampVelocity, 0.2f, Time.deltaTime);
				if(Mathf.Abs(TotalHeight - scrollTarget) < 0.1f){
					scrollTarget = TotalHeight;
					scrollClampVelocity = 0;
				}
			}
		}
		else{
			momentum = scrollVelocity;
		}
		

		if(scroll != scrollTarget || forceLayout){
			scroll = Smoothing.SpringSmooth(scroll, scrollTarget, ref scrollVelocity, 0.025f, Time.deltaTime);
			if(Mathf.Abs(scroll - scrollTarget) < 0.1f){
				scroll = scrollTarget;
				scrollVelocity = 0;
			}

			
			int newOffset = (int)(scroll / elementHeight);
			float scrollOffset = -scroll % elementHeight;

			if(indexOffset != newOffset){
				indexOffset = newOffset;
				UpdateOffset();
			}


			for(int i=0; i<elementPool.Length; i++){
				elementPool[i].root.Position = new Vector2(0,elementHeight*i + scrollOffset);
				//elementPool[i].UpdateTexture();
			}
		}
	}

	//Redraw all the elements
	void UpdateOffset(){


		for(int i=0; i<elementPool.Length; i++){
			int idx = i+indexOffset;
			if(idx < contentCount && idx > -1){
				
				elementPool[i].root.Transparency = 1;
				elementPool[i].Redraw(idx);
				// if(OnRedrawElement != null){
				// 	OnRedrawElement(idx, elementPool[i]);
				// }
			}
			else{
				elementPool[i].root.Transparency = 0;
			}
		}
	}

	#if UNITY_EDITOR
	
	#endif
	
}


