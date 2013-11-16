using UnityEngine;
using System.Collections;


/// <Summary>
/// UIText Layout determines how child elements are positioned, and word wrap
///  Dynamic positions children based on the size of the text
///  DynamicWrap positions children based on the size of the text, and applies word wrap based on the height and width values
///  StaticWrap positions children normally (like UITransform)
/// </Summary>
public enum UITextLayout{Dynamic,DynamicWrap, StaticWrap}  




//overflow just keeps adding text, overflow x doesnt wrap, overflow y keeps going down
//public enum UITextSize{Dynamic, Static} //option to change how sizing works....  maybe...

 
//I dislike the calling structure that is developing for text precipitating size changes... it is ugly

//none - specify nothing, all sizes are calculated
//clip - specify nothing, all widths are calculated

//okay, what actually should happen is that we need to introduce the dynamic sized element, which can fill a region, but doesnt have to - and presents to children as the filled portion.  This may mean adjusting the API such that 


//this could use some line height compensation...

//also override default shaders to 8 bit versions...

//So we should introduce the dynamic element (e.g. text)
//The simple form is the TextArea element, which has word wrap built in


[RequireComponent (typeof (TextMesh))]
public sealed class UITextNode : UIGraphicNode {
	
	public const float PixelScaleConstant = 10;
	
	[SerializeField][HideInInspector] private TextMesh textMesh;
	[SerializeField][HideInInspector] private float textScale = 1;
//	[SerializeField][HideInInspector] private float lineSpacing = 1;
	
	
	
	[SerializeField][HideInInspector] private string formattedText;
	[SerializeField] private UIFont uiFont;
	[SerializeField] private UITextLayout textLayout;
	
	[SerializeField][HideInInspector] private Vector2 measuredSize;
//	[SerializeField][HideInInspector] private float measuredWidth;
//	[SerializeField][HideInInspector] private float measuredHeight;
	
	[SerializeField] private int fontSize = 26;
	
	[SerializeField] private string text; //[HideInInspector]
	
	private float lastWrapDimension = -1;
	
	protected override void Awake(){
		base.Awake();
		#if UNITY_EDITOR
			if(!Application.isPlaying){
				EditorUpdate();
			}
		#endif
		
		
		ApplyTextSize();
		textMesh.tabSize = 0.5f;
		//FormatText(false);
		if(uiFont != null && uiFont.font != null){
			SetTexture(uiFont.font.material.mainTexture);
		}
		// Debug.Log( MeshBounds.size);
	}
	
	public float TextScale{
		get{ return textScale; }
		set{
			if(textScale != value){
				textScale = value;
				ApplyTextSize();
			}	
		}	
	}
	public float LineSpacing{
		get{ return textMesh.lineSpacing; }
		set{
			if(textMesh.lineSpacing != value){
				textMesh.lineSpacing = value;
				
			}	
		}	
	}
	
	public string Text{
		get{ return text; }
		set{
			if(text != value){
				text = value;
				
				FormatText(true);
				UpdateSize();
			}	
		}	
	}

	protected override void ApplyColor(Color c){
		textMesh.color = c;
	}

	public string FormattedText{
		get{ return formattedText;}	
	}
	private void FormatText(bool newText){
		if(uiFont != null){
			
			float wrapDimension = ( internalSize.x / textScale );
			if(Mathf.Abs(wrapDimension - lastWrapDimension) > 1 || newText){
				
//				if(! newText){
//					Debug.Log(wrapDimension + " : " + lastWrapDimension);
//				}
				if (textLayout == UITextLayout.Dynamic){
					formattedText = text;
				}
				else if (textLayout == UITextLayout.StaticWrap || textLayout == UITextLayout.DynamicWrap){
					
					
					formattedText = UIString.WrapString(text,uiFont,wrapDimension, (int)PixelScaleConstant*fontSize );
					
	//				Debug.Log(formattedText[formattedText.Length-1]);
				}
				lastWrapDimension = wrapDimension;
				textMesh.text = formattedText;
				CalculateHitArea();
			}
		}
	}
	
	private void ApplyTextSize(){
		if(uiFont.Dynamic){
			textMesh.fontSize = fontSize;
		}
		else{
			textMesh.fontSize = 0;
		}
		textMesh.characterSize = PixelScaleConstant * textScale;
	}
	
	private void CalculateHitArea(){
		Quaternion previousRotation  =transform.rotation;
		transform.rotation = Quaternion.identity;
		
			measuredSize.x = MeshBounds.size.x;
			measuredSize.y = MeshBounds.size.y;
		transform.rotation = previousRotation;
//		ApplyRotation();
	}
	
	/////SKIP MESH BOUNDS - DO DIRECT CALC?
	//this may be slow, hopefully we don't recalc often
	protected override void AssignTotalSizeValues(){
		// Debug.Log(" a: " + MeshBounds.size);
		if (textLayout == UITextLayout.Dynamic || textLayout == UITextLayout.DynamicWrap){
			transform.rotation = Quaternion.identity;
			textMesh.anchor = AnchorPosition;
			TextScale = TotalScale;

			//TODO - this is a fucking hack, to prevent overwriting size with 0
			if(MeshBounds.size.x != 0 && MeshBounds.size.y != 0){
				totalSize.x = MeshBounds.size.x;
				totalSize.y = MeshBounds.size.y;
			}
			// if(totalSize.x == 0){
				// Debug.Log(GetComponent<MeshRenderer>().enabled + " : " + textMesh.text);
				// Debug.Log(GetComponent<MeshRenderer>().sharedMesh.bounds);
			// }
			ApplyRotation();
			FormatText(false);  //this must happen prior to apply, otherwise it wont happen
		}
		else{
			base.AssignTotalSizeValues();
			FormatText(false);
		}
	}



	protected override void ApplySize(){
		// textMesh.anchor = AnchorPosition;
		// TextScale = TotalScale;

		//Why is this commented out?
//		FormatText();
	}

	
	public override bool HitTest(Vector2 inputPosition){
//		Debug.Log(measuredSize);
//		Debug.Log(WorldSize);
		Vector2 transformInput = transform.InverseTransformPoint(inputPosition);
		transformInput.y = -transformInput.y;
		
		inputPosition.x = ( inputPosition.x - (Screen.width * 0.5f) )/UICamera.UIScale;
		inputPosition.y = ( inputPosition.y - (Screen.height * 0.5f) )/UICamera.UIScale;
		
		Vector2 transformedInput = transform.InverseTransformPoint(inputPosition);
		transformedInput.y = -transformedInput.y;
		if(transformedInput.x < -measuredSize.x*(Anchor.x) || transformedInput.x > measuredSize.x*(1-Anchor.x)){
			return false;	
		}
		
		if(transformedInput.y < -measuredSize.y*(Anchor.y) || transformedInput.y > measuredSize.y*(1-Anchor.y)){
			return false;	
		}
		return true;
		
		
	}
	
	
	
	#if UNITY_EDITOR
	protected override void EditorUpdate(){
		// CheckChildren();
		if(textMesh == null){
			textMesh = transform.GetComponent<TextMesh>();
		}	
		base.EditorUpdate();
			
		if(uiFont != null && uiFont.font != null){
			textMesh.font = uiFont.font;
			SetTexture(uiFont.font.material.mainTexture);	
			FormatText(true);
		}
		else{
			textMesh.text = text;	
		}
		textMesh.hideFlags = 0;
		ApplyTextSize();
		
	}
//	protected override void OnDrawGizmos(){
//		
//		base.OnDrawGizmos();
//		textMesh = transform.GetComponent<TextMesh>();
//			
//	}
	#endif

}
