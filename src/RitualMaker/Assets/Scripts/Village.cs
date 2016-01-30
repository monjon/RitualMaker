using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Village : MonoBehaviour 
{

    public int food = 50;
    public int minerals = 50;
    public int intel = 50;

    public int chancesGettingSick = 5; // percents

    [HideInInspector]
    public List<GameObject> dwellers;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
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
