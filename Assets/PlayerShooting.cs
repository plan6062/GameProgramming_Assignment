using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFire()
    {
        GameObject clone = Instantiate(prefab);
        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;
    }
    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Mouse0)) 
    //     {     
    //         //Instantiate(prefab, transform.position, transform.rotation);
    //         GameObject clone = Instantiate(prefab);
    //         clone.transform.position = shootPoint.transform.position;
    //         clone.transform.rotation = shootPoint.transform.rotation;
    //     }
    // }



}