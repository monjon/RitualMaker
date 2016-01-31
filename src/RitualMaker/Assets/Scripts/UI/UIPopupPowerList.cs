using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPopupPowerList : UIPopUp {

	// Properties
	// 

	[Header("Power List")]
	public RectTransform PowerWheelBase;

	[Range(0f,0.5f)]
	public float xWheelPos = 0.2f;
	[Range(0f, 0.5f)]
	public float yWheelPos = 0.25f;

	[Header("Power Button")]
	public Button ButtonFire;
	public Button ButtonLightning;
	public Button ButtonBoost;
	public Button ButtonHeal;

	// Methods
	//

	override
	public void OpenPopUp(){
		base.OpenPopUp();

		if(!Application.isPlaying){

			if(this.PowerWheelBase != null){

				Vector3 pos = new Vector3(Mathf.Clamp(Input.mousePosition.x, Camera.main.pixelWidth *this.xWheelPos, Camera.main.pixelWidth*(1f-this.xWheelPos)),
					Mathf.Clamp(Input.mousePosition.y,Camera.main.pixelHeight *this.yWheelPos, Camera.main.pixelHeight*(1f-this.yWheelPos)), 0f);

				this.PowerWheelBase.anchoredPosition3D = pos;

				// Debug.Log("UIPopupPowerList.OpenPopUp - pos "+pos+", InputMousePos "+Input.mousePosition);

			}
		}

	}

	public void ActivatePower(string powerID){

		// activate the power through the game controller
		if(GameController.Instance != null){

			GameController.Instance.ActivatePower(powerID);
			this.ClosePopUp();
		}

	}

	public void Update(){

		if(Application.isPlaying && GameController.Instance != null){

			// Actualize the button status according to cooldowns

			this.ButtonFire.interactable = !GameController.Instance.GetPowerByID("Fire").IsOnCooldown;
			this.ButtonLightning.interactable = !GameController.Instance.GetPowerByID("Lightning").IsOnCooldown;
			this.ButtonBoost.interactable = !GameController.Instance.GetPowerByID("Boost").IsOnCooldown;
			//this.ButtonHeal.interactable = !GameController.Instance.GetPowerByID("Heal").IsOnCooldown;

			this.ButtonFire.GetComponent<Image>().fillAmount = GameController.Instance.GetPowerByID("Fire").CooldownRate;
			this.ButtonLightning.GetComponent<Image>().fillAmount = GameController.Instance.GetPowerByID("Lightning").CooldownRate;
			this.ButtonBoost.GetComponent<Image>().fillAmount = GameController.Instance.GetPowerByID("Boost").CooldownRate;
			//this.ButtonHeal.GetComponent<Image>().fillAmount = GameController.Instance.GetPowerByID("Heal").CooldownRate;

		}

	}

}
