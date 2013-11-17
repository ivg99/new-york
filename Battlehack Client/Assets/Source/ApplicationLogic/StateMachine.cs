using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {
	private static StateMachine instance;
	public static StateMachine Instance{
		get{
			return instance;
		}
	}
	
	[SerializeField] ApplicationScreen homeScreen;
	[SerializeField] ApplicationScreen itemScreen;
	[SerializeField] ApplicationScreen loginScreen;
	

	void Awake(){
		instance = this;
	}







}
