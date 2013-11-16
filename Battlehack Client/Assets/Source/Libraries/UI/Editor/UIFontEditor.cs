using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UIFont))]
[CanEditMultipleObjects]
public class UIFontEditor : Editor
{

	public override void OnInspectorGUI() 
	{
		base.OnInspectorGUI();
		UIFont t = target as UIFont;
		if(GUILayout.Button("Calculate Values")){
			t.RecalculateFontValues();
			EditorUtility.SetDirty(t);
			
		}
	}
	
	[MenuItem("Assets/Create/UIFont")] 
    public static void CreateFont() { 
		CustomAssetMaker.CreateAsset<UIFont>("New UIFont");			
    }
}
