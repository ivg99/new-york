using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class QuadraticBezierSpline2D : Spline2D{



	public static float GetSplineLength(Vector2[] points, CurveType cType, int samples){

		float totalLength = 0;
		Vector2 previousPosition = EvaluateSplinePosition(0, points, cType);
		

		for(int i=1; i<samples; i++){
			float t = (float)(i)/(samples-1);
			Vector2 position = EvaluateSplinePosition(t, points, cType);

			totalLength += (position - previousPosition).magnitude;
			previousPosition = position;
		}

		return totalLength;
	}


	public static ClosestPoint2D GetClosestPoint(Vector2 position, Vector2[] points, CurveType cType, int samples){
		
		Vector2 closestPosition = EvaluateSplinePosition(0, points, cType);
		float closestDistance = (position -  closestPosition).magnitude;
		float closestPoint = 0;


		for(int i=1; i<samples; i++){
			float t = (float)(i)/(samples-1);
			Vector2 pos = EvaluateSplinePosition(t, points, cType);
			float dist = (position -  pos).magnitude;

			if(dist < closestDistance){
				closestDistance = dist;
				closestPoint = t;
				closestPosition = pos;
			}

		}

		return new ClosestPoint2D(closestPoint, closestPosition, closestDistance);
	}

	//should use newtons method instead, this is just brute force
	public static ClosestPoint2D GetApproximateClosestPoint(Vector2 position, Vector2[] points, CurveType cType, int samples){
		
		Vector2 closestPosition = EvaluateSplinePosition(0, points, cType);
		float closestDistance = (position -  closestPosition).magnitude;
		float closestPoint = 0;


		for(int i=1; i<samples; i++){
			float t = (float)(i)/(samples-1);
			Vector2 pos = EvaluateSplinePosition(t, points, cType);
			float dist = (position -  pos).magnitude;

			if(dist < closestDistance){
				closestDistance = dist;
				closestPoint = t;
				closestPosition = pos;
			}

		}

		return new ClosestPoint2D(closestPoint, closestPosition, closestDistance);
	}
	
	public static Vector2 EvaluateSplinePosition(float t, Vector2[] points, CurveType cType){
		t = Mathf.Clamp01(t);	

		int segments = (points.Length -2);

		if(cType == CurveType.Closed){
			segments += 2;
		}

		float totalT = (t * segments);
		int segment = Mathf.Clamp((int)(t * segments),0,segments-1);
		float segmentT = totalT - segment;
		int initialPoint = segment;
		
		return EvaluatePosition(segmentT, initialPoint, points, cType);
	}

	public static Vector2 EvaluateSplineTangent(float t, Vector2[] points, CurveType cType){
		t = Mathf.Clamp01(t);	

		int segments = (points.Length - 2);

		if(cType == CurveType.Closed){
			segments += 2;
		}

		float totalT = (t * segments);
		int segment = Mathf.Clamp((int)(t * segments),0,segments-1);
		float segmentT = totalT - segment;
		int initialPoint = segment ;
		
		return EvaluateTangent(segmentT, initialPoint, points, cType);
	}
	


	private static Vector2 EvaluatePosition(float t, int initialPoint, Vector2[] points, CurveType cType){
		
		int pointCount = points.Length;

		int t0 = initialPoint;
		int t1 = initialPoint + 1;
		int t2 = initialPoint + 2;


		if(cType == CurveType.Closed){

			t0 = t0 % pointCount;
			t1 = t1 % pointCount;
			t2 = t2 % pointCount;	
		}


		Vector2 firstPoint;
		Vector2 finalPoint;
		if(cType == CurveType.Open && initialPoint == 0){
			firstPoint = points[t0];
		}
		else{
			firstPoint = 0.5f*(points[t0] + points[t1]);
		}

		if(cType == CurveType.Open && initialPoint == points.Length-3){
			finalPoint = points[t2];
		}
		else{
			finalPoint = 0.5f*(points[t1] + points[t2]);
		}
		
		float invT = 1-t;

		return 	(invT*invT)	*	firstPoint + //First point
				(invT*2*t)	*	points[t1] + //first tangent
				(t*t)	*	finalPoint; //second point

		
	}


	private static Vector2 EvaluateTangent(float t, int initialPoint, Vector2[] points, CurveType cType){
		
		int pointCount = points.Length;

		int t0 = initialPoint;
		int t1 = initialPoint + 1;
		int t2 = initialPoint + 2;


		if(cType == CurveType.Closed){

			t0 = t0 % pointCount;
			t1 = t1 % pointCount;
			t2 = t2 % pointCount;	
		}

		Vector2 firstPoint;
		Vector2 finalPoint;
		if(cType == CurveType.Open && initialPoint == 0){
			firstPoint = points[t0];
		}
		else{
			firstPoint = 0.5f*(points[t0] + points[t1]);
		}

		if(cType == CurveType.Open && initialPoint == points.Length-3){
			finalPoint = points[t2];
		}
		else{
			finalPoint = 0.5f*(points[t1] + points[t2]);
		}


		return 	(2 * t - 2)	*	firstPoint + //First point
				(2 - 4 * t)	*	points[t1] + //first tangent
				(2 * t)	*	finalPoint; //second point

		
	}
	
	
	public static void DrawGizmos(Vector2[] points, CurveType cType, Transform t){
		


		if(points != null && (points.Length-1) > 0){

			int segments = points.Length-1;


			for(int i=0; i<points.Length; i++){
				Gizmos.DrawSphere(t.TransformPoint(points[i]), 0.1f);
			}
			
			int lineCount = 20*segments;
			
			Vector2 lastPosition = EvaluateSplinePosition(0, points, cType);
			for(int i=1; i<lineCount+1; i++){
				float percent = (float)i / (float)lineCount;
				Vector2 newPosition = EvaluateSplinePosition(percent, points, cType);
				Gizmos.DrawLine(t.TransformPoint(lastPosition), t.TransformPoint(newPosition));
				lastPosition = newPosition;
			}
		}
	}

	

	

}

public struct ClosestPoint2D{

	public ClosestPoint2D(float t, Vector2 position, float distance){
		this.t = t;
		this.position = position;
		this.distance = distance;
	}

	public float t;
	public Vector2 position;
	public float distance;
}
// public struct QuadraticSolutions{
// 	public int solutionCount;
// 	public float solutionOne;
// 	public float solutionTwo;
// 	public float solutionThree;
// }
