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



public class InteractiveCube : MonoBehaviour, IFocusable, IInputClickHandler
{

    private int statusSocket = 0;
    public bool Rotating;

    public void OnFocusEnter()
    {
        Rotating = true;
    }
    public void OnFocusExit()
    {
        Rotating = false;
    }

    public float RotationSpeed;
    void Update()
    {
        /*if (Rotating)
            transform.Rotate(Vector3.up * Time.deltaTime
                * RotationSpeed);*/
        //Debug.Log("je suis cliquable depuis la fonction update");
    }

    public Vector3 ScaleChange;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (this.statusSocket == 1)
        {
            StartCoroutine(SwitchOffWallSocket("17")); // when click on the cube => start a coroutine to send a get request on Vera hub for the momemt
        }
        else
        {
            StartCoroutine(SwitchOnWallSocket("17"));
        }
         
    }


    IEnumerator GetRequestToVera()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.1.34/port_3480/data_request?id=lu_action&output_format=json&DeviceNum=6&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=0"); //get request to Vera server Need to change it to send post/put request to other devices 
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    IEnumerator SwitchOffWallSocket(string id)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=bonjour"));

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.33/port_3480/data_request?id=lu_action&output_format=json&DeviceNum="+id+"&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=0", formData); // need to add the real url
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        this.statusSocket = 0;
    }


    IEnumerator SwitchOnWallSocket(string id)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=bonjour"));

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.33/port_3480/data_request?id=lu_action&output_format=json&DeviceNum=" + id + "&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=1", formData); // need to add the real url
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        this.statusSocket = 1;
    }


        IEnumerator PutRequest()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("add the query that you need");
        UnityWebRequest www = UnityWebRequest.Put("http://add the url that you need", myData);
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

    }


    // Use this for initialization
    void Start()
    {
    }

}