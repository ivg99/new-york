using UnityEngine;
using System.Collections;


[RequireComponent (typeof (MeshFilter))]
public class UITextureNode : UIGraphicNode {

	[SerializeField][HideInInspector] private MeshFilter meshFilter;

	[HideInInspector] private Mesh m; //this doesn't get saved, so it doesn't get copied...
	[SerializeField][HideInInspector] private Vector3[] vertices = new Vector3[4];
	[SerializeField][HideInInspector] private Color[] colors = new Color[4];
	[SerializeField][HideInInspector] private Vector2[] uvs = new Vector2[4];
	[SerializeField] private Texture texture;
	
	[SerializeField] private Vector2 uvScale = new Vector2(1,1);
	[SerializeField] private Vector2 uvOffset = new Vector2(0,0);

	//private bool needsRebuild = false;
	
	public Texture Texture{
		get{ return texture;}
		set{
			texture = value;
			SetTexture(texture);
		}	
	}
	
	public Vector2 UVScale{
		get{
			return uvScale;
		}
		set{
			uvScale = value;
			RecalculateUVs();
		}
	}
	public Vector2 UVOffset{
		get{
			return uvOffset;
		}
		set{
			uvOffset = value;
			RecalculateUVs();
		}
	}
	

	protected override void Awake(){
		
//		#if UNITY_EDITOR
//			if(!Application.isPlaying){
//				EditorUpdate();
//			}
//		#endif
		
		RegenerateMeshData();
		base.Awake();
		
		SetTexture(texture);
	}
		
	protected override void ApplySize(){
		RecalculateMesh();	
		
	}
	protected override void ApplyColor(Color c){
		colors[0] = colors[1] = colors[2] = colors[3] = c;
		if(m != null){
			m.colors = colors;
		}
	}
	private void RecalculateMesh(){



		for(int i=0; i<4; i++){
			
			vertices[i] = new Vector3(WorldSize.x*(QuadULVertices[i].x - Anchor.x),WorldSize.y*(QuadULVertices[i].y + Anchor.y),0);
		}

		//this check is only here to support editor functionality
		if(m == null){
			RegenerateMeshData();
		}
		
		m.vertices = vertices;
		m.RecalculateBounds();	
	}
	
	private void RecalculateUVs(){
		uvs[0] = uvOffset;
		uvs[1] = new Vector2(uvOffset.x + uvScale.x, uvOffset.y);
		uvs[2] = new Vector2(uvOffset.x, uvOffset.y + uvScale.y);
		uvs[3] = new Vector2(uvOffset.x + uvScale.x, uvOffset.y + uvScale.y);
		m.uv = uvs;
	}

	
	private void RegenerateMeshData(){
		
		//if(m == null){
			m = new Mesh();
//			m.MarkDynamic();
			if(vertices.Length != 4){
				vertices = new Vector3[4];
				
			}
			RecalculateMesh();
			RecalculateUVs();
			//m.uv = QuadUvs;
			m.triangles = QuadTriangles;
			if(meshFilter == null){
				meshFilter = transform.GetComponent<MeshFilter>();
				if(meshFilter == null){
					meshFilter = gameObject.AddComponent<MeshFilter>();	
				}
			}	
			meshFilter.hideFlags = HideFlags.NotEditable;
			meshFilter.mesh = m;
		//}
		
		
	}

	
	//Static precalculated data

														
	/*private static readonly Vector2[] QuadUvs= new Vector2[]{
											new Vector2(0, 0),
											new Vector2(1, 0),
											new Vector2(0, 1),
											new Vector2(1, 1),
											};*/
	private static readonly int[] QuadTriangles = new int[]{
													0, 2, 3,
													3, 1, 0,
													};
	private static readonly Vector2[] QuadULVertices = new Vector2[]{
														new Vector2( 0, -1),
														new Vector2( 1, -1),
														new Vector2( 0, 0),
														new Vector2( 1, 0),
														};

														
														
	#if UNITY_EDITOR
	
	public void GetSizeFromTexture(){
		if(texture != null){
			Debug.Log(texture.width);
			Size = new Vector2(texture.width, texture.height);
		}
	}
	
	protected override void EditorUpdate(){
		// CheckChildren();
		if(meshFilter == null){
			meshFilter = transform.GetComponent<MeshFilter>();
			if(meshFilter == null){
				meshFilter = gameObject.AddComponent<MeshFilter>();	
			}
		}	
		meshFilter.hideFlags = HideFlags.HideInInspector;
		if(m == null){
			RegenerateMeshData();
		}
		//we make sure we can respond properly
		base.EditorUpdate();
		
		SetTexture(texture);
	}
	#endif
}
