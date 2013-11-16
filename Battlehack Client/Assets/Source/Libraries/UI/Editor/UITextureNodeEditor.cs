//using UnityEngine;
//using UnityEditor;
//using System.Collections;

//[CustomEditor(typeof(UITextureNode))]
//[CanEditMultipleObjects]
//public class UITextureNodeEditor : Editor
//{
	
//	const float TITLE_MAXWIDTH = 150;
//	const float TITLE_MINWIDTH = 100;
//	const float FLOAT_WIDTH = 80;
	
	
	
	
	
	//public override void OnInspectorGUI() 
	//{
		
		
	//	
	//	UITextureNode t = target as UITextureNode;
		
		
		
//		t.Position = DrawVector2("Position", t.Position);
//		t.RelativePosition = DrawVector2("Relative Position", t.RelativePosition);
//		t.Anchor = DrawVector2("Anchor", t.Anchor);
//		t.Size = DrawVector2("Size", t.Size);
//		t.RelativeSize = DrawVector2("Relative Size", t.RelativeSize);
		
	//	DrawDefaultInspector();
	//	if (GUI.changed){
	//		t.SetDirty();
	//	}	
	//}
	
//	Vector2 DrawVector2(string name, Vector2 targetProperty){
//		GUILayout.BeginHorizontal("Box");
//			Vector2 val = new Vector2(0,0);
//			GUILayout.Label(name, GUILayout.MaxWidth(TITLE_MAXWIDTH),GUILayout.MinWidth(TITLE_MINWIDTH));
//			val.x = EditorGUILayout.FloatField("", targetProperty.x, GUILayout.MinWidth(FLOAT_WIDTH));	
//			val.y = EditorGUILayout.FloatField("", targetProperty.y, GUILayout.MinWidth(FLOAT_WIDTH));	
//			
//		GUILayout.EndHorizontal();
//		return val;
//	}
	
	
//	public void OnSceneGUI() 
//	{
//		UITransformNode t = target as UITransformNode;
//		t.CheckSceneHandles();
////		Event.current.Use();
//	}

	
	
//}
