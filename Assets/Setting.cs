using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Setting : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("Quit").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Application.Quit();
        };
    }
}
