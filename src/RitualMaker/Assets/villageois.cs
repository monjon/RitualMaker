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

    [HideInInspector]
    public Dictionary<string, int> Ritual = new Dictionary<string,int>();

    private enum playerState
    {
        isWorking,
        isGoingToWork,
        isGoingBackHome,
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

    public void Fear()
    {
        Debug.Log("HUUUH");
    }

    public void WakeUp()
    {

    }

    public void EndOfJobCycle()
    {

    }

    void moove()
    {
        if (transform.position != destination)
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * speed);
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
                    pState = playerState.isGoingToWork;
                    Village.GetComponent<Village>().food += food;
                    food = 0;
                }
                else if (transform.position == villageToWorkplace[i])
                    --i;
                break;

            case playerState.isWorking:
                timer = timer + Time.deltaTime;
                if (food < maxFood && timer >= collectTime)
                {
                    timer = 0f;
                    food++;
                }
                else if (food == maxFood)
                {
                    timer = 0f;
                    pState = playerState.isGoingBackHome;
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
                moove();
                break;

            case playerState.isGoingBackHome:
                moove();
                break;

            default:
                break;

        }
    }
}
