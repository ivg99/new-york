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
		GotoHomeScreen(true);
	}

	void GotoItemScreen(bool immediate = false){
		itemScreen.Activate(immediate);
		loginScreen.Deactivate(immediate);
		homeScreen.Deactivate(immediate);
	}

	void GotoHomeScreen(bool immediate = false){
		itemScreen.Deactivate(immediate);
		loginScreen.Deactivate(immediate);
		homeScreen.Activate(immediate);
	}

	void GotoLoginScreen(bool immediate = false){
		itemScreen.Deactivate(immediate);
		loginScreen.Activate(immediate);
		homeScreen.Deactivate(immediate);
	}


}
