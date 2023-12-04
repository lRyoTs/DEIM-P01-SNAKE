using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event EventHandler OnFoodSpawn;

    public static void CallEventOnFoodSpawn() {
        if (OnFoodSpawn != null) {
            OnFoodSpawn(null, EventArgs.Empty);
        }
    }
}
