using System.Collections.Generic;
using UnityEngine;

public class InteractiveNode : BasicNode
{
	public List<BasicAction> Actions;

	public float range;

	[SerializeField]
	private bool DeleteUsedActions = true;

	protected override void Awake()
	{
		base.Awake();
		Vector3 angle = -transform.up * range;
		
		OnActivated.AddListener(()=> Fire(angle)); 
	}

	public void Fire(Vector3 direction)
	{
		if (Actions.Count == 0)
		{
			return;
		}

		Actions[0].DoAction(this, direction);

		if (DeleteUsedActions)
		{
			Actions.RemoveAt(0);
		}
	}

	public bool HasAnyActionsLeft()
	{
		return Actions.Count > 0;
	}
}