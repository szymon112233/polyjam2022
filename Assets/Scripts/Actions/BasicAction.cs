using UnityEngine;

public abstract class BasicAction : ScriptableObject
{
	[SerializeField]
	protected float Range;
	[SerializeField]
	protected float Speed;

	public abstract void DoAction(BasicNode invokingNode, Vector3 direction);
}
