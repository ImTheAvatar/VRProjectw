using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ParentHandler Prefab;
    public List<ParentHandler> parents;
    public Vector3 offset;
}
