using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeEvent", menuName = "NodeEvent", order = 0)]
public class NodeEvent : ScriptableObject
{
    public Action<BasicNode> Action;
}
