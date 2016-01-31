using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spots
{
    public string Name;
    public List<GameObject> waypoints;
    public string Job;
    public bool spotTaken;
}

public class Village : MonoBehaviour 
{

    public int food = 50;
    public int minerals = 50;
    public int intel = 50;

    public int chancesGettingSick = 5; // percents

    [SerializeField]
    public List<Spots> Spots = new List<Spots>();

    [HideInInspector]
    public List<GameObject> dwellers;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    public void WakeUpMOFOS()
    {
        foreach (GameObject dweller in dwellers)
        {
            dweller.GetComponent<villageois>().WakeUp();
        }
    }

    public void GetBackHomeMOFOS()
    {
        foreach (GameObject dweller in dwellers)
        {
            dweller.GetComponent<villageois>().EndOfJobCycle();
        }
    }

    public void UpdateStocks()
    {
        food -= dwellers.Count;

        if (food < 0)
        {
            food = 0;

            foreach (GameObject dweller in dwellers)
            {
                int r = Random.Range(0, 100);

                if (r < chancesGettingSick)
                {
                    // the dweller gets sick.
                    dweller.GetComponent<villageois>().GetsSick();
                }
            }
        }

        // Share rituals;
        List<GameObject> dwellersWithRituals = new List<GameObject>();
        foreach (GameObject dweller in dwellers)
        {
            if (dweller.GetComponent<villageois>().Ritual.Keys.Count > 0)
            {
                dwellersWithRituals.Add(dweller);
            }
        }
        Debug.Log(dwellersWithRituals.Count);
        if (dwellersWithRituals.Count <= 0)
            return;
        GameObject selectedDweller = dwellersWithRituals[Random.Range(0, dwellersWithRituals.Count)];

        List<string> selectedConditions = new List<string>();
        List<string> words = new List<string>();
        words.AddRange(selectedDweller.GetComponent<villageois>().Ritual.Keys);

        if (words.Count < 3)
        {
            foreach (string w in words)
                selectedConditions.Add(w);
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                int r = Random.Range(0, words.Count);
                selectedConditions.Add(words[r]);
                words.Remove(words[r]);
            }
        }

        if (dwellers.Count <= 1)
            return;

        GameObject otherDweller = dwellers[Random.Range(0, dwellers.Count)];
        while (otherDweller == selectedDweller)
        {
            otherDweller = dwellers[Random.Range(0, dwellers.Count)];
        }

        Debug.Log(selectedDweller + " / " + otherDweller);

        foreach (string key in selectedConditions)
        {
            Debug.Log("Condition : " + key);
            Debug.Log("Value : " + selectedDweller.GetComponent<villageois>().Ritual[key]);
            if (otherDweller.GetComponent<villageois>().Ritual.ContainsKey(key) == false)
            {
                otherDweller.GetComponent<villageois>().Ritual[key] = 0;
            }
            Debug.Log("Value : " + otherDweller.GetComponent<villageois>().Ritual[key]);

            int lhs = 10 + Mathf.Abs(selectedDweller.GetComponent<villageois>().Ritual[key]) * 10;
            int rhs = 10 + Mathf.Abs(otherDweller.GetComponent<villageois>().Ritual[key]) * 10;

            int rlhs = Random.Range(0, lhs);
            int rrhs = Random.Range(0, rhs);

            if (rlhs > rrhs)
            {
                Debug.Log("selectedDweller won the argument;");
                int a = 1 * (int)Mathf.Sign(selectedDweller.GetComponent<villageois>().Ritual[key]);
                otherDweller.GetComponent<villageois>().Ritual[key] += a;
            }
            else if (rlhs < rrhs)
            {
                Debug.Log("otherDweller won the argument;");
                int a = 1 * (int)Mathf.Sign(otherDweller.GetComponent<villageois>().Ritual[key]);
                selectedDweller.GetComponent<villageois>().Ritual[key] += a;
            }
            else
            {
                Debug.Log("StaleMate");
                //Stalemate;
                // NOthing happens
            }
        }
    }

	public void OnMouseDown(){

		// If there is no power activated, and there is no popup shown
		if(GameController.Instance != null && !GameController.Instance.IsPowerActive && UIScreenManager.Instance != null && UIScreenManager.Instance.CurrentPopUp == null){
			// Show the info popup
			UIScreenManager.Instance.OpenPopUp("InfoVillage");

			// Setup the popup with info from this village
			((UIPopupInfoVillage)UIScreenManager.Instance.CurrentPopUp).SetVillageInfo(this);

		}

	}

}
