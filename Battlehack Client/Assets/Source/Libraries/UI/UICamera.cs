

using UnityEngine;
using System.Collections;

///This Class should execute first, and be the only monobehavior point of access for static stuff, like input
[ExecuteInEditMode]
public sealed class UICamera : MonoBehaviour {

	//private static UICamera instance;

	const float MINIMUM_WIDTH = 640;
	const float MINIMUM_HEIGHT = 1136;
	const float ASPECT_RATIO = MINIMUM_WIDTH / MINIMUM_HEIGHT;
	const float STANDARD_HEIGHT = 480;
	
	//TODO - if-def around this to get editor support
	public static float UIScreenWidth{ 
		get{ 
//			if(!Application.isPlaying){
//				return Screen.width / UIScale;
//			}
			return scaledScreenWidth;
		} 
	}
	public static float UIScreenHeight{ 
		get{ 
//			if(!Application.isPlaying){
//				return Screen.height / UIScale;
//			}
			return scaledScreenHeight;
		}
	}

	public static Vector2 GetUIPosition(Vector3 worldPosition, Camera camera){
		Vector2 screenPosition = camera.WorldToScreenPoint(worldPosition);
		screenPosition = new Vector2(screenPosition.x, Screen.height - screenPosition.y);
		screenPosition /= UIScale;
		return screenPosition;
	}
	
	private static float cachedScreenHeight = MINIMUM_HEIGHT;
	private static float cachedScreenWidth = MINIMUM_WIDTH;
	private static float scaledScreenWidth =MINIMUM_WIDTH;
	private static float scaledScreenHeight= MINIMUM_HEIGHT;
	private static float uiScale=1;
	//
	public static float UIScale{ 
		get{ 
//			if(!Application.isPlaying){
//				
//				float currentAspect = Screen.width / Screen.height;
//				if(currentAspect < ASPECT_RATIO){
//					return Screen.width / MINIMUM_WIDTH;	
//				}
//				else{
//					return Screen.height / MINIMUM_HEIGHT;
//				}
//			}
			return uiScale ;
		}
	}
	
	[SerializeField] new private Camera camera;
	
//	[SerializeField] private Camera[] cameras;
//	uiScale = Screen.height / STANDARD_HEIGHT;	




	private void Awake(){
		//instance = this;
		UpdateScreen();	
		
	}

	//private void Update(){
//		TouchInput.Update();

	//	if(cachedScreenHeight != Screen.height || cachedScreenWidth != Screen.width){
	//		UpdateScreen();
	//	}
		
	//}
	
	void OnPreRender(){
		if(camera.orthographicSize != UIScreenHeight *0.5f){
			camera.orthographicSize = UIScreenHeight *0.5f;
		}
	}

	public static void UpdateScreen(){
		cachedScreenHeight = Screen.height;
		cachedScreenWidth = Screen.width;
		
//		float currentAspect = cachedScreenWidth / cachedScreenHeight;
//		if(currentAspect < ASPECT_RATIO){
//			uiScale = Screen.width / MINIMUM_WIDTH;	
//		}
//		else{
			uiScale = Screen.height / MINIMUM_HEIGHT;
//		}
		//Debug.Log(UIScale);
		
		scaledScreenWidth = cachedScreenWidth / UIScale;
		scaledScreenHeight = cachedScreenHeight / UIScale;
		//instance.camera.orthographicSize = UIScreenHeight *0.5f;
//		Debug.Log(camera.orthographicSize);
//		for(int i=0; i<cameras.Length; i++){
//			cameras[i].orthographicSize = UIScreenHeight *0.5f;
//		}
	}
	
	#if UNITY_EDITOR
//		protected void OnDrawGizmos(){
//			camera = GetComponent<Camera>();
//			camera.orthographicSize = UIScreenHeight *0.5f;
//		}
	#endif
}
