using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{

    //angle threshold to trigger if we reached limit
    public float angleBetweenThreshold = 1f;
    //State of the hinge joint : either reached min or max or none if in between
    public HingeJointState hingeJointState = HingeJointState.None;
    // door this lever is connected to
    public GameObject ConnectedDoor;

    //Event called on min reached
    public UnityEvent OnMinLimitReached;
    //Event called on max reached
    public UnityEvent OnMaxLimitReached;

    public enum HingeJointState { Min, Max, None }
    private HingeJoint hinge;
    

    // Start is called before the first frame update
    void Start()
    {
        hinge = this.transform.GetChild(0).GetComponent<HingeJoint>();
    }

    private void FixedUpdate()
    {
        float angleWithMinLimit = Mathf.Abs(hinge.angle - hinge.limits.min);
        float angleWithMaxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);

        //Reached Min
        if (angleWithMinLimit < angleBetweenThreshold)
        {
            //if (hingeJointState != HingeJointState.Min)
            //    OnMinLimitReached.Invoke();
            ConnectedDoor.GetComponent<DoorSwitch>().DoorOpen = true;
            //hingeJointState = HingeJointState.Min;
        }
        //Reached Max
        else if (angleWithMaxLimit < angleBetweenThreshold)
        {
            //if (hingeJointState != HingeJointState.Max)
            //    OnMaxLimitReached.Invoke();
            //ConnectedDoor.GetComponent<DoorSwitch>().DoorOpen = true;

            //hingeJointState = HingeJointState.Max;
        }
        //No Limit reached
        else
        {
            hingeJointState = HingeJointState.None;
        }
    }
}
