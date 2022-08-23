using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionExtensions
{
    public static bool IsLessThan(this Vector3 position, Vector3 compared, float distance)
    {
        return Vector3.Distance(position, compared) < distance;
    }
}
