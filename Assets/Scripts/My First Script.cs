using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float Atkdmg;
    public float Defdmg;
    public float height;
    public float width;

    // Start is called before the first frame update
    void Start()
    {
        print("test Start");
    }

    // Update is called once per frame
    void Update()
    {
        print(speed);
        print(Atkdmg);
        print(Defdmg);
        print(height);
        print(width);
    }
}
