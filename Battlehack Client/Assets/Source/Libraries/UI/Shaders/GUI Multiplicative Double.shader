Shader "GUI/MultiplicativeDouble" {
Properties {
	//_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Main Texture", 2D) = "" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend DstColor SrcColor
	AlphaTest Off
	
	Cull Off Lighting Off ZWrite Off Fog { Mode Off }
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				//constantColor [_Color]
				combine texture * primary
			}
			SetTexture [_MainTex] {
				constantColor (0.5,0.5,0.5,1)
				combine previous lerp( previous ALPHA) constant
			}
		}
	}
	
}
}
