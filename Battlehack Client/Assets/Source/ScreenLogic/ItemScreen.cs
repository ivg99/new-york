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

	[SerializeField] Camera customizerCamera;
	[SerializeField] UITextureNode touchArea;
	[SerializeField] UITextNode title;
	[SerializeField] UITextNode description;
	[SerializeField] UITextNode merchant;
	[SerializeField] UITextureNode icon;

	[SerializeField] UITextNode parameterOne;
	[SerializeField] UITextNode parameterTwo;
	[SerializeField] UITextNode parameterThree;


	int item = 0;
	ItemEntity entity;
	public void SetItem(int item){
		icon.Texture = null;
		this.item = item;
		entity = null;

	}

	void Update(){
		if(activated){
			if(entity == null){
				entity = ItemManagement.Instance.GetItem(item);
				if(entity != null){
					DrawItem();
				}
			}
			else if(icon.Texture == null){
				icon.Texture = PhotoManager.Instance.GetImage(entity.Id);
			}
		}
		if(animating){
			time = Mathf.Clamp01(Time.deltaTime + time);
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
	}


	void DrawItem(){
		title.Text = entity.Name;
		description.Text = entity.Description;
		merchant.Text = entity.MerchantName;
		PhotoManager.Instance.LoadImage(entity.Id, entity.PhotoLargeURL);
		Debug.Log("hell yeah");

		// StartCoroutine(GetParameters());
	}

	// IEnumerator GetParameters(){
		

	// 	WWWForm paramForm = new WWWForm();
	// 	paramForm.AddField("id_i", entity.Id);
	// 	paramForm.AddField("submitted", 1);
	// 	WWW www = new WWW( PARAMETER_URL, paramForm );
	// 	yield return www;
	// 	if(www.error != null){
	// 		Debug.Log("ERROR ON PARAMETERS: "+ www.error);
	// 	}
	// 	else{
	// 		string encodedString = www.data;
	// 		Debug.Log(encodedString + " : " + www.url + " : " + entity.Id);
	// 		JSONObject j = new JSONObject(encodedString);

	// 	}
	// }
}
