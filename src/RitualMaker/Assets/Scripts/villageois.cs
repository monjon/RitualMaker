using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class villageois : MonoBehaviour {
    private Vector3 destination;
    private List<Vector3> villageToWorkplace = new List<Vector3>();

    public float speed;
    public float collectTime = 10f;

    public GameObject Village;

    public int maxFood = 10;
    public int food = 0;
    public string sex;
    public string job;
    public string health;
    public string age;
    private float timer = 0;
    private int i = 0;
    private float variantX = 0.5f;
    private float variantY = 0.5f;
    public float faith = 0.0f;

    private enum playerState
    {
        isWorking,
        isGoingToWork,
        isGoingBackHome,
        isResting,
        isSleeping,
        isGoingToPray,
        isPraying,
        isBackFromRituals,
    }

    private playerState pState;

	// Use this for initialization
	void Start () {
        pState = playerState.isGoingToWork;

        villageToWorkplace.Clear();
        List<Spots> spots = Village.GetComponent<Village>().Spots;

        villageToWorkplace.Add(Village.transform.position);

        foreach (Spots spot in spots)
        {
            if (spot.spotTaken == false && spot.Job == job)
            {
                foreach (GameObject waypoint in spot.waypoints)
                {
                    villageToWorkplace.Add(waypoint.transform.position);
                }
                spot.spotTaken = true;
                break;
            }
        }

        Village.GetComponent<Village>().dwellers.Add(this.gameObject);
	}

    public void WakeUp()
    {
        if (Random.value >= faith)
            pState = playerState.isGoingToWork;
        else
            pState = playerState.isGoingToPray;
    }

    public void EndOfJobCycle()
    {
        pState = playerState.isResting;
    }

    void moove(float coeff)
    {
        if (transform.position != destination)
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * speed * coeff);
    }
	
	// Update is called once per frame
	void Update () {
        switch (pState)
        {
            case playerState.isGoingToWork:
                destination = villageToWorkplace[i];
                if (transform.position == villageToWorkplace[villageToWorkplace.Count -1])
                {
                    pState = playerState.isWorking;
                }
                else if (transform.position == villageToWorkplace[i])
                    ++i;
                break;

            case playerState.isGoingBackHome:
                destination = villageToWorkplace[i];
                if (transform.position == villageToWorkplace[0])
                {
                    if (Random.value >= faith)
                        pState = playerState.isGoingToWork;
                    else
                        pState = playerState.isGoingToPray;
                    Village.GetComponent<Village>().food += food;
                    food = 0;
                }
                else if (transform.position == villageToWorkplace[i])
                    --i;
                break;

            case playerState.isWorking:
                timer = timer + Time.deltaTime;
                destination = new Vector3(villageToWorkplace[villageToWorkplace.Count - 1].x + variantX, villageToWorkplace[villageToWorkplace.Count - 1].y + variantY, 0);
                if (food < maxFood && timer >= collectTime)
                {
                    if (food % 2 == 0)
                        variantX = variantX * -1;
                    else if (food % 3 == 0)
                        variantY = variantY * -1;
                    timer = 0f;
                    food++;
                }
                else if (food == maxFood)
                {
                    timer = 0f;
                    pState = playerState.isGoingBackHome;
                    variantX = 0.5f;
                    variantY = 0.5f;
                }
                break;

            case playerState.isResting:
                destination = villageToWorkplace[i];
                if (transform.position == villageToWorkplace[0])
                {
                        Village.GetComponent<Village>().food += food;
                        food = 0;
                    pState = playerState.isSleeping;
                }
                else if (transform.position == villageToWorkplace[i])
                    --i;
                break;

            case playerState.isSleeping:
                if (transform.position == destination)
                {
                    variantX = Random.value - 0.5f;
                    variantY = Random.value - 0.5f;
                    destination = new Vector3(villageToWorkplace[0].x + variantX, villageToWorkplace[0].y + variantY, 0);
                }
                break;

            case playerState.isGoingToPray:
                destination = new Vector3(10,10,0);
                if (transform.position == destination)
                {
                    pState = playerState.isPraying;
                }
                break;

            case playerState.isPraying:
                break;

            case playerState.isBackFromRituals:
                destination = villageToWorkplace[0];
                if (transform.position == destination)
                {
                    if (Random.value >= faith)
                        pState = playerState.isGoingToWork;
                    else
                        pState = playerState.isGoingToPray;
                }
                break;

            default:
                break;

            }
    }

    void FixedUpdate()
    {
        switch (pState)
        {
            case playerState.isGoingToWork:
                moove(1);
                break;

            case playerState.isGoingBackHome:
                moove(1);
                break;

            case playerState.isResting:
                moove(1);
                break;

            case playerState.isWorking:
                moove(1);
                break;

            case playerState.isSleeping:
                moove(1);
                break;

            case playerState.isGoingToPray:
                moove(1);
                break;

            case playerState.isPraying:
                moove(1);
                break;

            case playerState.isBackFromRituals:
                moove(1);
                break;

            default:
                break;

        }
    }
}
