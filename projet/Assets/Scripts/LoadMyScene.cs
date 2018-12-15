using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMyScene : MonoBehaviour {

    //Use this for initialization
    private void Start()
    {
        SceneManager.LoadSceneAsync("Scenes/Scene1", LoadSceneMode.Single);
    }
}
