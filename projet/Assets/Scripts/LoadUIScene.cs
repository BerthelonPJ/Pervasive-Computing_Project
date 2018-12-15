using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity.InputModule;

public class LoadUIScene : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        SceneManager.LoadSceneAsync("Scenes/SceneUI", LoadSceneMode.Additive);
    }
}
