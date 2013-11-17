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
	[SerializeField] ItemScreen itemScreen;
	[SerializeField] ApplicationScreen loginScreen;

	void Awake(){
		instance = this;
		
	}

	void Start(){
		GotoHomeScreen(true);
	}

	public void LoadItem(int itemID){
		ItemManagement.Instance.GetItem(itemID);
		itemScreen.SetItem(itemID);
		GotoItemScreen();
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
