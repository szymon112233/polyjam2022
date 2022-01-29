using System.Collections.Generic;
using UnityEngine;

public class InteractiveNode : BasicNode
{
	public List<BasicAction> Actions;

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
		Actions.RemoveAt(0);
	}
}