using UnityEngine;
using System.Collections;

public class AttributeScroll : MonoBehaviour {


	[SerializeField] UIScrollGestureRecognizer scroll;
	[SerializeField] UITextureNode scrollButton;
	[SerializeField] UITextureNode scrollBar;



	void Start(){
		scroll.OnGestureChanged += UpdateScroll;
		scroll.OnGestureBegan += StartScroll;


	}

	float value;
	public float Value{
		get{
			return value;
		}
	}



	float startPos;
	float initialPos;
	void StartScroll(int idx){
		startPos = scroll.InputPosition.x;
		initialPos = scrollButton.RelativePosition.x;
	}


	void UpdateScroll(int idx){

		float scrollPosition = scroll.InputPosition.x - startPos;
		value = Mathf.Clamp01(initialPos + scrollPosition / scrollBar.WorldSize.x);
		scrollButton.RelativePosition = new Vector2(value,0.5f);


	}
}
