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
	[SerializeField] CheckoutScreen checkoutScreen;
	void Awake(){
		instance = this;
		
	}

	void Start(){
		GotoHomeScreen(1, true);
	}

	public void LoadItem(int itemID){
		ItemManagement.Instance.GetItem(itemID);
		itemScreen.SetItem(itemID);
		GotoItemScreen(1);
	}

	public void GotoItemScreen(int direction, bool immediate = false){
		itemScreen.Activate(immediate, direction);
		loginScreen.Deactivate(immediate,-direction);
		homeScreen.Deactivate(immediate,-direction);
		checkoutScreen.Deactivate(immediate,-direction);
	}

	public void GotoHomeScreen(int direction,bool immediate = false){
		itemScreen.Deactivate(immediate,-direction);
		loginScreen.Deactivate(immediate,-direction);
		homeScreen.Activate(immediate,direction);
		checkoutScreen.Deactivate(immediate,-direction);
	}

	void GotoLoginScreen(int direction,bool immediate = false){
		itemScreen.Deactivate(immediate,-direction);
		loginScreen.Activate(immediate, direction);
		homeScreen.Deactivate(immediate,-direction);
		checkoutScreen.Deactivate(immediate,-direction);
	}

	public void Checkout(int direction,bool immediate = false){
		itemScreen.Deactivate(immediate,-direction);
		loginScreen.Deactivate(immediate,-direction);
		homeScreen.Deactivate(immediate,-direction);
		checkoutScreen.Activate(immediate,direction);
	}


}
