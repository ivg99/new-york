using UnityEngine;
using System.Collections;

public class ItemScreen : ApplicationScreen {

	public override void Activate(bool immediate){
		base.Activate(immediate);
		time = 0;
		xStart = 1;
		xTarget = 0;
		animating = true;
	}

	public override void Deactivate(bool immediate){
		base.Deactivate(immediate);
		time = 0;
		xStart = 0;
		xTarget = 1;
		animating = true;
	}

	float xStart;
	float xTarget;
	float time;
	bool animating = false;
	bool activating = false;

	



	int item = 0;
	ItemEntity entity;
	public void SetItem(int item){
		this.item = item;
		entity = null;
	}

	void Update(){
		if(activated && entity == null){
			entity = ItemManagement.Instance.GetItem(item);
			if(entity != null){
				DrawItem();
			}
		}
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


	void DrawItem(){
		Debug.Log("hell yeah");
	}
}
