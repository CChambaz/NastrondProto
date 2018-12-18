using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLimits : MonoBehaviour
{
    public Transform LCorner;
    public Transform RCorner;
    public Transform TCorner;
    public Transform DCorner;
    public Transform center;

    public float GetHeight()
    {
        return (TCorner.position-LCorner.position).magnitude;
    }

    public float GetWidth()
    {
        return (DCorner.position - LCorner.position).magnitude;
    }

    public Transform GetTransformL()
    {
        return LCorner;
    }

    public Transform GetTransformR()
    {
        return RCorner;
    }

    public Transform GetTransformT()
    {
        return TCorner;
    }

    public Transform GetTransformD()
    {
        return DCorner;
    }
    public Transform GetCenter()
    {
        return center;
    }
}
