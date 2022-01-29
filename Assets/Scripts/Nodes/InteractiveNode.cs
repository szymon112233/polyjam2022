using System.Collections.Generic;
using UnityEngine;

public class InteractiveNode : BasicNode
{
	public List<BasicAction> Actions;
	
	public void Fire(Vector2 direction)
	{
		if (Actions.Count == 0)
		{
			return;
		}
		Actions[0].DoAction(direction);
		Actions.RemoveAt(0);
	}
}