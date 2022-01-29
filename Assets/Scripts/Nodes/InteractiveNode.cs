using System.Collections.Generic;
using UnityEngine;

public class InteractiveNode : BasicNode
{
	public List<BasicAction> Actions;

	[SerializeField]
	private bool DeleteUsedActions = true;

	private void Awake()
	{
		OnActivated.AddListener(()=> Fire(transform.localRotation.eulerAngles)); 
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
}