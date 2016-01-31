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

            RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.right);

            List<string> words = new List<string>();

            foreach (RaycastHit2D hit in hits)
            {
                GameObject touched = hit.collider.gameObject;

                //if (touched.CompareTag("Villager"))
                //{
                //    //Add the ritual to the villager

                //    Ritual r = touched.AddComponent<Ritual>();
                //    // Init the Ritual data to the Villager

                //}

                //// If the touched villager has a keywords component on him (he should !)
                //if(touched.GetComponent<KeyWords>() != null){
                if (touched.GetComponent<KeyWords>())
                    words.AddRange(touched.GetComponent<KeyWords>().KeyWordsList);
                //}
            }

            //for (int i = 0; i < words.Count; i++)
            //{
            //    string temp = words[i];
            //    int randomIndex = Random.Range(i, words.Count);
            //    words[i] = words[randomIndex];
            //    words[randomIndex] = temp;
            //}

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

            //for (int i = 0; i < 3 && i < words.Count - 1; ++i)
            //{
            //    selectedConditions.Add(words[i]);
            //}

            foreach (RaycastHit2D hit in hits)
            {
                GameObject touched = hit.collider.gameObject;

                if (touched.CompareTag("Villager"))
                {
                    foreach (string condition in selectedConditions)
                    {
                        //Debug.Log("Condition : " + condition);
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
            /*
            for (int i = 0; i < 3; ++i)
            {
                newRitual.GetComponent<Ritual>().keywords.Add(words[i]);
            }
            newRitual.GetComponent<Ritual>().godAction = powerName;
            newRitual.GetComponent<Ritual>().faith = 42;// Change to the action type;
            */

        }
        else
        {
            Debug.Log("RitualManager.CreateRitual - No Ritual Prefab set");
        }
    }
}
