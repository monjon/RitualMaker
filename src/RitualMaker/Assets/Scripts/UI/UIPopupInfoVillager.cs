using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIPopupInfoVillager : UIPopUp {

	// Properties
	//

	[Header("Info Villager")]
	public Text Age;
	public Text Food;
	public Text Health;
	public Text Job;
	public Text Sex;

	[Header("Rites")]
	public RectTransform RitesList;
	public RituelHolder RiteHolderPrefab;

	// Methods
	//

	public void SetVillagerInfo(villageois v){

		if(v != null){
			// Update the texts
			this.Age.text = v.age;
			this.Food.text = v.food.ToString();
			this.Health.text = v.health;
			this.Job.text = v.job;
			this.Sex.text = v.sex;

			// Set the rites holder
			if(this.RitesList != null && this.RiteHolderPrefab != null){

				// Bad way to clean the content
				GameObject tmp = new GameObject();

				// Clean the rite list
				foreach(Transform t in this.RitesList.GetComponentsInChildren<Transform>()){
					if(t != this.RitesList.transform){
						t.SetParent(tmp.transform);
					}
				}

				GameObject.Destroy(tmp);

				// For each keyword in the dico
				foreach(KeyValuePair<string, int> kvp in v.Ritual){

					// Instantiate a rite holder
					RituelHolder rh = GameObject.Instantiate(this.RiteHolderPrefab);
					rh.transform.SetParent(this.RitesList);
					rh.RituelKeyWord.text = kvp.Key;
					rh.RituelLoveFear.text = kvp.Value.ToString();

				}

			}

		}

	}

}
