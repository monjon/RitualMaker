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
	public Text FaithCount;


	// Methods
	//

	public void SetVillageInfo(Village v){

		if(v != null){
			// Update the texts
			this.FoodCount.text = v.food.ToString();
			this.MineralsCount.text = v.minerals.ToString();
			this.IntelCount.text = v.intel.ToString();
			this.PopulationCount.text = v.dwellers.Count.ToString();
			this.FaithCount.text = v.faith.ToString();

		}

	}

}
