using UnityEngine;
using System.Collections;

public class CheckoutScreen : ApplicationScreen {



	public override void Activate(bool immediate){
		base.Activate(immediate);
		gameObject.SetActive(true);
		if(immediate){
			uiTransform.RelativePosition = new Vector2(0, 0);
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
			uiTransform.RelativePosition = new Vector2(1, 0);
			gameObject.SetActive(false);
		}
		else{
			time = 0;
			xStart = 0;
			xTarget = 1;
			animating = true;
			activating = false;
		}
	}

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

	float xStart;
	float xTarget;
	float time;
	bool animating = false;
	bool activating = false;

	void Start(){
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


	void Send(int idx){

	}
	const string ADD_URL = Config.BASE_URL + "/paypal.vault.add.php";
	const string ISSUER_URL = Config.BASE_URL + "/cc.issuer.php";
	const string PAY_URL = Config.BASE_URL + "/paypal.pay.php";

	IEnumerator Submit(){

		yield return 0;
	}

	IEnumerator GetIssuer(){
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
