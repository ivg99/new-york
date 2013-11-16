using UnityEngine;
using System.Collections;

public class UserManagement : MonoBehaviour {
	private static UserManagement instance;
	public static UserManagement Instance{
		get{
			return instance;
		}
	}
	

	void Awake(){
		instance = this;
	}



	public void Login(string username, string password, bool saveLoginInformation = false){

		if(saveLoginInformation){

		}
	}

	public void Logout(){

	}

	IEnumerator PerformLogin(string username, string password){
		yield return 0;
	}

}
