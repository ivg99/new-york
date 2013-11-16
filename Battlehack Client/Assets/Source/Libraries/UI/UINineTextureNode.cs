using UnityEngine;
using System.Collections;

//tomorrow - material work (not in project, saved in scene? - requires texture and shader reference), get the text stuff working


[RequireComponent (typeof (MeshFilter))]
public class UINineTextureNode : UIGraphicNode {

	[SerializeField][HideInInspector] private MeshFilter meshFilter;

	[HideInInspector] private Mesh m; //this doesn't get saved, so it doesn't get copied...
	[SerializeField][HideInInspector] private Vector3[] vertices = new Vector3[16];
	[SerializeField][HideInInspector] private Color[] colors = new Color[16];
	[SerializeField] private Texture texture;
	
	[SerializeField] private float left;
	[SerializeField] private float right;
	[SerializeField] private float top;
	[SerializeField] private float bottom;
	
	//private bool needsRebuild = false;
	
	public Texture Texture{
		get{ return texture;}
		set{
			texture = value;
			SetTexture(texture);
		}	
	}
	
	protected override void Awake(){
		
		#if UNITY_EDITOR
			if(!Application.isPlaying){
				EditorUpdate();
			}
		#endif
		
		RegenerateMeshData();
		base.Awake();
		
		SetTexture(texture);
	}
		
	protected override void ApplySize(){
		//needsRebuild = true;
		RecalculateMesh();	
		
	}
	protected override void ApplyColor(Color c){
		colors[0] = colors[1] = colors[2] = colors[3]
		= colors[4] = colors[5] = colors[6] = colors[7]
		= colors[8] = colors[9] = colors[10] = colors[11]
		= colors[12] = colors[13] = colors[14] = colors[15] = c;
		m.colors = colors;
	}
	private void RecalculateMesh(){
//		Vector2[] NineVertices;
//		
//		
//		
//		switch(AnchorPoint){
//			case TextAnchor.UpperLeft:
//				NineVertices = NineULVertices;
//				break;
//			case TextAnchor.UpperCenter:
//				NineVertices = NineUCVertices;
//				break;
//			case TextAnchor.UpperRight:
//				NineVertices = NineURVertices;
//				break;
//
//			
//			case TextAnchor.MiddleLeft:
//				NineVertices = NineMLVertices;
//				break;
//			case TextAnchor.MiddleCenter:
//			default:
//				NineVertices = NineMCVertices;
//				break;
//			case TextAnchor.MiddleRight:
//				NineVertices = NineMRVertices;
//				break;
//
//			case TextAnchor.LowerLeft:
//				NineVertices = NineLLVertices;
//				break;
//			case TextAnchor.LowerCenter:
//				NineVertices = NineLCVertices;
//				break;
//			case TextAnchor.LowerRight:
//				NineVertices = NineLRVertices;
//				break;
//		
//		}
		
		
//		for(int i=0; i<4; i++){
//			
//			vertices[i] = new Vector3(WorldSize.x*(QuadULVertices[i].x - Anchor.x),WorldSize.y*(QuadULVertices[i].y + Anchor.y),0);
//		}
		
//		for(int i=0; i<16; i++){
//			
//			vertices[i] = new Vector3(WorldSize.x*NineVertices[i].x,WorldSize.y*NineVertices[i].y,0);
//		}
		
			
		
		
		//Bottom Row
		vertices[0] = new Vector3(WorldSize.x*(NineULVertices[0].x - Anchor.x),WorldSize.y*(NineULVertices[0].y + Anchor.y),0);
		vertices[3] = new Vector3(WorldSize.x*(NineULVertices[3].x - Anchor.x),vertices[0].y,0);
		
		float secondColumn = vertices[0].x + left;
		float thirdColumn = vertices[3].x - right;
		if(secondColumn > thirdColumn){
			secondColumn = thirdColumn = 0.5f*(secondColumn + thirdColumn);	
		}
		
		//Top Row
		vertices[12] = new Vector3(vertices[0].x,WorldSize.y*(NineULVertices[12].y + Anchor.y),0);
		vertices[15] = new Vector3(vertices[3].x,vertices[12].y,0);
		
		float secondRow = vertices[0].y + bottom;
		float thirdRow = vertices[12].y - top;
		
		if(secondRow > thirdRow){
			secondRow = thirdRow = 0.5f*(secondRow + thirdRow);	
		}
		
		vertices[1] = new Vector3(secondColumn,vertices[0].y,0);
		vertices[2] = new Vector3(thirdColumn,vertices[0].y,0);
		
		vertices[4] = new Vector3(vertices[0].x,secondRow,0);
		vertices[5] = new Vector3(secondColumn,secondRow,0);
		vertices[6] = new Vector3(thirdColumn,secondRow,0);
		vertices[7] = new Vector3(vertices[3].x,secondRow,0);
		
		vertices[8] = new Vector3(vertices[0].x,thirdRow,0);
		vertices[9] = new Vector3(secondColumn,thirdRow,0);
		vertices[10] = new Vector3(thirdColumn,thirdRow,0);
		vertices[11] = new Vector3(vertices[3].x,thirdRow,0);
		
		
		vertices[13] = new Vector3(secondColumn,vertices[12].y,0);
		vertices[14] = new Vector3(thirdColumn,vertices[12].y,0);
		
		//this check is only here to support editor functionality
		if(m == null){
			RegenerateMeshData();
		}
		
		m.vertices = vertices;
		m.RecalculateBounds();	
	}
	
	
	/*protected override void LateUpdate(){
		base.LateUpdate();
		if(needsRebuild){
			
			RecalculateMesh();	
			needsRebuild = false;
		}
	}*/
	
	private void RegenerateMeshData(){
		
		

		//if(m == null){
			m = new Mesh();
//			m.MarkDynamic();
			if(vertices.Length != 16){
				vertices = new Vector3[16];
				
			}
			RecalculateMesh();
			m.uv = NineUvs;
			m.triangles = NineTriangles;
			meshFilter.mesh = m;
		//}
		
		
	}



	
	
	//Static precalculated data

														
	private static readonly Vector2[] NineUvs= new Vector2[]{
											new Vector2(0		,0),
											new Vector2(1/3f	,0),
											new Vector2(2/3f	,0),
											new Vector2(1		,0),
											
											new Vector2(0		,1/3f),
											new Vector2(1/3f	,1/3f),
											new Vector2(2/3f	,1/3f),
											new Vector2(1		,1/3f),
											
											new Vector2(0		,2/3f),
											new Vector2(1/3f	,2/3f),
											new Vector2(2/3f	,2/3f),
											new Vector2(1		,2/3f),
											
											new Vector2(0		,1),
											new Vector2(1/3f	,1),
											new Vector2(2/3f	,1),
											new Vector2(1		,1),
											
											
											};
	private static readonly int[] NineTriangles = new int[]{
													0, 4, 5,
													5, 1, 0,
													
													1, 5, 6,
													6, 2, 1,
													
													2, 6, 7,
													7, 3, 2,
													
													
													4, 8, 9,
													9, 5, 4,
													
													5, 9, 10,
													10, 6, 5,
													
													6, 10, 11,
													11, 7, 6,
													
													
													8, 12, 13,
													13, 9, 8,
													
													9, 13, 14,
													14, 10, 9,
													
													10, 14, 15,
													15, 11, 10,
													};
	private static readonly Vector2[] NineULVertices = new Vector2[]{
											new Vector2(0		,0 -1),
											new Vector2(1/3f	,0 -1),
											new Vector2(2/3f	,0 -1),
											new Vector2(1		,0 -1),
											
											new Vector2(0		,1/3f -1),
											new Vector2(1/3f	,1/3f -1),
											new Vector2(2/3f	,1/3f -1),
											new Vector2(1		,1/3f -1),
											
											new Vector2(0		,2/3f -1),
											new Vector2(1/3f	,2/3f -1),
											new Vector2(2/3f	,2/3f -1),
											new Vector2(1		,2/3f -1),
											
											new Vector2(0		,1 -1),
											new Vector2(1/3f	,1 -1),
											new Vector2(2/3f	,1 -1),
											new Vector2(1		,1 -1),
	};
	
	/*private static readonly Vector2[] NineUCVertices = new Vector2[]{
											new Vector2(0-0.5f		,0-1),
											new Vector2(1/3f-0.5f	,0-1),
											new Vector2(2/3f-0.5f	,0-1),
											new Vector2(1	-0.5f	,0-1),
											
											new Vector2(0-0.5f		,1/3f-1),
											new Vector2(1/3f-0.5f	,1/3f-1),
											new Vector2(2/3f-0.5f	,1/3f-1),
											new Vector2(1-0.5f		,1/3f-1),
											
											new Vector2(0	-0.5f	,2/3f-1),
											new Vector2(1/3f-0.5f	,2/3f-1),
											new Vector2(2/3f-0.5f	,2/3f-1),
											new Vector2(1	-0.5f	,2/3f-1),
											
											new Vector2(0	-0.5f	,1-1),
											new Vector2(1/3f-0.5f	,1-1),
											new Vector2(2/3f-0.5f	,1-1),
											new Vector2(1-0.5f		,1-1),
	};
	private static readonly Vector2[] NineURVertices = new Vector2[]{
											new Vector2(0-1		,0-1),
											new Vector2(1/3f-1	,0-1),
											new Vector2(2/3f-1	,0-1),
											new Vector2(1	-1	,0-1),
											
											new Vector2(0-1		,1/3f-1),
											new Vector2(1/3f-1	,1/3f-1),
											new Vector2(2/3f-1	,1/3f-1),
											new Vector2(1-1		,1/3f-1),
											
											new Vector2(0	-1	,2/3f-1),
											new Vector2(1/3f-1	,2/3f-1),
											new Vector2(2/3f-1	,2/3f-1),
											new Vector2(1	-1	,2/3f-1),
											
											new Vector2(0	-1	,1-1),
											new Vector2(1/3f-1	,1-1),
											new Vector2(2/3f-1	,1-1),
											new Vector2(1-1		,1-1),
	};
	private static readonly Vector2[] NineMLVertices = new Vector2[]{
											new Vector2(0		,0 -0.5f),
											new Vector2(1/3f	,0 -0.5f),
											new Vector2(2/3f	,0 -0.5f),
											new Vector2(1		,0 -0.5f),
											
											new Vector2(0		,1/3f -0.5f),
											new Vector2(1/3f	,1/3f -0.5f),
											new Vector2(2/3f	,1/3f -0.5f),
											new Vector2(1		,1/3f -0.5f),
											
											new Vector2(0		,2/3f -0.5f),
											new Vector2(1/3f	,2/3f -0.5f),
											new Vector2(2/3f	,2/3f -0.5f),
											new Vector2(1		,2/3f -0.5f),
											
											new Vector2(0		,1 -0.5f),
											new Vector2(1/3f	,1 -0.5f),
											new Vector2(2/3f	,1 -0.5f),
											new Vector2(1		,1 -0.5f),
	};
	private static readonly Vector2[] NineMCVertices = new Vector2[]{
											new Vector2(0-0.5f		,0-0.5f),
											new Vector2(1/3f-0.5f	,0-0.5f),
											new Vector2(2/3f-0.5f	,0-0.5f),
											new Vector2(1	-0.5f	,0-0.5f),
											
											new Vector2(0-0.5f		,1/3f-0.5f),
											new Vector2(1/3f-0.5f	,1/3f-0.5f),
											new Vector2(2/3f-0.5f	,1/3f-0.5f),
											new Vector2(1-0.5f		,1/3f-0.5f),
											
											new Vector2(0	-0.5f	,2/3f-0.5f),
											new Vector2(1/3f-0.5f	,2/3f-0.5f),
											new Vector2(2/3f-0.5f	,2/3f-0.5f),
											new Vector2(1	-0.5f	,2/3f-0.5f),
											
											new Vector2(0	-0.5f	,1-0.5f),
											new Vector2(1/3f-0.5f	,1-0.5f),
											new Vector2(2/3f-0.5f	,1-0.5f),
											new Vector2(1-0.5f		,1-0.5f),
	};
	private static readonly Vector2[] NineMRVertices = new Vector2[]{
											new Vector2(0-1		,0-0.5f),
											new Vector2(1/3f-1	,0-0.5f),
											new Vector2(2/3f-1	,0-0.5f),
											new Vector2(1	-1	,0-0.5f),
											
											new Vector2(0-1		,1/3f-0.5f),
											new Vector2(1/3f-1	,1/3f-0.5f),
											new Vector2(2/3f-1	,1/3f-0.5f),
											new Vector2(1-1		,1/3f-0.5f),
											
											new Vector2(0	-1	,2/3f-0.5f),
											new Vector2(1/3f-1	,2/3f-0.5f),
											new Vector2(2/3f-1	,2/3f-0.5f),
											new Vector2(1	-1	,2/3f-0.5f),
											
											new Vector2(0	-1	,1-0.5f),
											new Vector2(1/3f-1	,1-0.5f),
											new Vector2(2/3f-1	,1-0.5f),
											new Vector2(1-1		,1-0.5f),
	};
	private static readonly Vector2[] NineLLVertices = new Vector2[]{
											new Vector2(0		,0),
											new Vector2(1/3f	,0),
											new Vector2(2/3f	,0),
											new Vector2(1		,0),
											
											new Vector2(0		,1/3f),
											new Vector2(1/3f	,1/3f),
											new Vector2(2/3f	,1/3f),
											new Vector2(1		,1/3f),
											
											new Vector2(0		,2/3f),
											new Vector2(1/3f	,2/3f),
											new Vector2(2/3f	,2/3f),
											new Vector2(1		,2/3f),
											
											new Vector2(0		,1),
											new Vector2(1/3f	,1),
											new Vector2(2/3f	,1),
											new Vector2(1		,1),
	};
	private static readonly Vector2[] NineLCVertices = new Vector2[]{
											new Vector2(0-0.5f		,0),
											new Vector2(1/3f-0.5f	,0),
											new Vector2(2/3f-0.5f	,0),
											new Vector2(1	-0.5f	,0),
											
											new Vector2(0-0.5f		,1/3f),
											new Vector2(1/3f-0.5f	,1/3f),
											new Vector2(2/3f-0.5f	,1/3f),
											new Vector2(1-0.5f		,1/3f),
											
											new Vector2(0	-0.5f	,2/3f),
											new Vector2(1/3f-0.5f	,2/3f),
											new Vector2(2/3f-0.5f	,2/3f),
											new Vector2(1	-0.5f	,2/3f),
											
											new Vector2(0	-0.5f	,1),
											new Vector2(1/3f-0.5f	,1),
											new Vector2(2/3f-0.5f	,1),
											new Vector2(1-0.5f		,1),
	};
	private static readonly Vector2[] NineLRVertices = new Vector2[]{
											new Vector2(0-1		,0),
											new Vector2(1/3f-1	,0),
											new Vector2(2/3f-1	,0),
											new Vector2(1	-1	,0),
											
											new Vector2(0-1		,1/3f),
											new Vector2(1/3f-1	,1/3f),
											new Vector2(2/3f-1	,1/3f),
											new Vector2(1-1		,1/3f),
											
											new Vector2(0	-1	,2/3f),
											new Vector2(1/3f-1	,2/3f),
											new Vector2(2/3f-1	,2/3f),
											new Vector2(1	-1	,2/3f),
											
											new Vector2(0	-1	,1),
											new Vector2(1/3f-1	,1),
											new Vector2(2/3f-1	,1),
											new Vector2(1-1		,1),
	};*/
														
														
	#if UNITY_EDITOR
	
	public void GetSizeFromTexture(){
		if(texture != null){
			Debug.Log(texture.width);
			Size = new Vector2(texture.width,texture.height);

		}
	}
	
	protected override void EditorUpdate(){
		
		if(meshFilter == null){
			meshFilter = transform.GetComponent<MeshFilter>();
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
