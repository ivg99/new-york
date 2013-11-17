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
	System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Any;
	System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture;
	private Dictionary<int, ItemEntityLoader> localItemStore = new Dictionary<int, ItemEntityLoader>();

	//Editor debug purposes, so we can debug issues in the editos
	private List<ItemEntity> allItems = new List<ItemEntity>();

	const string ITEM_URL = Config.BASE_URL + "/item.fetch.php";

	public ItemEntity GetItem(int id){
		if(localItemStore.ContainsKey(id)){
			if( localItemStore[id].Loaded ){
				return localItemStore[id].Entity;
			}
			else{
				return null;
			}
			
		}
		else{
			ItemEntityLoader loader = new ItemEntityLoader();
			localItemStore.Add(id, loader);
			StartCoroutine(GetItemData(id, loader));
			return null;
		}
	}


	void Awake(){
		instance = this;
		//GetItem(1);
	}


	IEnumerator GetItemData(int itemID, ItemEntityLoader loader){
		Debug.Log(itemID);
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
			string photo_large = (j.GetField("photo_large").str);
			string photo_thumb = (j.GetField("photo_thumb").str);
			string modelURL = j.GetField("model").str;
			string price = j.GetField("price").str;

			int intId = System.Int32.Parse(id);
			int intMerchantID = System.Int32.Parse(merchantID);
			float floatPrice = System.Single.Parse(price);
			int intPrice = (int)(100*floatPrice);

			Debug.Log(photo_large + " : " + photo_thumb);


			List<ItemParameter> parameters = new List<ItemParameter>();
			JSONObject parametersArray = j.GetField("parameters");
			for(int i = 0; i < parametersArray.list.Count; i++){
				ItemParameter p = new ItemParameter();
				JSONObject ob = parametersArray.list[i];

				p.name = ob.GetField("param_name").str;


				ParseParam(out p.translate_x, out p.translate_x_min,ob.GetField("translate_x").str,
						   out p.translate_x_max,ob.GetField("translate_x1").str);
	
				ParseParam(out p.translate_y, out p.translate_y_min,ob.GetField("translate_y").str,
						   out p.translate_y_max,ob.GetField("translate_y1").str);

				ParseParam(out p.translate_z, out p.translate_z_min,ob.GetField("translate_z").str,
						   out p.translate_z_max,ob.GetField("translate_z1").str);

				ParseParam(out p.rotate_x, out p.rotate_x_min,ob.GetField("rotate_x").str,
						   out p.rotate_x_max,ob.GetField("rotate_x1").str);
	
				ParseParam(out p.rotate_y, out p.rotate_y_min,ob.GetField("rotate_y").str,
						   out p.rotate_y_max,ob.GetField("rotate_y1").str);

				ParseParam(out p.rotate_z, out p.rotate_z_min,ob.GetField("rotate_z").str,
						   out p.rotate_z_max,ob.GetField("rotate_z1").str);

				ParseParam(out p.scale_x, out p.scale_x_min,ob.GetField("scale_x").str,
						   out p.scale_x_max,ob.GetField("scale_x1").str);
	
				ParseParam(out p.scale_y, out p.scale_y_min,ob.GetField("scale_y").str,
						   out p.scale_y_max,ob.GetField("scale_y1").str);

				ParseParam(out p.scale_z, out p.scale_z_min,ob.GetField("scale_z").str,
						   out p.scale_z_max,ob.GetField("scale_z1").str);
				parameters.Add(p);
				//Debug.Log(p.name);
			}	
/** PHOTO DEBUG **/
			//for(int i=0; i<100; i++){
				
			//}



			ItemEntity item = new ItemEntity(
				intId, name, description, photo_large ,photo_thumb, modelURL, intPrice, intMerchantID, merchantName,parameters
			);
			loader.Entity = item; //localItemStore.Add(intId, item);
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

	void ParseParam(out bool inUse, out float min, string mindata,out float max, string maxdata ){
		bool success1 = false;//
		success1 = System.Single.TryParse(mindata,style, culture, out min);

		bool success2 = false;
		success2 = System.Single.TryParse(maxdata,style, culture, out max);

		if(success1 && success2){
			// Debug.Log(min + " : " +  max);
			inUse = true;
		}
		else{
			inUse = false;
		}
	}


}


