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
                }
            }
        }
    }
}
