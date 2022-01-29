using UnityEngine;

public abstract class BasicAction : ScriptableObject
{
	public float Range;

	public abstract void DoAction(Vector2 direction);
}
