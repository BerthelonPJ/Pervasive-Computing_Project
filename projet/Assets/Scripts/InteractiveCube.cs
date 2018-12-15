using System.Collections;
using HoloToolkit.Unity.InputModule;
using System;
using System.Net;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;



public class InteractiveCube : MonoBehaviour, IFocusable, IInputClickHandler
{

   
   
    private int nb = 0;
    private int position = -2;
    public void OnFocusEnter()
    {
     
    }
    public void OnFocusExit()
    {
 
    }

    public int id;
    void Update()
    {
    }


    public void OnInputClicked(InputClickedEventData eventData)
    {
         
    }

    int ind = 0;
    IEnumerator GetRequestToVera()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.1.33:3480/data_request?id=sdata&room=2"); //get request to Vera server Need to change it to send post/put request to other devices 
        yield return www.SendWebRequest();
        var stuff = JObject.Parse(www.downloadHandler.text);
        JArray devices = (JArray)stuff["devices"];
        for (int i = 0; i < devices.Count; i++)
        {
            if ((string)devices[i]["name"] != "" && (string)devices[i]["name"] != "Philips Hue Plugin")
            {
                setCube((int)devices[i]["id"], (string)devices[i]["name"] , (string)devices[i]["type"]);
                Debug.Log(ind);
                ind++;
            }
        }
    }

    void setCube(int id , string name , string type )
    {
        this.position++;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(position, 0, 5);
        cube.transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);
        cube.AddComponent<ActionButton>();
        cube.AddComponent<LoadUIScene>();
        cube.GetComponent<ActionButton>().setId(id , name , type);
    }

    // Use this for initialization
    void Start()
    {
        
        StartCoroutine(GetRequestToVera());
       
    }

}