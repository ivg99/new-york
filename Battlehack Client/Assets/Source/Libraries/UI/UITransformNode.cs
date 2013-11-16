using UnityEngine;
using System.Collections;

public class UITransformNode : UINode {
	
	[SerializeField] private Vector2 position;
	[SerializeField] private Vector2 relativePosition;
	[SerializeField] private TextAnchor anchorPosition;
	[SerializeField][HideInInspector] private Vector2 anchor;
	[SerializeField] private Vector2 size;
	[SerializeField] private Vector2 relativeSize;

	[SerializeField] private float scale = 1;	
	
	[SerializeField] private int depth;
	[SerializeField] private float rotation;

	[SerializeField] private float transparency = 1;
	





	[SerializeField] protected Vector2 totalSize;
	[SerializeField][HideInInspector] protected Vector2 internalSize;

	[SerializeField][HideInInspector] private Vector3 transformPosition;
	
	protected float totalTransparency = 1;	
	protected float totalScale = 1;	
	private float lastScreenWidth;
	private float lastScreenHeight;
	
	//private bool applyPosition = false;
	//private bool applyRotation = false;
	

	protected virtual void Start(){
		if(parent == null){
			UpdateTransparency();
			 // UpdateSize();

			UpdateDepth();
		}
	}





	protected override void Update(){
		base.Update();	

		if(parent == null){
            if(lastScreenWidth != Screen.width || lastScreenHeight != Screen.height){
        		    lastScreenWidth = Screen.width;
					lastScreenHeight = Screen.height;
            		UICamera.UpdateScreen();
                    UpdateSize();//

            }  
        }
//		if(HitTest(Input.mousePosition)){
//		Debug.Log(name+ Time.time);
//		}
	}
	


	/*protected virtual void LateUpdate(){


		if(applyPosition){
			applyPosition = false;
			transform.localPosition = transformPosition;		
		}
		if(applyRotation){
			applyRotation = false;
			transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);	
		}

	}*/

	public Vector2 Position{
		get{ return position;}
		set{
			if(position.x != value.x || position.y != value.y){
				position = value;
				UpdatePosition();
			}
		}	
	}
		
	public Vector2 RelativePosition{
		get{ return relativePosition;}
		set{
			if(relativePosition.x != value.x || relativePosition.y != value.y){
				relativePosition = value;
				UpdatePosition();
			}
		}	
	}
	public TextAnchor AnchorPosition{
		get{ return anchorPosition;}
		set{
			if(anchorPosition != value){
				anchorPosition = value;
				
				
				
				UpdateAnchor();
			}
		}	
	}
	public Vector2 Anchor{
		get{ return anchor;}
//		private set{
//			if(anchor.x != value.x || anchor.y != value.y){
//				anchor = value;
//				UpdateAnchor();
//			}
//		}	
	}
	
	public Vector2 Size{
		get{ return size;}
		set{
			if(size.x != value.x || size.y != value.y){
				size = value;
				UpdateSize();
			}
		}	
	}
			
	public Vector2 RelativeSize{
		get{ return relativeSize;}
		set{
			if(relativeSize.x != value.x || relativeSize.y != value.y){
				relativeSize = value;
				UpdateSize();
			}
		}	
	}
	public int Depth{
		get{ return depth; }
		set{
			if(depth != value){
					
				depth = value;
				UpdateDepth();
			}	
		}
	}
	public int WorldDepth{
		get{ return (int) -transform.position.z; }
	}
	public float Rotation{
		get{ return rotation; }
		set{
			if(rotation != value){
				rotation = value;
				ApplyRotation();
			}	
		}	
	}

	public float Transparency{
		get{ return transparency; }
		set{
			value = Mathf.Clamp01(value);
			if(transparency != value){
				transparency = value;	
				UpdateTransparency();
			}
		}	
	}
	public float Scale{
		get{ return scale; }
		set{
			if(scale != value){
				scale = value;	
				UpdateSize();
			}
		}	
	}
	
	public float TotalTransparency{
		get{ return totalTransparency; }
	}
	public float TotalScale{
		get{ return totalScale; }
	}
	
	public Vector2 WorldSize { get{ return totalSize;}}

	public float ParentTransparency{
		get{ 
			if(parent != null){
				return parent.TotalTransparency; 
			}
			return 1;
		}
	}
	public float ParentScale{
		get{ 
			if(parent != null){
				return parent.TotalScale; 
			}
			return 1;
		}
	}

	public Vector2 ParentWorldSize{
		get{ 
			if(parent != null){
				return parent.WorldSize; 
			}
			return new Vector2(UICamera.UIScreenWidth,UICamera.UIScreenHeight);
			
		}	
	}

	
	private Vector2 ParentAnchor{
		get{
			if(parent != null){
				return new Vector2(-parent.WorldSize.x * parent.Anchor.x, parent.WorldSize.y * parent.Anchor.y);
			}
			else{
				return new Vector2( -UICamera.UIScreenWidth*0.5f, UICamera.UIScreenHeight*0.5f );
			}
		}
	}
		
	private void UpdatePosition(){
		float localX = ParentScale*position.x + (relativePosition.x*ParentWorldSize.x);
		float localY = ParentScale*position.y + (relativePosition.y*ParentWorldSize.y);
		
		transformPosition.x = localX + ParentAnchor.x;
		transformPosition.y = ParentAnchor.y - localY;
		
		ApplyPosition();
		
	}
	
	public void UpdateSize(){
		


		CalculateSize();
		
		ApplySize();
		UpdatePosition();
		
		UpdateChildrenSize();  //since we recalc position anyway when updating size, children will get anchor updates
	}
	

	private void UpdateChildrenSize(){
		//TODO - fix this too
		if(children != null){
		for(int i=0; i<children.Length; i++){
			children[i].UpdateSize();
		}
		}
		else{
			Debug.Log(transform.name);
		}
	}
	
	private void UpdateDepth(){
		transformPosition.z = -depth;
		ApplyPosition();
		
	}
	private void UpdateAnchor(){
		switch(anchorPosition){
					
			case TextAnchor.UpperLeft:
				anchor.x = 0;
				anchor.y = 0;
			break;
			case TextAnchor.MiddleLeft:
				anchor.x = 0;
				anchor.y = .5f;
			break;
			case TextAnchor.LowerLeft:
				anchor.x = 0;
				anchor.y = 1;
			break;

			case TextAnchor.UpperCenter:
				anchor.x = .5f;
				anchor.y = 0;
			break;
			case TextAnchor.MiddleCenter:
				anchor.x = .5f;
				anchor.y = .5f;
			break;
			case TextAnchor.LowerCenter:
				anchor.x = .5f;
				anchor.y = 1;
			break;

			case TextAnchor.UpperRight:
				anchor.x = 1;
				anchor.y = 0;
			break;
			case TextAnchor.MiddleRight:
				anchor.x = 1;
				anchor.y = .5f;
			break;
			case TextAnchor.LowerRight:
				anchor.x = 1;
				anchor.y = 1;
			break;

		}
		//make sure
		ApplySize();
		
		//changes to the parent anchor do not affect children anchor, but do affect positions of immediate children
		UpdateChildrenPosition();
	}
	
	private void UpdateTransparency(){

		totalTransparency = transparency * ParentTransparency;

		ApplyTransparency();
		UpdateChildrenTransparency();
	}
	private void UpdateChildrenTransparency(){
		////TODO: this is a hack until we fix order of updates, all the children should be calculated from the root node first, and operations should happen depth-first
		if(children != null){
			for(int i=0; i<children.Length; i++){
				children[i].UpdateTransparency();
			}
		}
	}
	
	private void UpdateChildrenPosition(){
		for(int i=0; i<children.Length; i++){
			children[i].UpdatePosition();
		}
	}
	
	protected virtual void ApplySize(){}
	
	private void ApplyPosition(){
		//applyPosition = true;
		transform.localPosition = transformPosition;		
		
	}
	protected void ApplyRotation(){
		//applyRotation = true;		
		transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);		
	}
	
	protected virtual void ApplyTransparency(){
		
	}
	protected void CalculateSize(){
//		if(inheritScale){
			totalScale = scale * ParentScale;
//		}
//		else{
//			totalScale = scale;	
//		}

		internalSize.x = totalScale*size.x + scale*(relativeSize.x*ParentWorldSize.x);
		internalSize.y = totalScale*size.y + scale*(relativeSize.y*ParentWorldSize.y);
		
		if(internalSize.x < 0){
			internalSize.x = 0;
		}
		if(internalSize.y < 0){
			internalSize.y = 0;
		}
		AssignTotalSizeValues();
	}
	protected virtual void AssignTotalSizeValues(){
		totalSize = internalSize;
	}
	
	public virtual bool HitTest(Vector2 inputPosition){
		
		//center the input, then scale it
		inputPosition.x = ( inputPosition.x - (Screen.width * 0.5f) )/UICamera.UIScale;
		inputPosition.y = ( inputPosition.y - (Screen.height * 0.5f) )/UICamera.UIScale;
		
		Vector2 transformedInput = transform.InverseTransformPoint(inputPosition);
		transformedInput.y = -transformedInput.y;
		if(transformedInput.x < -WorldSize.x*(Anchor.x) || transformedInput.x > WorldSize.x*(1-Anchor.x)){
			return false;	
		}
		
		if(transformedInput.y < -WorldSize.y*(Anchor.y) || transformedInput.y > WorldSize.y*(1-Anchor.y)){
			return false;	
		}
		return true;
	}

	

	#if UNITY_EDITOR
	public void SetDirty(){
		//applyPosition = true;
	}
	
	
	
	protected override void EditorUpdate(){
		// CheckChildren();

		base.EditorUpdate();	
		
		/*if(!applyPosition){
			if(transform.localPosition.x != transformPosition.x || transform.localPosition.y != transformPosition.y || (int)transform.localPosition.z != transformPosition.z){
				Position = new Vector3(Position.x + transform.localPosition.x - transformPosition.x,Position.y -( transform.localPosition.y - transformPosition.y) );
				depth = (int)(-transform.localPosition.z);
				UpdateDepth();
				ApplyPosition();
			}
			if(transform.localEulerAngles.z != rotation){
				rotation = transform.localEulerAngles.z;
				ApplyRotation();
			}
			
			if(transform.localScale.x != 1 || transform.localScale.y != 1 || transform.localScale.z != 1){
				transform.localScale = new Vector3(1,1,1);
//				Debug.Log(transform.localScale);
			}
			
			
		}
		else{       */                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
			
		
			transparency = Mathf.Clamp01(transparency);
			UpdateTransparency();
			
			UpdateAnchor();
			//CheckScreen();
			UpdateSize();
			UpdateDepth();
			ApplyRotation();
			transform.localScale = Vector3.one;
		//}
		
	}
	#endif
}
