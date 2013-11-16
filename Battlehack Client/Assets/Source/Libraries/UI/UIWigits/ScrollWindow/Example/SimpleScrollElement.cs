using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleScrollElement : ScrollElement{

	[SerializeField] UITextNode title;
	[SerializeField] SimpleScroll scrollData;
	[SerializeField] UITextureNode bg;



	public override void Redraw(int id){
		title.Text = scrollData.data[id].name;
		Normal();
	}
	
	protected override void Possible(int idx){
		Selected();
	}

	protected override void Complete(int idx){
		Normal();
	}
	protected override void Failed(int idx){
		Normal();
	}
	

	void Normal(){
		bg.Color = new Color(1,1,1,1);
		title.Color = new Color(0,0,0,1);
	}

	void Selected(){
		bg.Color = new Color(0,0,0.5f,1);
		title.Color = new Color(1,1,0,1);
	}
}





