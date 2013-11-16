Shader "Masked/Mask" {
	Properties {

		
	}
	SubShader {
		// Render the mask before everything
		Blend Off
		Tags {"Queue" = "Geometry-1" }
		
		// Don't draw in the RGBA channels; just the depth buffer
		Lighting Off
		
		ColorMask 0
		ZWrite On
		Fog { Mode Off }
		AlphaTest Off
		// Do nothing specific in the pass:
		
		Pass { }
	}
}