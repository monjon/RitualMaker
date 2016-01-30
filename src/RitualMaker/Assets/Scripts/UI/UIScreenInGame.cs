using UnityEngine;
using System.Collections;

public class UIScreenInGame : UIScreen {

	// Properties
	//

	public bool isCastingPower = false;

	//TODO :
	//public Power CurrentSelectedPower;

	// Methods
	//

	public void Update(){

		// On a left click
		if(UIScreenManager.Instance != null && UIScreenManager.Instance.CurrentScreen == this && Input.GetKeyDown(KeyCode.Mouse0)){

			// If we are not casting a popup is not open
			if(!this.isCastingPower && UIScreenManager.Instance.CurrentPopUp == null){

				// TODO : if villager, or village selected, show popup info

				// Show Popup powerList
				this.OpenOtherPopUp("PowerList");

			}
			// If we are casting a power
			else if(this.isCastingPower){

				Vector3 castTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				Debug.Log("UIScreenInGame.Update - Cast target : "+castTarget);

				// TODO :
				// this.CurrentlySelectedPower.CastAt(castTarget);

			}


		}

	}




}
