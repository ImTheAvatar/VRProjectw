using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsLocalPosition : ScriptableObjectBehavior
{
    public List<ObjectLocalPositionData> data;
    public ObjectLocalPositionData GetData(int id)
    {
        return data.FirstOrDefault(a => a.id == id);
    }
}
