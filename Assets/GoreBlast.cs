using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreBlast : MonoBehaviour
{
    public GameObject[] gores;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _gore = Instantiate(gores[Random.Range(0,gores.Length)],transform);
            // _gore.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
