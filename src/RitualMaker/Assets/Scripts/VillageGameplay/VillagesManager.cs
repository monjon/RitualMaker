﻿using UnityEngine;
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

    private int numberOfVillages = 1;

    public List<GameObject> Villages;

    public List<GameObject> UnlockableVillages = new List<GameObject>();

	void Start ()
    {
	
	}

    bool goBack = false;

	void Update ()
    {
        float dt = Time.deltaTime * editor;

        if (UnlockableVillages.Count > 0)
        {
            int miner = 0;
            int intel = 0;

            foreach (GameObject village in Villages)
            {
                miner += village.GetComponent<Village>().minerals;
                intel += village.GetComponent<Village>().intel;
            }

            if (miner >= 500 && intel >= 200)
            {
                Villages.Add(UnlockableVillages[0]);
                UnlockableVillages[0].SetActive(true);
                UnlockableVillages.Remove(UnlockableVillages[0]);

                Villages[0].GetComponent<Village>().minerals -= 500;
                Villages[0].GetComponent<Village>().intel -= 200;
            }

            if (miner >= 1000 && intel >= 400)
            {
                Villages.Add(UnlockableVillages[0]);
                UnlockableVillages[0].SetActive(true);
                UnlockableVillages.Remove(UnlockableVillages[0]);
            }
        }

        if (numberOfVillages == 1)
            GameController.Instance.MaxActionPoints = 10;

        if (numberOfVillages == 2)
            GameController.Instance.MaxActionPoints = 15;

        if (numberOfVillages == 3)
            GameController.Instance.MaxActionPoints = 20;

        if (timer >= TimeUnit)
        {
            timer = 0.0f;
            actualTime += 1;
            Debug.Log("Actual Time : " + actualTime + " / Cycle : " + numberOfCycles);
        }

        if (actualTime >= TimeEndCycle &&
            actualTime <= TimeEndCycle + EndCycle &&
            goBack == false)
        {
            goBack = true;
            foreach (GameObject village in Villages)
            {
                village.GetComponent<Village>().GetBackHomeMOFOS();
            }
            // Time to gather to village;
        }
        if (actualTime > TimeEndCycle + EndCycle)
        {
            actualTime = 0.0f;

            ++numberOfCycles;

            foreach (GameObject village in Villages)
            {
                village.GetComponent<Village>().UpdateStocks();
                village.GetComponent<Village>().WakeUpMOFOS();
            }
            goBack = false;
            // Time to go back to work;
        }

        timer += dt;
    }
}
