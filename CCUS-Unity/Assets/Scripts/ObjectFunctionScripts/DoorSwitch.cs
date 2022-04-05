using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{

    public bool DoorOpen;
    private bool Opening;

    private float OpenedTime;
    

    [SerializeField]
    private float MovementSpeed;

    private GameObject LWall;
    private GameObject RWall;
    private float countOpenTime;

    // Start is called before the first frame update
    void Start()
    {
        DoorOpen = false;
        Opening = true;

        LWall = transform.GetChild(0).gameObject;
        RWall = transform.GetChild(1).gameObject;

        MovementSpeed = 2.25f;
        countOpenTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorOpen & Opening)
        {
            countOpenTime += Time.deltaTime;

            LWall.transform.position -= LWall.transform.right * Time.deltaTime * MovementSpeed;
            RWall.transform.position += RWall.transform.right * Time.deltaTime * MovementSpeed;

            if(countOpenTime > 2f)
            {
                Opening = false;
            }
        }
    }
}
