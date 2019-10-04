using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class tes : MonoBehaviour {
    public GameObject arrow;
    public GameObject TmpPlayer;
    public GameObject TmpPeople;
    Hand hand;
    private bool flag = true;
    private bool setflag = true;
    Vector3 tmp;
    private Vector3 upview;

    // Use this for initialization
    void Start()
    {
        upview.Set(10, 20, 10);
        hand = gameObject.GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        ;
        if (hand.grabGripAction.GetStateDown(hand.handType))
        {
            Debug.Log("Trigger");
            flag = !flag;
        }

        if (flag)
        {
            TmpPeople.SetActive(false);
            arrow.SetActive(true);
            if (!setflag)
            {
                TmpPlayer.transform.position = tmp;
                setflag = !setflag;
            }
        }
        else
        {
            arrow.SetActive(false);
            TmpPeople.SetActive(true);
            if (setflag)
            {   
                tmp = TmpPlayer.transform.position;
                setflag = !setflag;
            }
            else
            {
                TmpPlayer.transform.position = upview;
                TmpPeople.transform.position = tmp;

            }

        }

    }
}
