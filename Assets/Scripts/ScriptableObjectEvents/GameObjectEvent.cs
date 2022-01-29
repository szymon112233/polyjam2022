using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectEvent", menuName = "GameObjectEvent", order = 0)]
public class GameObjectEvent : ScriptableObject
{
    public Action<GameObject> Action;
}
