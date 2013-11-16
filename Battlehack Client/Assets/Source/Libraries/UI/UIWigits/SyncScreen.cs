// using UnityEngine;
// using System.Collections;

// public class SyncScreen : MonoBehaviour {

// 	[SerializeField] UIScrollGestureRecognizer secondScroll;
// 	[SerializeField] UITextureNode secondScrollButton;
// 	[SerializeField] UITextureNode secondScrollBar;

// 	[SerializeField] UIScrollGestureRecognizer msecondScroll;
// 	[SerializeField] UITextureNode msecondScrollButton;
// 	[SerializeField] UITextureNode msecondScrollBar;

// 	[SerializeField] UITextureNode flash;
// 	[SerializeField] UITextNode secondText;
// 	[SerializeField] UITextNode millisecondText;

// 	void Start(){
// 		secondScroll.OnGestureChanged += UpdateScroll;
// 		secondScroll.OnGestureBegan += StartScroll;

// 		msecondScroll.OnGestureChanged += UpdateMScroll;
// 		msecondScroll.OnGestureBegan += StartMScroll;

// 		secondScrollButton.RelativePosition = new Vector2(Mathf.Clamp01(SyncClock.Instance.SecondsOffset /120f + 0.5f),0.5f);
// 		msecondScrollButton.RelativePosition = new Vector2(Mathf.Clamp01(SyncClock.Instance.MillisecondsOffset /1000f + 0.5f),0.5f);
// 		SetSecondText();
// 		SetMillisecondText();
// 	}

// 	void SetSecondText(){
// 		string sign = "";
// 		if(SyncClock.Instance.SecondsOffset > 0){
// 			sign = "+";
// 		}
// 		else if(SyncClock.Instance.SecondsOffset < 0){
// 			sign = "-";
// 		}
// 		secondText.Text = sign + SyncClock.Instance.SecondsOffset.ToString("D2") + "s";
// 	}
// 	void SetMillisecondText(){
// 		string sign = "";
// 		if(SyncClock.Instance.MillisecondsOffset > 0){
// 			sign = "+";
// 		}
// 		else if(SyncClock.Instance.MillisecondsOffset < 0){
// 			sign = "-";
// 		}
// 		millisecondText.Text = sign + SyncClock.Instance.MillisecondsOffset.ToString("D3") + "ms";
// 	}

// 	void LateUpdate(){
// 		flash.Transparency = 1 - SyncClock.Instance.Millisecond*0.001f;
// 	}



// 	float startPos;
// 	float initialPos;
// 	void StartScroll(int idx){
// 		startPos = secondScroll.InputPosition.x;
// 		initialPos = secondScrollButton.RelativePosition.x;
// 	}

// 	float mstartPos;
// 	float minitialPos;
// 	void StartMScroll(int idx){
// 		mstartPos = msecondScroll.InputPosition.x;
// 		minitialPos = msecondScrollButton.RelativePosition.x;
// 	}

// 	void UpdateScroll(int idx){

// 		float scroll = secondScroll.InputPosition.x - startPos;
// 		float x = Mathf.Clamp01(initialPos + scroll / secondScrollBar.WorldSize.x);
// 		secondScrollButton.RelativePosition = new Vector2(x,0.5f);

// 		SyncClock.Instance.SecondsOffset = (int)(120*(x - 0.5f));

// 		SetSecondText();

// 	}

// 	void UpdateMScroll(int idx){
// 		float mscroll = msecondScroll.InputPosition.x - mstartPos;
// 		float mx = Mathf.Clamp01(minitialPos + mscroll / msecondScrollBar.WorldSize.x);
// 		msecondScrollButton.RelativePosition = new Vector2(mx,0.5f);

// 		SyncClock.Instance.MillisecondsOffset = (int)(1000*(mx - 0.5f));
// 		SetMillisecondText();
// 	}



// }
