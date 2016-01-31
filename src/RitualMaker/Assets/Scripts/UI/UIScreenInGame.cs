using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScreenInGame : UIScreen {

	// Properties
	//

	[Header("InGame - SoundOptions")]
	public Image Sound;
	public Image Music;

	public Sprite SoundOn;
	public Sprite SoundOff;
	public Sprite MusicOn;
	public Sprite MusicOff;

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

	public void ChangeSound(){

		if(SoundManager.Instance != null){

			SoundManager.Instance.SoundOn = !SoundManager.Instance.SoundOn;

			if(SoundManager.Instance.SoundOn){
				this.Sound.sprite = this.SoundOn;
			}
			else{
				this.Sound.sprite = this.SoundOff;
			}

		}

	}

	public void ChangeMusic(){

		if(SoundManager.Instance != null){

			SoundManager.Instance.MusicOn = !SoundManager.Instance.MusicOn;

			if(SoundManager.Instance.MusicOn){
				this.Music.sprite = this.MusicOn;
			}
			else{
				this.Music.sprite = this.MusicOff;
			}

		}

	}


}
