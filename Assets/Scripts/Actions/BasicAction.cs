using UnityEngine;

public abstract class BasicAction : ScriptableObject
{
	public float Range;

	public abstract void DoAction(BasicNode invokingNode, Vector3 direction);
}
