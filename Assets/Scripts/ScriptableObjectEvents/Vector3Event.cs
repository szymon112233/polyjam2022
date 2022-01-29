using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Event", menuName = "Vector2Event", order = 0)]
public class Vector3Event : ScriptableObject
{
	public Action<Vector3> Action;
}
