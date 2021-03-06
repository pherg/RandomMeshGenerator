﻿using UnityEngine;

public static class Noise
{
    public static float Value(Vector3 point, float frequency)
    {
        point *= frequency;
        int i = (int)point.x;
        return i & 1;
    }
}