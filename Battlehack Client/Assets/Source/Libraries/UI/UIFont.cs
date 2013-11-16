using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UIFont : ScriptableObject{

	[SerializeField] public Font font;
	[SerializeField] private float[] characterLengths;
	[SerializeField] private bool dynamic;
	private const int CONTROL_CHARACTERS = 32;
	private const int SECONDARY_CONTROL_CHARACTERS = 34;
	private const int ALL_CONTROL_CHARACTERS = CONTROL_CHARACTERS + SECONDARY_CONTROL_CHARACTERS;
	
	private const int CHARACTER_COUNT = (16*24) - ALL_CONTROL_CHARACTERS;


	private const int S_END = 164;
	//extended A has 17 colums of 16
	//end first controls 31, space is 32
	//start second controls 128
	//end second controls 144
	
	public bool Dynamic{
		get{ return dynamic;}
	}

	public float GetLength(char c){
		
		int currentIndex = (int)c;
		
		if(currentIndex > 31 && currentIndex < 384){
			if( currentIndex < 127){
				currentIndex -= CONTROL_CHARACTERS;
//				Debug.Log(c + " : " + currentIndex);
				
				return characterLengths[currentIndex];
			}
			else if( currentIndex > 160){
				currentIndex -= ALL_CONTROL_CHARACTERS;
//				Debug.Log(c + " : " + currentIndex);
//				Debug.Log(currentIndex + " : " + c);
				return characterLengths[currentIndex];
			}
			else{
				return 0;	
			}
			
		}
		else{
			return 0;	
		}
		
	}
	
	/////* Editor Only Code */////
	
	
	#if UNITY_EDITOR
	public void RecalculateFontValues(){
		
		StringBuilder sb = new StringBuilder(CHARACTER_COUNT);
		
		if(Application.isEditor){
			GameObject g = new GameObject();
			TextMesh textMesh = g.AddComponent<TextMesh>();
			MeshRenderer textRenderer = g.GetComponent<MeshRenderer>();
			
			textMesh.font = font;
			textMesh.fontSize = 100;
			characterLengths = new float[CHARACTER_COUNT];
			
			for(int i=0; i<CHARACTER_COUNT; i++){
				int modifiedIndex = i + CONTROL_CHARACTERS;
				if(modifiedIndex > 126){
					modifiedIndex += SECONDARY_CONTROL_CHARACTERS;
				}
				char c = (char)modifiedIndex;
				string charString = c.ToString();
				
				textMesh.text = charString;
				sb.Append(charString);
			
				characterLengths[i] = textRenderer.bounds.size.x / 100f;
	//			Debug.Log(i + " : " + modifiedIndex + " : " + c + " : " + c.ToString());
				
			}
			
			//Special code to calculate a space
			textMesh.text = "a a";
			characterLengths[0] = (textRenderer.bounds.size.x - 2*GetLength('a'))/100f;
			//cleanup
			DestroyImmediate(g);
			
			//spits out the character list
			Debug.Log(sb.ToString()); 
		}
	}
	#endif
}
