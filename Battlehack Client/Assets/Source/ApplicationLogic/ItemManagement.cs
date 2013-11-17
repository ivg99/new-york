using UnityEngine;
using System.Collections;

public class ItemManagement : MonoBehaviour {

	private static ItemManagement instance;
	public static ItemManagement Instance{
		get{
			return instance;
		}
	}

	const string ITEM_URL = Config.BASE_URL + "/item.fetch.php";


	void Awake(){
		instance = this;
		StartCoroutine(GetItemData(1));
	}


	IEnumerator GetItemData(int itemID){
		WWWForm itemForm = new WWWForm();
		itemForm.AddField("id_i", itemID);
		itemForm.AddField("submitted", 1);
		WWW www = new WWW( ITEM_URL, itemForm );
		yield return www;
		if(www.error != null){
			Debug.Log("ERROR ON REGISTRATION: "+ www.error);
		}
		else{
			string encodedString = www.data;
			Debug.Log(encodedString);
			JSONObject j = new JSONObject(encodedString);
			// Debug.Log(j.list);
			// Debug.Log(j.HasField("id_u"));
			//name,photo,model,description,price
			// if(j.HasField("name")){
			// 	string uID = j.GetField("name").str;
			// 	Debug.Log(uID);
			// 	userID = int.Parse(uID);
			// 	Debug.Log(userID + ": Registered and Logged In");
			// 	loginStatus = LoginStatus.LoggedIn;
			// }
		}

		yield return 0;
	}

}
