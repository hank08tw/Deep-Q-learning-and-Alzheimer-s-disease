using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour {


    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("Button Clicked. ClickHandler.");
        GameObject.Find("Controller (right)").GetComponent<MapControl>().Restart();
    }

}
