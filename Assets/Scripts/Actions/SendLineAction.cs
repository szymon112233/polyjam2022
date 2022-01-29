using UnityEngine;

[CreateAssetMenu(fileName = "SendLineAction", menuName = "SendLineAction", order = 0)]
public class SendLineAction : BasicAction
{
	[SerializeField]
	private float SpeedOfRay;

	[SerializeField]
	private GameObject RayPrefab;

	public override void DoAction(BasicNode invokingNode, Vector2 direction)
	{
		

		//Instantiate(RayPrefab, invokingNode.transform.position, Quaternion.to)
	}
}
