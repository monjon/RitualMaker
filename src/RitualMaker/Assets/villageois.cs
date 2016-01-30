using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class villageois : MonoBehaviour {
    private Vector3 destination;
    public List<Vector3> villageToWorkplace;

    public float speed;
    public float collectTime = 10f;

    public int maxFood = 10;
    public int food = 0;
    public string sex;
    public string job;
    public string health;
    public string age;
    private float timer = 0;
    private int i = 0;

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
	}

    void moove()
    {
        if (transform.position != destination)
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
    }
	
	// Update is called once per frame
	void Update () {
        switch (pState)
        {
            case playerState.isGoingToWork:
                destination = villageToWorkplace[i];
                moove();
                if (transform.position == villageToWorkplace[villageToWorkplace.Count -1])
                {
                    pState = playerState.isWorking;
                }
                else if (transform.position == villageToWorkplace[i])
                    ++i;
                break;

            case playerState.isGoingBackHome:
                destination = villageToWorkplace[i];
                moove();
                if (transform.position == villageToWorkplace[0])
                {
                    pState = playerState.isGoingToWork;
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
                    Debug.Log(food);
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

    }
}
