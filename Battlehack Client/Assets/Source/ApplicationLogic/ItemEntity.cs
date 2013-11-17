using UnityEngine;
using System.Collections;

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
				int merchantID, 
				string merchantName, 
				string name, 
				string description, 
				string photoURL, 
				string modelURL, 
				int price){
		this.id = id;
		this.merchantID = merchantID;
		this.merchantName = merchantName;
		this.name = name;
		this.description = description;
		this.photoURL = photoURL;
		this.modelURL = modelURL;
		this.price = price;

	} 
}
