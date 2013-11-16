using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleScroll : MonoBehaviour {


	[SerializeField] ScrollWindow scrollWindow;
	public List<ListData> data = new List<ListData>();
	
	void Start(){
		data.Add(new ListData());
		data.Add(new ListData());
		data.Add(new ListData());
		data.Add(new ListData());
		data.Add(new ListData());

		for(int i=0; i<data.Count; i++){
			data[i].name = (i+1) + "! I am the very model of a modern major general, I've information animal";
		}

		//scrollWindow.OnRedrawElement += Redraw;
	}

	// void Redraw(int idx, ScrollElement s){
	// 	s.Redraw(idx, data);

	// }
	
}



public class ListData{
	public string name;
	public Texture2D icon;
}


