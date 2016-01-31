using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualManager : MonoBehaviour
{


    // Singleton
    //

    static private RitualManager instance;
    static public RitualManager Instance
    {
        get { return RitualManager.instance; }
    }

    public void Awake()
    {
        // Singleton logic
        if (RitualManager.instance == null)
        {
            RitualManager.instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

    }

    // Properties
    //


    public GameObject RitualPrefab;

    // Methods
    //

    public void CreateRitual(Vector2 position, float range, string powerName, int power)
    {
        if (this.RitualPrefab != null)
        {
            //GameObject newRitual = (GameObject)GameObject.Instantiate(RitualPrefab, Vector3.zero, Quaternion.identity);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.zero);

            List<string> words = new List<string>();

            foreach (RaycastHit2D hit in hits)
            {
                GameObject touched = hit.collider.gameObject;

                //// If the touched villager has a keywords component on him (he should !)
                if (touched.GetComponent<KeyWords>())
                    words.AddRange(touched.GetComponent<KeyWords>().KeyWordsList);
            }


            List<string> selectedConditions = new List<string>();

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

            foreach (RaycastHit2D hit in hits)
            {
                GameObject touched = hit.collider.gameObject;

                if (touched.CompareTag("Villager"))
                {
                    foreach (string condition in selectedConditions)
                    {
                        if (touched.GetComponent<villageois>().Ritual.ContainsKey(condition))
                        {
                            touched.GetComponent<villageois>().Ritual[condition] += power;
                            if (touched.GetComponent<villageois>().Ritual[condition] > 3)
                                touched.GetComponent<villageois>().Ritual[condition] = 3;
                            if (touched.GetComponent<villageois>().Ritual[condition] < -3)
                                touched.GetComponent<villageois>().Ritual[condition] = -3;
                        }
                        else
                        {
                            touched.GetComponent<villageois>().Ritual[condition] = power;
                        }
                    }
                    if (power < 0)
                        touched.GetComponent<villageois>().Fear();
                }
            }

            --GameController.Instance.ActionPoints;
            ++GameController.Instance.TotalActionPointsUsed;
        }
        else
        {
            Debug.Log("RitualManager.CreateRitual - No Ritual Prefab set");
        }
    }
}
