using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void SnakeEat();
    public static SnakeEat onSnakeEat;

    public static void CallEventOnSnakeEat() {
        if (onSnakeEat != null) {
            onSnakeEat();
        }
    }
}
