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
using UnityEngine.UI;
using HoloToolkit.Unity;
using HoloToolkit.Unity.Buttons;

public class ActionButton : MonoBehaviour, IFocusable, IInputClickHandler
{
    private int statusSocket = 0;
    private bool done = false;
    private int id;
    private string nom;
    private string type;

    public void setId(int id , string name , string type)
    {
        this.id = id;
        this.nom = name;
        this.type = type;
    }
    public void OnFocusEnter()
    {

    }

    public void OnFocusExit()
    {
        
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
    
        if (this.statusSocket == 1)
        {
            StartCoroutine(SwitchOffWallSocket(id)); // when click on the cube => start a coroutine to send a get request on Vera hub for the momemt
        }
        else
        {
            StartCoroutine(SwitchOnWallSocket(id));
        }
    }

    IEnumerator SwitchOffWallSocket(int id)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=bonjour"));

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.33/port_3480/data_request?id=lu_action&output_format=json&DeviceNum=" + id + "&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=0", formData); // need to add the real url
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        this.statusSocket = 0;
    }


    IEnumerator SwitchOnWallSocket(int id)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=bonjour"));

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.33/port_3480/data_request?id=lu_action&output_format=json&DeviceNum=" + id + "&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=1", formData); // need to add the real url
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        this.statusSocket = 1;
    }
    void Start()
    {

    }
}
