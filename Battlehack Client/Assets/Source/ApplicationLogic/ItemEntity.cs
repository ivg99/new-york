using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemEntity  {

	int id;
	int merchantID;
	string merchantName;
	string name;
	string description;
	string photoURL;
	string modelURL;
	int price;


	public ItemEntity(int id, 
				string name, 
				string description, 
				string photoURL, 
				string modelURL, 
				int price,
				int merchantID, 
				string merchantName
				
				){
		this.id = id;
		this.merchantID = merchantID;
		this.merchantName = merchantName;
		this.name = name;
		this.description = description;
		this.photoURL = photoURL;
		this.modelURL = modelURL;
		this.price = price;

	} 

	public int ID{
		get{ return id; }
	}
	public int MerchantID{
		get{ return merchantID; }
	}
	public string MerchantName{
		get{ return merchantName; }
	}
	public string Name{
		get{ return name; }
	}
	public string Description{
		get{ return description; }
	}
	public string PhotoURL{
		get{ return photoURL; }
	}
	public string ModelURL{
		get{ return modelURL; }
	}
	public int Price{
		get{ return price; }
	}
}

