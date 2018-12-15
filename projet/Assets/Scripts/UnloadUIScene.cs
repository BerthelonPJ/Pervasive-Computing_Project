using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity.InputModule;

public class UnloadUIScene : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        SceneManager.UnloadSceneAsync("Scenes/SceneUI");
    }
}
