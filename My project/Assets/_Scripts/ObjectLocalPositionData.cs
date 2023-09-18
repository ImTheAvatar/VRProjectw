using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectLocalPositionData 
{
    [SerializeField] public List<ObjectData> data;
    [SerializeField] public int id;
}
[System.Serializable]
public class ObjectData
{
    [SerializeField] public Vector3 position;
    [SerializeField] public Vector3 rotation;
    [SerializeField] public int id;
}
