using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CatmullRomSpline2D : Spline2D{



	//[HideInInspector]
	//[SerializeField] private Vector2[] points;
	//[SerializeField] private int segments;

	
//	public void SetSegments(int segmentCount){
//		segments = segmentCount;
//		
//		points = new Vector2[3*segments + 1];	
//	}
	

	public static float GetSplineLength(Vector2[] points, CurveType cType, int samples){

		float totalLength = 0;
		Vector2 previousPosition = EvaluateSplinePosition(0, points, cType);
		

		for(int i=1; i<samples; i++){
			float t = (float)(i)/(samples-1);
			Vector2 position = CatmullRomSpline2D.EvaluateSplinePosition(t, points, cType);

			totalLength += (position - previousPosition).magnitude;
			previousPosition = position;
		}

		return totalLength;
	}
	

	public static Vector2 EvaluateSplinePosition(float t, Vector2[] points, CurveType cType){
		t = Mathf.Clamp01(t);	

		int segments = points.Length -1;

		if(cType == CurveType.Closed){
			segments += 1;
		}

		float totalT = (t * segments);
		int segment = Mathf.Clamp((int)(t * segments),0,segments-1);
		float segmentT = totalT - segment;
		int initialPoint = segment;
		
		return EvaluatePosition(segmentT, initialPoint, points, cType);
	}

	public static Vector2 EvaluateSplineTangent(float t, Vector2[] points, CurveType cType){
		t = Mathf.Clamp01(t);	

		int segments = points.Length -1;

		if(cType == CurveType.Closed){
			segments += 1;
		}

		float totalT = (t * segments);
		int segment = Mathf.Clamp((int)(t * segments),0,segments-1);
		float segmentT = totalT - segment;
		int initialPoint = segment;
		
		return EvaluateTangent(segmentT, initialPoint, points, cType);
	}

	// public static Vector2 EvaluateSecondDerivative(float t, Vector2[] points, CurveType cType){
	// 	t = Mathf.Clamp01(t);	

	// 	int segments = points.Length -1;

	// 	if(cType == CurveType.Closed){
	// 		segments += 1;
	// 	}

	// 	float totalT = (t * segments);
	// 	int segment = Mathf.Clamp((int)(t * segments),0,segments-1);
	// 	float segmentT = totalT - segment;
	// 	int initialPoint = segment;
		
	// 	return SecondDerivative(segmentT, initialPoint, points, cType);
	// }

	private static Vector2 EvaluatePosition(float t, int initialPoint, Vector2[] points, CurveType cType){
		
		int pointCount = points.Length;

		int pMinusOne = initialPoint-1;
		int pPlusOne = initialPoint+1;
		int pPlusTwo = initialPoint+2;

		if(cType == CurveType.Closed){
			pMinusOne = (initialPoint-1 + pointCount) % pointCount;
			pPlusOne = (initialPoint+1) % pointCount;
			pPlusTwo = (initialPoint+2) % pointCount;
		}
		else{
			if(pMinusOne < 0){
				pMinusOne = 0;
			}
			if(!(pPlusTwo < pointCount) ){
				pPlusTwo = pPlusOne;
			}
		}

		Vector2 previousPoint = points[pMinusOne];
		Vector2 tangent1 = 0.5f*(points[pPlusOne] - previousPoint);  
		Vector2 nextPoint = points[pPlusTwo];
		Vector2 tangent2 = 0.5f*(nextPoint - points[initialPoint]);  
		
		float tSquared = t*t;
		float tCubed = t*tSquared;

		return 	(2*tCubed - 3*tSquared + 1)	*	points[initialPoint] + //First point
				(tCubed - 2*tSquared + t)	*	tangent1 + //first tangent
				(-2*tCubed + 3*tSquared)	*	points[pPlusOne] + //second point
				(tCubed - tSquared)			*	tangent2; //second tangent
		
	}

	private static Vector2 EvaluateTangent(float t, int initialPoint, Vector2[] points, CurveType cType){
		
		int pointCount = points.Length;

		int pMinusOne = initialPoint-1;
		int pPlusOne = initialPoint+1;
		int pPlusTwo = initialPoint+2;

		if(cType == CurveType.Closed){
			pMinusOne = (initialPoint-1 + pointCount) % pointCount;
			pPlusOne = (initialPoint+1) % pointCount;
			pPlusTwo = (initialPoint+2) % pointCount;
		}
		else{
			if(pMinusOne < 0){
				pMinusOne = 0;
			}
			if(!(pPlusTwo < pointCount) ){
				pPlusTwo = pPlusOne;
			}
		}

		Vector2 previousPoint = points[pMinusOne];
		Vector2 tangent1 = 0.5f*(points[pPlusOne] - previousPoint);  
		Vector2 nextPoint = points[pPlusTwo];
		Vector2 tangent2 = 0.5f*(nextPoint - points[initialPoint]);  
		
		float tSquared = t*t;
//		float tCubed = t*tSquared;

		return 	(6*tSquared - 6*t)		*	points[initialPoint] + //First point
				(3*tSquared - 4*t + 1)	*	tangent1 + //first tangent
				(-6*tSquared + 6*t)		*	points[pPlusOne] + //second point
				(3*tSquared - 2*t)		*	tangent2; //second tangent
		
	}

	// private static Vector2 SecondDerivative(float t, int initialPoint, Vector2[] points, CurveType cType){
		
	// 	int pointCount = points.Length;

	// 	int pMinusOne = initialPoint-1;
	// 	int pPlusOne = initialPoint+1;
	// 	int pPlusTwo = initialPoint+2;

	// 	if(cType == CurveType.Closed){
	// 		pMinusOne = (initialPoint-1 + pointCount) % pointCount;
	// 		pPlusOne = (initialPoint+1) % pointCount;
	// 		pPlusTwo = (initialPoint+2) % pointCount;
	// 	}
	// 	else{
	// 		if(pMinusOne < 0){
	// 			pMinusOne = 0;
	// 		}
	// 		if(!(pPlusTwo < pointCount) ){
	// 			pPlusTwo = pPlusOne;
	// 		}
	// 	}

	// 	Vector2 previousPoint = points[pMinusOne];
	// 	Vector2 tangent1 = 0.5f*(points[pPlusOne] - previousPoint);  
	// 	Vector2 nextPoint = points[pPlusTwo];
	// 	Vector2 tangent2 = 0.5f*(nextPoint - points[initialPoint]);  
		

	// 	return 	(12*t - 6)		*	points[initialPoint] + //First point
	// 			(6*t - 4)		*	tangent1 + //first tangent
	// 			(-6*t + 6)		*	points[pPlusOne] + //second point
	// 			(3*t - 2)		*	tangent2; //second tangent
		
	// }

//	
//	
//	private Vector2 SecondDerivative(float d, int initialPoint){
//			
//		float invD = 1-d;
//		
//		return 6*invD*points[initialPoint] + 3*(-4+6*d)*points[initialPoint+1] + 3*(2-6*d)*points[initialPoint+2] + 6*d*points[initialPoint+3];
//		
//		
//	}
	
	
	public static void DrawGizmos(Vector2[] points, CurveType cType){
		


		if(points != null && (points.Length-1) > 0){

			int segments = points.Length-1;


			for(int i=0; i<points.Length; i++){
				Gizmos.DrawSphere(points[i], 0.125f);
			}
			
			int lineCount = 20*segments;
			
			Vector2 lastPosition = EvaluateSplinePosition(0, points, cType);
			for(int i=1; i<lineCount+1; i++){
				float percent = (float)i / (float)lineCount;
				Vector2 newPosition = EvaluateSplinePosition(percent, points, cType);
				Gizmos.DrawLine(lastPosition, newPosition);
				lastPosition = newPosition;
			}
		}
	}

	

	

}
