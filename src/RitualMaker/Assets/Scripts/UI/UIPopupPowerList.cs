using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPopupPowerList : UIPopUp {

	// Properties
	// 

	public RectTransform PowerWheelBase;

	// Methods
	//

	override
	public void OpenPopUp(){
		base.OpenPopUp();

		if(this.PowerWheelBase != null){

			Vector3 pos = new Vector3(Mathf.Clamp(Input.mousePosition.x, Camera.main.pixelWidth *0.2f, Camera.main.pixelWidth*0.8f), Mathf.Clamp(Input.mousePosition.y,Camera.main.pixelHeight *0.25f, Camera.main.pixelHeight*0.75f ), 0f);

			this.PowerWheelBase.anchoredPosition3D = pos;

			// Debug.Log("UIPopupPowerList.OpenPopUp - pos "+pos+", InputMousePos "+Input.mousePosition);

		}

	}

}
