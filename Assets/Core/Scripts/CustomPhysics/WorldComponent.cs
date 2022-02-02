using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldComponent : MonoBehaviour
{
    public Vector2 scale = new Vector3(1, 1);
    public bool drawGizmos = false;

    public BorderPoints GetBorderPoints()
    {
        float dsx = scale.x / 2;
        float dsy = scale.y / 2;
        Vector2 _1 = new Vector2(transform.position.x - dsx, transform.position.y - dsy);
        Vector2 _2 = new Vector2(transform.position.x + dsx, transform.position.y - dsy);
        Vector2 _3 = new Vector2(transform.position.x - dsx, transform.position.y - dsy);
        Vector2 _4 = new Vector2(transform.position.x + dsx, transform.position.y - dsy);
        Vector2 _5 = new Vector2(transform.position.x - dsx, transform.position.y + dsy);
        Vector2 _6 = new Vector2(transform.position.x + dsx, transform.position.y + dsy);
        Vector2 _7 = new Vector2(transform.position.x - dsx, transform.position.y + dsy);
        Vector2 _8 = new Vector2(transform.position.x + dsx, transform.position.y + dsy);

        return new BorderPoints(_1, _2, _3, _4, _5, _6, _7, _8);
    }

    public static bool IsPointWithinRange(Vector2 range, float inspected)
    {
        //Debug.Log($"range: {range} | inspectedPoint {inspected}");
        if (inspected >= range.x && inspected <= range.y) return true;
        return false;
    }
    public static bool DoComponentsIntersectByAxis(Vector2 firstRange, Vector2 secondRange)
    {
        if (IsPointWithinRange(firstRange, secondRange.x) ||
            IsPointWithinRange(firstRange, secondRange.y) ||
            IsPointWithinRange(secondRange, firstRange.x) ||
            IsPointWithinRange(secondRange, firstRange.y)) return true;
        return false;
    }

    public static bool DoComponentsIntersect(WorldComponent first, WorldComponent second)
    {
        Vector2 XaxisFirst = new Vector2(first.transform.position.x - first.scale.x / 2, first.transform.position.x + first.scale.x / 2);
        Vector2 XaxisSecond = new Vector2(second.transform.position.x - second.scale.x / 2, second.transform.position.x + second.scale.x / 2);
        Vector2 YaxisFirst = new Vector2(first.transform.position.y - first.scale.y / 2, first.transform.position.y + first.scale.y / 2);
        Vector2 YaxisSecond = new Vector2(second.transform.position.y - second.scale.y / 2, second.transform.position.y + second.scale.y / 2);

        if (DoComponentsIntersectByAxis(XaxisFirst, XaxisSecond) && 
            DoComponentsIntersectByAxis(YaxisFirst, YaxisSecond)) return true;
        return false;
    }

    public static void FixYPosition(WorldComponent hisPosToFix, WorldComponent second)
    {
        float upperMult = hisPosToFix.transform.position.y >= second.transform.position.y ? 1f : -1f;
        
        float YUpperPoint = second.transform.position.y + (second.scale.y / 2) * upperMult;
        hisPosToFix.transform.position = new Vector3(hisPosToFix.transform.position.x, YUpperPoint + (hisPosToFix.scale.y / 2) * upperMult, hisPosToFix.transform.position.z);
    }

    public float GetUpperPoint(WorldComponent comp) => (comp.transform.position.y + comp.scale.y / 2);
    public float GetBottomPoint(WorldComponent comp) => (comp.transform.position.y - comp.scale.y / 2);
}
