using UnityEngine;
using System.Collections;

public class GoatController : MonoBehaviour
{

    Vector3 nextDirection;
    public float TimeToWait = 2.0f;
    public float Speed;

	private Animator goatAnimator;

	void Start ()
    {
        nextDirection = new Vector3(transform.position.x + Random.Range(-2.0f, 3.0f), transform.position.y + Random.Range(-2.0f, 3.0f), 0.0f);
		goatAnimator = gameObject.GetComponent<Animator> ();
    }

    void Update()
    {
        if (TimeToWait > 0.0f)
        {
            TimeToWait -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, nextDirection) < 0.3f)
        {
            TimeToWait = 2.0f;
            nextDirection = new Vector3(transform.position.x + Random.Range(-2.0f, 3.0f), transform.position.y + Random.Range(-2.0f, 3.0f), 0.0f);
			goatAnimator.SetBool ("Idle", true);
			goatAnimator.SetBool ("Left", false);
			goatAnimator.SetBool ("Right", false);
        }

    }

    void FixedUpdate()
    {
        if (TimeToWait <= 0.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextDirection, Time.fixedDeltaTime * Speed);
			Vector3 tmpPos = transform.position - nextDirection;
			if(tmpPos.x < 0){
				goatAnimator.SetBool ("Right", true);
				goatAnimator.SetBool ("Idle", false);
				goatAnimator.SetBool ("Left", false);
				gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			}
			if(tmpPos.x > 0){
				goatAnimator.SetBool ("Right", false);
				goatAnimator.SetBool ("Idle", false);
				goatAnimator.SetBool ("Left", true);
				gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			}
        }

        if (transform.position.y > 8.5f)
        {
            Vector3 pos = transform.position;
            pos.y = 8.5f;
        }
        if (transform.position.y < -16.0f)
        {
            Vector3 pos = transform.position;
            pos.y = -16.0f;
        }

        if (transform.position.x < -28.0f)
        {
            Vector3 pos = transform.position;
            pos.x = -28.0f;
        }
        if (transform.position.x > 22.5f)
        {
            Vector3 pos = transform.position;
            pos.x = 22.5f;
        }
    }
}
