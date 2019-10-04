using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;


public class MapControl : MonoBehaviour
{
    public GameObject Map;
    public GameObject PersonModel;
    public GameObject TmpPlayer;
    public GameObject Camera;
    public GameObject Timeleft;
    public Text Timecost;
    public float timer = 5.0f;
    Hand Hand;
    private bool ShowMap = false,endjudge = false, Starttest = false;
    private Vector3 StartPosition;
    private float ViewMapTime;


    private GameObject maze;
    private float timebegin, timeend;

    // Use this for initialization
    void Start()
    {
        Hand = gameObject.GetComponent<Hand>();
        PersonModel.SetActive(true);
        StartPosition = TmpPlayer.transform.position;
        ViewMapTime = timer;
        Map.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (!Starttest)
        {
            if (GameObject.Find("Button1").GetComponent<ClickHandler>().CheckStart()) { ActivateMaze("maze", "maze1"); GameObject.Find("Button1").GetComponent<ClickHandler>().Reset(); }
            if (GameObject.Find("Button2").GetComponent<ClickHandler>().CheckStart()) { ActivateMaze("maze", "maze2"); GameObject.Find("Button2").GetComponent<ClickHandler>().Reset(); }
            if (GameObject.Find("Button3").GetComponent<ClickHandler>().CheckStart()) { ActivateMaze("maze", "maze3"); GameObject.Find("Button3").GetComponent<ClickHandler>().Reset(); }
            if (GameObject.Find("Button4").GetComponent<ClickHandler>().CheckStart()) { ActivateMaze("maze", "mazegenrator"); GameObject.Find("Button4").GetComponent<ClickHandler>().Reset(); }
        }

        else
        {   //set the person's model in the watching camera
            PersonModel.transform.position = TmpPlayer.transform.position;
            PersonModel.transform.rotation = Camera.transform.rotation;

            //timer is the time for tested person to watch the map
            ViewMapTime -= Time.deltaTime;
            if (ViewMapTime >= 0)
            {
                Map.SetActive(true);
                Timeleft.GetComponent<TextMesh>().text = "Timeleft:" + ViewMapTime.ToString("f2");
            }
            // start test
            else
            {   
                //judge the postion and time costed, show them in two screen 
                if (Camera.transform.position.z > 18.5)
                {
                    if (!endjudge) { timeend = Time.time; endjudge = true; ShowMap = true; }

                    Timeleft.GetComponent<TextMesh>().text = "Total Timecost:" + (timeend - timebegin).ToString("f2");
                    Timecost.text = "Timecost:" + (timeend - timebegin).ToString("f2");
                }
                else if (!endjudge)
                {
                    Timeleft.GetComponent<TextMesh>().text = "Timecost:" + (Time.time - timebegin).ToString("f2");
                    Timecost.text = "Timecost:" + (Time.time - timebegin).ToString("f2");

                }

                //open and close the map
                if (Hand.grabGripAction.GetStateDown(Hand.handType))
                {
                    Debug.Log("Trigger");
                    ShowMap = !ShowMap;
                }

                Control_map(ShowMap, Map);
            }

        }

    }

    // show the map for the tested person
    void Control_map(bool flag,GameObject Map)
    {
        if (flag)
        {
            Map.SetActive(true);
        }
        else
        {
            Map.SetActive(false);
        }
    }

    //activate the map according to the chosen button and record the start time
    void ActivateMaze(string Root,string MazeName)
    {
        GameObject root = GameObject.Find(Root);
        maze = root.transform.Find(MazeName).gameObject;
        maze.SetActive(true);
        Starttest = true;
        timebegin = Time.time + timer;
    }

    void InActivateMaze(string Root, string MazeName)
    {
        GameObject root = GameObject.Find(Root);
        maze = root.transform.Find(MazeName).gameObject;
        maze.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restart");
        //PersonModel.transform.position = StartPosition;
        TmpPlayer.transform.position = StartPosition;

        InActivateMaze("maze", "maze1");
        InActivateMaze("maze", "maze2");
        InActivateMaze("maze", "maze3");
        InActivateMaze("maze", "mazegenrator");

        Starttest = false;
        timebegin = Time.time + timer;
        ViewMapTime = timer;
        endjudge = false;
        ShowMap = false;
        Map.SetActive(false);

        Timeleft.GetComponent<TextMesh>().text = "Timecost:";
        Timecost.text = "Timecost:";
    }

}
