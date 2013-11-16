using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UIShader{Normal, Normal8Bit, Additive,Additive8Bit,Multiplicative, Multiplicative8Bit, DepthMask, NormalSolid, DepthMaskCutout, MultiplicativeDouble}

public abstract class UIGraphicNode : UITransformNode {

	[SerializeField][HideInInspector] private MeshRenderer meshRenderer;
	[HideInInspector] private Material material; //this doesn't get saved, so it doesn't get copied...
	

	[SerializeField] private UIShader uiShader;
	// [SerializeField] private Shader customShader;
	// [SerializeField] private Shader shader;

	[SerializeField] private Material customMaterial;

	[SerializeField] private Color color = new Color(1,1,1,1);

	private static Dictionary<UIShader, Dictionary<Texture,Material> > shaderMaterialDictionary = new Dictionary <UIShader, Dictionary<Texture,Material> >();




	// static Shader Normal;
	// static Shader Normal8Bit;
	// static Shader Additive;
	// static Shader Additive8Bit;
	// static Shader Multiplicative;
	// static Shader Multiplicative8Bit;
	// static Shader DepthMask;
	// static Shader NormalSolid;
	// static Shader DepthMaskCutout;
	// static Shader MultiplicativeDouble;


	static UIGraphicNode(){
		// Normal = GetShader( UIShader.Normal );
		// Normal8Bit = GetShader( UIShader.Normal8Bit );
		// Additive = GetShader( UIShader.Additive );
		// Additive8Bit = GetShader( UIShader.Additive8Bit );
		// Multiplicative = GetShader( UIShader.Multiplicative );
		// Multiplicative8Bit = GetShader( UIShader.Multiplicative8Bit );
		// DepthMask = GetShader( UIShader.DepthMask );
		// NormalSolid = GetShader( UIShader.NormalSolid );
		// DepthMaskCutout = GetShader( UIShader.DepthMaskCutout );
		// MultiplicativeDouble = GetShader( UIShader.MultiplicativeDouble );

		shaderMaterialDictionary.Add( UIShader.Normal ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.Normal8Bit ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.Additive ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.Additive8Bit ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.Multiplicative ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.Multiplicative8Bit,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.DepthMask ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.NormalSolid ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.DepthMaskCutout ,new Dictionary<Texture,Material>());
		shaderMaterialDictionary.Add( UIShader.MultiplicativeDouble,new Dictionary<Texture,Material>());

	}
	
	protected override void Awake(){
		base.Awake();
		InitializeMaterial();	
		SetColor();
	}
	

//	protected Vector2 MainTextureScale{
//		get{ return material.mainTextureScale;}
//		set{
//			material.mainTextureScale = value;	
//		}	
//	}
	
	private void InitializeMaterial(){


		
		//if(NormalMaterial == null){
		//	Debug.Log("null");
		//	NormalMaterial = new Material(GetShader(UIShader.Normal));
		//}
		//material = NormalMaterial;
		//material = new Material(GetShader(uiShader));


//		material.hideFlags = HideFlags.HideAndDontSave;
	
		if(meshRenderer == null){
			meshRenderer = transform.GetComponent<MeshRenderer>();
			if(meshRenderer == null){
				meshRenderer = gameObject.AddComponent<MeshRenderer>();
			}
		}
		//meshRenderer.material = material;	
	}
	
	public Color Color{
		get{ return color; }
		set{
			color = value;	
			SetColor();
		}	
	}
	
	
	public UIShader UIShader{
		get{
			return uiShader;	
		}	
		set{
			//editor only for now
			if(!Application.isPlaying){
				uiShader = value;	
				GetShader(uiShader);
				
				if(material != null){
					SetTexture(material.mainTexture);
				}

				
			}
		}
	}

	// void GetShader(){
		// if(customShader != null){
				// shader = customShader;
		// }
		// else{
			// shader  = GetShader( uiShader );
					// Debug.Log(shader);
		// }
		// if(shader == null){
			// Debug.Log(customShader + " : " + uiShader);
		// }
	// }

	private static Shader GetShader(UIShader shade){
		switch(shade){
			case UIShader.Normal:
			default:
				return Shader.Find("GUI/Normal");
				
			case UIShader.Normal8Bit:
				return Shader.Find("GUI/Normal-8Bit");
				
			case UIShader.Additive:
				return Shader.Find("GUI/Additive");	
			case UIShader.Multiplicative:
				return Shader.Find("GUI/Multiplicative");
			case UIShader.Additive8Bit:
				return Shader.Find("GUI/Additive-8Bit");
			case UIShader.Multiplicative8Bit:
				return Shader.Find("GUI/Multiplicative-8Bit");
			case UIShader.DepthMask:
				return Shader.Find("Masked/Mask");
			case UIShader.NormalSolid:
				return Shader.Find("GUI/Normal-Solid");
			case UIShader.DepthMaskCutout:
				return Shader.Find("Masked/Mask-Cutout");	
			case UIShader.MultiplicativeDouble:
				return Shader.Find("GUI/MultiplicativeDouble");		

		}	
	}
	
	protected Bounds MeshBounds{
		get{
			return meshRenderer.bounds;	
		}	
	}
	protected override void ApplyTransparency(){
		SetColor();
	}
	
	
	
	private void SetColor(){
		float visibility = color.a * totalTransparency;
		ApplyColor(new Color(color.r, color.g, color.b, visibility));
		if(visibility > 0 && material != null){
			SetVisible();
		}
		else{
			// Debug.Log(gameObject.name + " : " + material);
			SetHidden();
		}
		
	}
	protected abstract void ApplyColor(Color c);


	private void SetVisible(){
		meshRenderer.enabled = true;
	}
	private void SetHidden(){
		meshRenderer.enabled = false;

	}

	protected void SetTexture(Texture t){
		//if(material == null){
		//	InitializeMaterial();	
		//}
		
		if(t != null){
			t.mipMapBias = -1f;

			if(customMaterial != null){
				if(material != customMaterial){
					material = new Material(customMaterial);

					meshRenderer.material = material;
				}
				material.mainTexture = t;	
			}
			else{
			// if(shaderMaterialDictionary.ContainsKey(shader)){
				SetupMaterial(shaderMaterialDictionary[uiShader],t);
			// }
			// else{
				// shaderMaterialDictionary.Add( shader ,new Dictionary<Texture,Material>());
				// SetupMaterial(shaderMaterialDictionary[shader],t);
			// }
			}

		}
	}
	///Make sure shaders are pre-warmed
	private void SetupMaterial(Dictionary<Texture, Material> materialDictionary, Texture t){
		if(Application.isPlaying){
			if(materialDictionary.ContainsKey(t)){
				material = materialDictionary[t];
				meshRenderer.material = material;
			}
			else{
				//GetShader();
				material = new Material(GetShader(uiShader));
				material.mainTexture = t;	
				meshRenderer.material = material;
				materialDictionary.Add(t,material);
			}
		}
		else{
			//GetShader();
			if(material == null){
				material = new Material(GetShader(uiShader) );	

			}
			else{
				material.shader = GetShader(uiShader);
			}
			material.hideFlags = HideFlags.None;

			material.mainTexture = t;	
			meshRenderer.material = material;
		}


	}




	#if UNITY_EDITOR
	///Editor code
	protected override void EditorUpdate(){
		if(meshRenderer == null){
			meshRenderer = transform.GetComponent<MeshRenderer>();
			if(meshRenderer == null){
				meshRenderer = gameObject.AddComponent<MeshRenderer>();
			}
		}	
		base.EditorUpdate();
		
		meshRenderer.hideFlags =HideFlags.None ;// HideFlags.HideInInspector; //NotEditable
		//if(material == null){
		//	material = new Material(GetShader(uiShader) );	
		//}
		//else{
		//	material.shader = GetShader(uiShader);
		//}
//		UpdateTransparency();
		SetColor();
		
		//material.hideFlags = HideFlags.DontSave;  //dont save?
	}
	#endif
}
