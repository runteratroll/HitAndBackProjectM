using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{

    public RectTransform _fillImage;
    Tween _t = null;

    public void SetHealth(float value)
    {
        if(_t != null && _t.IsActive()) //�������ϸ�
        {
            _t.Kill(); 
        }
        value = Mathf.Clamp(value, 0f, 1f);
        _t = _fillImage.DOScaleX(value, 0.2f);



    }

}
