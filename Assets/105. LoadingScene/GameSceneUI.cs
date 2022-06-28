using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameSceneUI : MonoBehaviour {

    private void Awake() {
        transform.Find("Start").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Main Menu");
            Loader.Load(Loader.Scene.SampleScene);
        };

        transform.Find("End").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Application.Quit();   
        };
    }

}
