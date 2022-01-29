using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SendLineAction", menuName = "SendLineAction", order = 0)]
public class SendLineAction : BasicAction
{
	[SerializeField]
	private float SpeedOfRay;

	[SerializeField]
	private GameObject RayPrefab;

	[SerializeField]
	private bool IsAffectedByDirectionVectorLength;

	public override void DoAction(BasicNode invokingNode, Vector3 direction)
	{
		var relativeDirection = direction - invokingNode.transform.position;
		var desiredRayRotation = Vector3.Angle(Vector3.up, relativeDirection);

		if (relativeDirection.x > 0)
		{
			desiredRayRotation = -desiredRayRotation;
		}

		float currentRange;

		if (IsAffectedByDirectionVectorLength)
		{
			currentRange = relativeDirection.magnitude;
		}
		else
		{
			currentRange = Range;
		}

		var rayObject = Instantiate(RayPrefab, invokingNode.transform.position, Quaternion.Euler(0f, 0f, desiredRayRotation));
		invokingNode.StartCoroutine(RayMovementCoroutine(rayObject, currentRange));
	}

	private IEnumerator RayMovementCoroutine(GameObject rayObject, float currentRange)
	{
		var capsuleCollider2D = rayObject.GetComponent<CapsuleCollider2D>();

		if (capsuleCollider2D == null)
		{
			Debug.LogError("ADD SOME CAPSULE COLLIDER TO PREFAB PLEASE.");
			yield break;
		}

		while (rayObject.transform.localScale.y < currentRange)
		{
			var newScale = new Vector3(rayObject.transform.localScale.x, rayObject.transform.localScale.y + SpeedOfRay * Time.deltaTime, rayObject.transform.localScale.z);
			rayObject.transform.localScale = newScale;
			rayObject.transform.Translate(0f, -SpeedOfRay * Time.deltaTime * 0.5f, 0f, Space.Self);
			yield return null;
		}
	}
}
