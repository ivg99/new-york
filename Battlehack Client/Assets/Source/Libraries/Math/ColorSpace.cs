using UnityEngine;
using System.Collections;


//this isn't complete - need to linearize the RGB values to match photoshop to prove it works, then switch back to D65 regular.
public static class ColorSpace {


	//D65 white point
//	public static Vector3 RGBToXYZ(Vector3 rgb){
//		Vector3 result = new Vector3();
//		
//		result.x = (rgb.x * 0.4124564f) + (rgb.y * 0.3575761f) + (rgb.z * 0.1804375f);
//		result.y = (rgb.x * 0.2126729f) + (rgb.y * 0.7151522f) + (rgb.z * 0.0721750f);
//		result.z = (rgb.x * 0.0193339f) + (rgb.y * 0.1191920f) + (rgb.z * 0.9503041f);
//		
//		return result;
//	}
//	
//	public static Vector3 XYZToRGB(Vector3 xyz){
//		Vector3 result = new Vector3();
//		
//		result.x = (xyz.x * 3.2404542f) + (xyz.y * -1.5371385f) + (xyz.z * -0.4985314f);
//		result.y = (xyz.x * -0.9692660f) + (xyz.y * 1.8760108f) + (xyz.z * 0.0415560f);
//		result.z = (xyz.x * 0.0556434f) + (xyz.y * -0.2040259f) + (xyz.z * 1.0572252f);
//		
//		return result;
//	}
	//D50 white point - matches photoshop
	public static Vector3 RGBToXYZ(Vector3 rgb){
		Vector3 result = new Vector3();
		
		result.x = (rgb.x * 0.4360747f) + (rgb.y * 0.3850649f) + (rgb.z * 0.1430804f);
		result.y = (rgb.x * 0.2225045f) + (rgb.y * 0.7168786f) + (rgb.z * 0.0606169f);
		result.z = (rgb.x * 0.0139322f) + (rgb.y * 0.0971045f) + (rgb.z * 0.7141733f);
		
		return result;
	}
	
	public static Vector3 XYZToRGB(Vector3 xyz){
		Vector3 result = new Vector3();
		
		result.x = (xyz.x * 3.2404542f) + (xyz.y * -1.5371385f) + (xyz.z * -0.4985314f);
		result.y = (xyz.x * -0.9692660f) + (xyz.y * 1.8760108f) + (xyz.z * 0.0415560f);
		result.z = (xyz.x * 0.0556434f) + (xyz.y * -0.2040259f) + (xyz.z * 1.0572252f);
		
		return result;
	}
	
	const float LAB_E = 216f/24389f;
	const float LAB_K = 24389f/27f;
	
	//D65 white point in LAB space
	const float WHITE_X = 0.950455f;
	const float WHITE_Y = 1f;
	const float WHITE_Z = 1.088753f;
	
	//D50 white point in LAB space - used in photoshop LAB mode
	const float WHITE_X_50 = 0.9642f;
	const float WHITE_Y_50 = 1f;
	const float WHITE_Z_50 = 0.8249f;
	
	public static Vector3 XYZToLAB(Vector3 xyz){
		Vector3 result = new Vector3();
		
		float normalX = xyz.x / WHITE_X_50;
		float normalY = xyz.y / WHITE_Y_50;
		float normalZ = xyz.z / WHITE_Z_50;
		
		float fx;
		float fy;
		float fz;
		
		if(normalX > LAB_E){
			fx = Mathf.Pow(normalX, 0.33333333f);
		}
		else{
			fx = ( LAB_K*normalX + 16f )/ 116f;
		}
		
		if(normalY > LAB_E){
			fy = Mathf.Pow(normalY, 0.33333333f);
		}
		else{
			fy = ( LAB_K*normalY + 16f )/ 116f;
		}
		
		if(normalZ > LAB_E){
			fz = Mathf.Pow(normalZ, 0.33333333f);
		}
		else{
			fz = ( LAB_K*normalZ + 16f )/ 116f;
		}
		/* L */ result.x = 116f*fy - 16;
		/* a */ result.y = 500f*(fx-fy);
		/* b */ result.z = 200f*(fy-fz);
		
		return result;
	}
}
