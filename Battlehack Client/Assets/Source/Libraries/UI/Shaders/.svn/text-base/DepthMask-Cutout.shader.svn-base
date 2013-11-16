Shader "Masked/Mask-Cutout" {
	Properties {

		_MainTex ("Main Texture", 2D) = "" {}
	}
	SubShader {
		// Render the mask before everything
		Blend Off
		Tags {"Queue" = "Geometry-1" }
		
		// Don't draw in the RGBA channels; just the depth buffer
		Lighting Off
		Fog { Mode Off }
		ColorMask 0
		ZWrite On
		AlphaTest Greater 0.5
		// Do nothing specific in the pass:
		
		Pass { SetTexture [_MainTex] {}}
	}
}