using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameOverMenu : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("ReStart").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.SampleScene);
        };
    }
}
