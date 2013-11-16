using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public abstract class UINode : MonoBehaviour {
	
	[SerializeField][HideInInspector] new private Transform transform;
	
	protected UITransformNode parent;
	protected UITransformNode[] children;
	
	protected virtual void Awake(){
		
		//We either initialize at runtime, or serialize the data
		InitializeNode();
		//Consider a non-invasive way to rebuild children
	}
	
	protected virtual void Update(){
		 #if UNITY_EDITOR
		 	if(!Application.isPlaying){
		 		EditorUpdate();	
		 	}
		 #endif
	}
	
	
	public UITransformNode Parent{
		get{
			return parent;
		}
		set{
			if(parent != value){
				if(parent == null){
					transform.parent = null;
				}
				else{
					transform.parent = parent.GetComponent<Transform>();
				}
				SetParent(value);
				
			}
		}	
	}
	
	
	private void CollectChildren(){
		int childCount = 0;
		foreach (Transform t in transform){
			
			UITransformNode uit = t.GetComponent<UITransformNode>();
			if(uit != null){
				uit.CacheTransform();
				childCount++;	
			}
		}
		
		children = new UITransformNode[childCount];
		int index = 0;
		foreach (Transform t in transform){
			UITransformNode uit = t.GetComponent<UITransformNode>();
			if(uit != null){
				children[index] = uit;
				index++;
			}
		}
	}
	private void CollectChildrenRecursively(){

		if(transform == null){
			CacheTransform();	
		}
		if(transform.parent == null){
			if(parent != null){
				parent = null;
			}	
		}
		else{
			UITransformNode realParent = transform.parent.GetComponent<UITransformNode>();
			if(parent != realParent){
				parent = realParent;	
			}
		}

		int childCount = 0;
		foreach (Transform t in transform){
			
			UITransformNode uit = t.GetComponent<UITransformNode>();
			if(uit != null){
				uit.CacheTransform();
				childCount++;	
			}
		}
		
		children = new UITransformNode[childCount];
		int index = 0;
		foreach (Transform t in transform){
			UITransformNode uit = t.GetComponent<UITransformNode>();
			if(uit != null){
				children[index] = uit;
				uit.CollectChildrenRecursively();
				index++;
			}
		}
	}
	public void CacheTransform(){
		transform = GetComponent<Transform>();
		transform.hideFlags = HideFlags.HideInInspector;
	}
	
	protected void SetParent(UITransformNode newParent){
		UITransformNode previousParent = parent;
		parent = newParent;	
		
		if(previousParent != null){
			previousParent.CollectChildren();
		}
		if(parent != null){
			parent.CollectChildren();	
		}
	}
	
	private void InitializeNode(){
		/*if(transform == null){
			CacheTransform();	
		}
		if(transform.parent == null){
			if(parent != null){
				parent = null;
			}	
		}
		else{
			UITransformNode realParent = transform.parent.GetComponent<UITransformNode>();
			if(parent != realParent){
				parent = realParent;	
			}
		}*/
		if(transform == null){
			CacheTransform();	
		}
		if(transform.parent == null){
			CollectChildrenRecursively();
		}
		//CollectChildren();
	}
	
	
	///Editor Code
	
	protected virtual void EditorUpdate(){
		SetupTransform();
		CheckParent();
		CheckChildren();
	}
	
	public void CheckParent(){
		
		if(transform.parent == null){
			if(parent != null){
				SetParent(null);
			}	
		}
		else{
			UITransformNode realParent = transform.parent.GetComponent<UITransformNode>();
			if(parent != realParent){
				SetParent(realParent);
			}
		}
	}
	public void CheckChildren(){
		
		CollectChildren();
		
	}
	public void SetupTransform(){
		if(transform == null){
			CacheTransform();
		}
	}
}
