using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControlCamera : MonoBehaviour
{
    public Hand hand;
    private bool flag = true;
    private bool setflag = true;
    Vector3 tmp;
    // Use this for initialization
    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hand.grabGripAction.GetStateDown(hand.handType))
        {
            Debug.Log("Trigger");
            flag = !flag;
        }

        if (flag)
        {
            if (!setflag)
            {
                transform.position = tmp;
                setflag = !setflag;
            }


        }
        else
        {
            if (setflag)
            {
                tmp = transform.position;
                setflag = !setflag;
            }
            else
            {
                transform.position.Set(10, 20, 10);
            }

        }

    }
}