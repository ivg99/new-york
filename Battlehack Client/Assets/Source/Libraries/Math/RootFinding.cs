using UnityEngine;
using System.Collections;

public static class RootFinding  {



	//http://mathworld.wolfram.com/CubicFormula.html
	public static CubicRoots FindCubicRoots(float a, float b, float c, float d){

	 	//divide through by a, to create a monic polynomial
	 	b = b / a;
	 	c = c / a;
	 	d = d / a;

	 	//now we substitute x = y - b/3a, to remove the squared term, and solve through, creating x^3 + px + q

	 	float p = c - b*b/3f;
	 	float q = b*c/3f - d - 2f*b*b*b/27f;

	 	float Q = p/3f;
	 	float R = q/2f;



	 	float discriminant = Q*Q*Q + R*R;

	 	if(discriminant > 0){
	 		float sqrtD = Mathf.Sqrt(discriminant);

	 		float s = R + sqrtD;
	 		float t = R - sqrtD;

	 		float signS = Mathf.Sign(s);
	 		float signT = Mathf.Sign(t);

	 		float S = signS*Mathf.Pow(signS*s, 1/3f);
	 		float T = signT*Mathf.Pow(signT*t, 1/3f);

	 		float result = -b*(1f/3f) + S + T;
	 		// float result2 = -b*(1f/3f) - 0.5f* (S + T) + 0.5f*Mathf.Sqrt(3)*(S-T);
	 		// float result3 = -b*(1f/3f) - 0.5f* (S + T) - 0.5f*Mathf.Sqrt(3)*(S-T);
	 		return new CubicRoots(1, result, 0,0);
	 		// Debug.Log(result);//+ " : " + result2 + " : " + result3);
	 	}
	 	else if(discriminant < 0){
	 		float theta = Mathf.Acos( R / Mathf.Sqrt(-Q*Q*Q) );
	 		float resultOne = 2f* Mathf.Sqrt(-Q)*Mathf.Cos(theta/3f) -b/3f;
	 		float resultTwo = 2f* Mathf.Sqrt(-Q)*Mathf.Cos( (theta + 2f*Mathf.PI)/3f) -b/3f;
	 		float resultThree = 2f* Mathf.Sqrt(-Q)*Mathf.Cos((theta + 4f*Mathf.PI)/3f) -b/3f;
	 		return new CubicRoots(3, resultOne,resultTwo,resultThree);
	 		// Debug.Log(resultOne + " : " + resultTwo + " : " + resultThree);
	 	}
	 	else{

	 		float signR = Mathf.Sign(R) ;


	 		float S = signR*Mathf.Pow(signR*R, 1/3f);


	 		float resultOne = -b*(1f/3f) + 2f*S;
	 		float resultTwo = -b*(1f/3f) - (S);
	 		return new CubicRoots(2, resultOne, resultTwo,0);

	 	}
		


	 }

	//quadratic formula, rearranged to prevent catestrophic cancellation of floats
	public static void QuadraticRoots(float a, float b, float c){

		float signb = (b > 0) ? 1 : 0;
		float discriminant = b*b - 4f*a*c;  //this can still cause cancellation - may want to improve precision here

		//we may want to use epsilon here instead...
		if(discriminant < 0){
			//there are no real roots.
		}
		else if(discriminant > 0){
			//two real roots
		}
		else{
			// Debug.Log("one root");
			//only one root
		}

		//check if p is greater than 0

		float q = (-b - signb * Mathf.Sqrt(discriminant) );

		float root1 = 0.5f*q / a;
		float root2 = 2f*c / q;

		Debug.Log(root1 + " : " + root2);
		//return the root count, and roots
	}
}

public struct CubicRoots{

	public CubicRoots(int rootCount, float rootOne, float rootTwo, float rootThree){
		this.rootCount = rootCount;
		this.rootOne = rootOne;
		this.rootTwo = rootTwo;
		this.rootThree = rootThree;
	}

	public int rootCount;
	public float rootOne;
	public float rootTwo;
	public float rootThree;
}
