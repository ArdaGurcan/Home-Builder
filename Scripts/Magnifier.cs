using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Magnifier : MonoBehaviour
{
    public GameObject decorator;
    public GameObject cam;
    public Vector3 camPos;
    bool mouseOver;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
        
    //    /if (mouseOver){
    //        cam.GetComponent<Camera>().orthographicSize = 3f;
    //        Vector3 mousePosition = Input.mousePosition;
    //        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //        cam.transform.position = mousePosition;
    //    }
    //    else 
    //    {
    //        cam.GetComponent<Camera>().orthographicSize = 10f;

    //        cam.transform.position = camPos;
    //    }
    //}

	public void MouseOver()
	{
        Debug.Log("mouseoverme");
        mouseOver = true;
	}

	public void MouseExit()
	{
        //mouseOver = false;
	}
}
