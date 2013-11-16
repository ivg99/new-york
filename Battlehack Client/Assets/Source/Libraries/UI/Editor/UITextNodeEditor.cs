/*using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UITextNode))]
[CanEditMultipleObjects]
public class UITextNodeEditor : Editor
{

	
	
	
	public override void OnInspectorGUI() 
	{
		
		
		
		UITextNode t = target as UITextNode;

		
		DrawDefaultInspector();
		if (GUI.changed){
			t.SetDirty();
		}	
	}
	

	
	
}
*/
