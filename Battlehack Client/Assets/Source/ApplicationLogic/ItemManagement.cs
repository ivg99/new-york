using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManagement : MonoBehaviour {

	private static ItemManagement instance;
	public static ItemManagement Instance{
		get{
			return instance;
		}
	}

	private Dictionary<int, ItemEntity> localItemStore = new Dictionary<int, ItemEntity>();

	//Editor debug purposes, so we can debug issues in the editos
	private List<ItemEntity> allItems = new List<ItemEntity>();

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


	
			//TODO: error checking, blah blah blah
			string id = j.GetField("id_i").str;
			string merchantID = j.GetField("idmi_i").str;
			string merchantName = j.GetField("storename").str;
			string name = j.GetField("name").str;
			string description = j.GetField("description").str;
			string photoURL = WWW.UnEscapeURL(j.GetField("photo").str);
			string modelURL = j.GetField("model").str;
			string price = j.GetField("price").str;

			int intId = System.Int32.Parse(id);
			int intMerchantID = System.Int32.Parse(merchantID);
			float floatPrice = System.Single.Parse(price);
			int intPrice = (int)(100*floatPrice);

			Debug.Log(photoURL);
			PhotoManager.Instance.LoadImage(intId, photoURL);

			ItemEntity item = new ItemEntity(
				intId, name, description, photoURL, modelURL, intPrice, intMerchantID, merchantName
			);
			localItemStore.Add(intId, item);
			allItems.Add(item);
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
