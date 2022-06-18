using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    //public Slider slider;

    public RectTransform _fillImage;
    Tween _t = null;
    //public void SetMaxHealth(int health)
    //{
    //    slider.maxValue = health;
    //    slider.value = health;
    //}
    public void SetHealth(float value)
    {
        if(_t != null && _t.IsActive()) //실행중일면
        {
            _t.Kill(); 
        }
        value = Mathf.Clamp(value, 0f, 1f);
        _t = _fillImage.DOScaleX(value, 0.2f);

        //slider.value = health;

    }

}
