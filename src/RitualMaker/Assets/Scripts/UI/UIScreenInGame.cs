using UnityEngine;
using System.Collections;

public class UIScreenInGame : UIScreen {

	// Properties
	//


	//TODO :
	//public Power CurrentSelectedPower;

	// Methods
	//

	public void Update(){

		// On a left click
		if(UIScreenManager.Instance != null && GameController.Instance != null && UIScreenManager.Instance.CurrentScreen == this && Input.GetMouseButtonDown(0)){

			// If we are not casting a popup is not open
			if(!GameController.Instance.IsPowerActive && UIScreenManager.Instance.CurrentPopUp == null && GameController.Instance.IsPowerReady){

				// TODO : if villager, or village selected, show popup info

				// Show Popup powerList
				this.OpenOtherPopUp("PowerList");

			}

		}

	}

}
