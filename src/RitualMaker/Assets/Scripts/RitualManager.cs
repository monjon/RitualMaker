using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualManager : MonoBehaviour
{


	// Singleton
	//

	static private RitualManager instance;
	static public RitualManager Instance{
		get {return RitualManager.instance;}
	}

	public void Awake(){
		// Singleton logic
		if(RitualManager.instance == null){
			RitualManager.instance = this;
			GameObject.DontDestroyOnLoad(this.gameObject);
		}

	}

	// Properties
	//


    public GameObject RitualPrefab;

	// Methods
	//

    public void CreateRitual(Vector2 position, float range, string powerName)
    {
		if(this.RitualPrefab != null){
	        //GameObject newRitual = (GameObject)GameObject.Instantiate(RitualPrefab, Vector3.zero, Quaternion.identity);

	        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.right);

	        List<string> words = new List<string>();

	        foreach (RaycastHit2D hit in hits)
	        {
	            GameObject touched = hit.collider.gameObject;

	            if (touched.CompareTag("Villager"))
	            {
	                //Add the ritual to the villager

					Ritual r = touched.AddComponent<Ritual>();
					// Init the Ritual data to the Villager

	            }

				// If the touched villager has a keywords component on him (he should !)
				if(touched.GetComponent<KeyWords>() != null){
	            	words.AddRange(touched.GetComponent<KeyWords>().KeyWordsList);
				}
	        }

	        for (int i = 0; i < words.Count; i++)
	        {
	            string temp = words[i];
	            int randomIndex = Random.Range(i, words.Count);
	            words[i] = words[randomIndex];
	            words[randomIndex] = temp;
	        }
			/*
	        for (int i = 0; i < 3; ++i)
	        {
	            newRitual.GetComponent<Ritual>().keywords.Add(words[i]);
	        }
	        newRitual.GetComponent<Ritual>().godAction = powerName;
	        newRitual.GetComponent<Ritual>().faith = 42;// Change to the action type;
	        */

		}
		else{
			Debug.Log("RitualManager.CreateRitual - No Ritual Prefab set");
		}
    }
}
