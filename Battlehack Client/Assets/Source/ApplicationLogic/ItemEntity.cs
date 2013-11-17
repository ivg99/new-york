using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class ItemEntity  {

	int id;
	int merchantID;
	string merchantName;
	string name;
	string description;
	string photoLargeURL;
	string photoThumbURL;
	string modelURL;
	int price;
	List<ItemParameter> parameters;

	public ItemEntity(int id, 
				string name, 
				string description, 
				string photoLargeURL, 
				string photoThumbURL, 
				string modelURL, 
				int price,
				int merchantID, 
				string merchantName,
				List<ItemParameter> parameters
				){
		this.id = id;
		this.merchantID = merchantID;
		this.merchantName = merchantName;
		this.name = name;
		this.description = description;
		this.photoLargeURL = photoLargeURL;
		this.photoThumbURL = photoThumbURL;
		this.modelURL = modelURL;
		this.price = price;
		this.parameters = parameters;
	} 

	public int Id{
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
	public string PhotoLargeURL{
		get{ return photoLargeURL; }
	}
	public string PhotoThumbURL{
		get{ return photoThumbURL; }
	}
	public string ModelURL{
		get{ return modelURL; }
	}
	public int Price{
		get{ return price; }
	}
	public List<ItemParameter> Parameters{
		get{ return parameters;}
	}
}

