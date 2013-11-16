using UnityEngine;
using System.Collections;

public class UserManagement : MonoBehaviour {
	private static UserManagement instance;
	public static UserManagement Instance{
		get{
			return instance;
		}
	}
	
	public enum LoginStatus{None, Connecting, LoggedIn, IncorrectPassword, UsernameNotFound};

	private string cachedUsername;
	private string cachedPassword;

	private string userID;

	const string USERNAME_KEY = "USERNAME";
	const string PASSWORD_KEY = "PASSWORD";

	const string BASE_URL = "http://futurestore.areality3d.com";
	const string LOGIN_URL = BASE_URL + "/login.php";
	const string REGISTER_URL = BASE_URL + "/register.php";
	void Awake(){
		instance = this;

		GetStoredInfo();

		Login("alpha", "abcdefg");
	}

	void GetStoredInfo(){
		if(PlayerPrefs.HasKey(USERNAME_KEY)){
			cachedUsername = PlayerPrefs.GetString(USERNAME_KEY);
		}

		//TODO: Hash and salt this, instead of storing in the clear, obviously ;)
		if(PlayerPrefs.HasKey(PASSWORD_KEY)){
			cachedPassword = PlayerPrefs.GetString(PASSWORD_KEY);
		}
	}

	void StoreLoginInfo(string username, string password){
		PlayerPrefs.SetString(USERNAME_KEY, username);
		PlayerPrefs.SetString(PASSWORD_KEY, password);
	}

	public void CreateNewUser(string username, string password, string email, bool saveLoginInformation = false){

		StartCoroutine(CreateAccount(username, password, email));

	}

	public void Login(string username, string password, bool saveLoginInformation = false){

		if(saveLoginInformation){
			StoreLoginInfo(username, password);
	
		}

		StartCoroutine(PerformLogin(username, password));
	}

	public void Logout(){

	}

	IEnumerator PerformLogin(string username, string password){

		WWWForm loginForm = new WWWForm();
		loginForm.AddField("username", username);
		loginForm.AddField("password", password);
		loginForm.AddField("submitted", 1);
		WWW www = new WWW( LOGIN_URL, loginForm );
		yield return www;
		if(www.error != null){
			Debug.Log("ERROR ON LOGIN: "+ www.error);
		}
		Debug.Log(www.data);

		yield return 0;
	}

	IEnumerator CreateAccount(string username, string password, string email){

		WWWForm registerForm = new WWWForm();
		registerForm.AddField("username", username);
		registerForm.AddField("email", email);
		registerForm.AddField("password", password);
		registerForm.AddField("submitted", 1);
		WWW www = new WWW( REGISTER_URL, registerForm );
		yield return www;
		if(www.error != null){
			Debug.Log("ERROR ON LOGIN: "+ www.error);
		}
		Debug.Log(www.data);

		yield return 0;
	}
}
