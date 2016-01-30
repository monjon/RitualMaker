using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillagesManager : MonoBehaviour 
{
    public float TimeUnit = 60.0f;
    public float TimeEndCycle = 12.0f;
    public float EndCycle = 2.0f;

    private float timer = 0.0f;
    private float editor = 1.0f;

    private float actualTime = 0.0f;
    private int numberOfCycles = 0;

    public List<GameObject> Villages;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        float dt = Time.deltaTime * editor;
	
        if (timer >= TimeUnit)
        {
            timer = 0.0f;
            actualTime += 1;
            Debug.Log("Actual Time : " + actualTime + " / Cycle : " + numberOfCycles);
        }

        if (actualTime >= TimeEndCycle &&
            actualTime <= TimeEndCycle + EndCycle)
        {
            // Time to gather to village;
        }
        if (actualTime > TimeEndCycle + EndCycle)
        {
            actualTime = 0.0f;

            ++numberOfCycles;

            foreach (GameObject village in Villages)
            {
                village.GetComponent<Village>().UpdateStocks();
            }

            // Time to go back to work;
        }

        timer += dt;
    }
}
