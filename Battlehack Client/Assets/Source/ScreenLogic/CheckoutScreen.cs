using UnityEngine;
using System.Collections;

public class CheckoutScreen : ApplicationScreen {



	public override void Activate(bool immediate, int startPoint){
		base.Activate(immediate, startPoint);
		gameObject.SetActive(true);
		checkoutGroup.Transparency = 1;
		successGroup.Transparency = 0;
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

	public override void Deactivate(bool immediate, int endPoint){

		base.Deactivate(immediate, endPoint);
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

	[SerializeField] UITransformNode checkoutGroup;
	[SerializeField] UITransformNode successGroup;

	[SerializeField] UITapGestureRecognizer back;
	[SerializeField] UITapGestureRecognizer sendPayment;
	[SerializeField] UITapGestureRecognizer firstNameBtn;
	[SerializeField] UITapGestureRecognizer lastNameBtn;

	[SerializeField] UITapGestureRecognizer ccNumberBtn;
	[SerializeField] UITapGestureRecognizer expMonthBtn;
	[SerializeField] UITapGestureRecognizer expYearBtn;
	[SerializeField] UITapGestureRecognizer cvvBtn;

	[SerializeField] UITextNode firstName;
	[SerializeField] UITextNode lastName;

	[SerializeField] UITextNode ccNumber;
	[SerializeField] UITextNode expMonth;
	[SerializeField] UITextNode expYear;
	[SerializeField] UITextNode cvv;
	[SerializeField] UITextNode type;

	float xStart;
	float xTarget;
	float time;
	bool animating = false;
	bool activating = false;

	void Start(){
		back.OnGestureRecognized += Back;
		firstNameBtn.OnGestureRecognized += First;
		lastNameBtn.OnGestureRecognized += Last;

		ccNumberBtn.OnGestureRecognized += CC;
		expMonthBtn.OnGestureRecognized += Month;
		expYearBtn.OnGestureRecognized += Year;
		cvvBtn.OnGestureRecognized += CVV;

		sendPayment.OnGestureRecognized += Send;
	}

	TouchScreenKeyboard keyboard;
	private int currentField = 0;
	private string[] texts = new string[6];


	void Back(int idx){
		StateMachine.Instance.GotoItemScreen(-1);
	}

	void Send(int idx){
		StartCoroutine(GetIssuer());
	}
	const string ADD_URL = Config.BASE_URL + "/paypal.vault.add.php";
	const string ISSUER_URL = Config.BASE_URL + "/cc.issuer.php";
	const string PAY_URL = Config.BASE_URL + "/paypal.pay.php";

	

	IEnumerator GetIssuer(){
		WWWForm issuerForm = new WWWForm();
		issuerForm.AddField("cc", texts[2]);
		issuerForm.AddField("submitted", 1);
		WWW www = new WWW( ISSUER_URL, issuerForm );
		yield return www;
		if(www.error != null){
			Debug.Log("ERROR ON ISSUER: "+ www.error);
		}
		else{
			string encodedString = www.data;
			Debug.Log(encodedString);
			JSONObject j = new JSONObject(encodedString);
			string issuer = j.GetField("issuer").str;
			type.Text = issuer;
			StartCoroutine(Submit(issuer));
		}
		yield return 0;
	}

	IEnumerator Submit(string issuerType){

		WWWForm submitForm = new WWWForm();
		submitForm.AddField("type", issuerType);
		submitForm.AddField("number", texts[2]);
		submitForm.AddField("expire_month", texts[3]);
		submitForm.AddField("expire_year", texts[4]);
		submitForm.AddField("first_name", texts[0]);
		submitForm.AddField("last_name", texts[1]);
		submitForm.AddField("cvv", texts[5]);
		submitForm.AddField("submitted", 1);
		WWW www = new WWW( ADD_URL, submitForm );
		yield return www;
		if(www.error != null){
			Debug.Log("ERROR ON ADD CC: "+ www.error);
		}
		else{
			string encodedString = www.data;
			Debug.Log(encodedString);
			JSONObject j = new JSONObject(encodedString);
			string ccid = j.GetField("id_cc").str;

			StartCoroutine(Pay(ccid));
		}
		yield return 0;
	}

	IEnumerator Pay(string ccidNumber){
		WWWForm submitForm = new WWWForm();
		submitForm.AddField("ccid", ccidNumber);
		submitForm.AddField("price", "10");
		submitForm.AddField("submitted", 1);
		WWW www = new WWW( PAY_URL, submitForm );
		yield return www;
		if(www.error != null){
			Debug.Log("ERROR ON PAY: "+ www.error);
		}
		else{
			string encodedString = www.data;
			Debug.Log(encodedString);
			JSONObject j = new JSONObject(encodedString);
			string status = j.GetField("status").str;
			string id = j.GetField("id").str;
			Debug.Log(status + " : " + id);
			checkoutGroup.Transparency = 0;
			successGroup.Transparency = 1;
		}
		yield return 0;
	}

	void First(int idx){
		if (keyboard == null || !keyboard.active || currentField != 0){
			keyboard = TouchScreenKeyboard.Open(texts[0], TouchScreenKeyboardType.Default, false, false);
			currentField = 0;
		}
		else{
			keyboard.active = false;
		}
	}

	void Last(int idx){
		if (keyboard == null || !keyboard.active || currentField != 1){
			keyboard = TouchScreenKeyboard.Open(texts[1], TouchScreenKeyboardType.Default, false, false);
			currentField = 1;
		}
		else{
			keyboard.active = false;
		}
	}
	void CC(int idx){
		if (keyboard == null || !keyboard.active || currentField != 2){
			keyboard = TouchScreenKeyboard.Open(texts[2], TouchScreenKeyboardType.NumberPad, false, false);
			currentField = 2;
		}
		else{
			keyboard.active = false;
		}
	}
	void Month(int idx){
		if (keyboard == null || !keyboard.active || currentField != 3){
			keyboard = TouchScreenKeyboard.Open(texts[3], TouchScreenKeyboardType.NumberPad, false, false);
			currentField = 3;
		}
		else{
			keyboard.active = false;
		}
	}
	void Year(int idx){
		if (keyboard == null || !keyboard.active || currentField != 4){
			keyboard = TouchScreenKeyboard.Open(texts[4], TouchScreenKeyboardType.NumberPad, false, false);
			currentField = 4;
		}
		else{
			keyboard.active = false;
		}
	}
	void CVV(int idx){
		if (keyboard == null || !keyboard.active || currentField != 5){
			keyboard = TouchScreenKeyboard.Open(texts[5], TouchScreenKeyboardType.NumberPad, false, false);
			currentField = 5;
		}
		else{
			keyboard.active = false;
		}
	}


	void UpdateText(){
		if (keyboard != null && keyboard.active){//&& keyboard.done){
			texts[currentField] = keyboard.text;

			switch(currentField){

				case 0:
					firstName.Text = texts[currentField];
					break;
				case 1:
					lastName.Text = texts[currentField];
					break;
				case 2:	
					ccNumber.Text = texts[currentField];
					break;
				case 3:
					if(texts[currentField].Length > 2){
						texts[currentField] = texts[currentField].Substring(0,2);
						keyboard.text = texts[currentField];
					}
					expMonth.Text = texts[currentField];
					break;
				case 4:
					if(texts[currentField].Length > 4){
						texts[currentField] = texts[currentField].Substring(0,4);
						keyboard.text = texts[currentField];
					}
					expYear.Text = texts[currentField];
					break;
				case 5:
					if(texts[currentField].Length > 4){
						texts[currentField] = texts[currentField].Substring(0,4);
						keyboard.text = texts[currentField];
					}
					cvv.Text = texts[currentField];
					break;
			}
			
		}
	}


	void Update(){
		UpdateText();
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
	
}
