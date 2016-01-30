using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPopupInfoVillage : UIPopUp {

	// Properties
	//

	[Header("Info Village")]
	public Text FoodCount;
	public Text MineralsCount;
	public Text IntelCount;
	public Text PopulationCount;



	// Methods
	//

	public void SetVillageInfo(Village v){

		if(v != null){
			// Update the texts
			this.FoodCount.text = v.food.ToString();
			this.MineralsCount.text = v.minerals.ToString();
			this.IntelCount.text = v.intel.ToString();

		}

	}

}
