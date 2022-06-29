using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : SingleT<GameEvent>
{


    public event Action onPlayerHealth;
    public void PlayerHealthDown()
    {
        if(onPlayerHealth != null)
        {
            onPlayerHealth();
        }
    }

        
}
