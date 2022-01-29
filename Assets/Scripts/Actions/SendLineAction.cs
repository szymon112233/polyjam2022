using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SendLineAction", menuName = "SendLineAction", order = 0)]
public class SendLineAction : BasicAction
{
	[SerializeField]
	private GameObject RayPrefab;
	[SerializeField]
	private GameObjectEvent LightObjectCreated;
	[SerializeField]
	private GameObjectEvent LightObjectDestroyed;

	[SerializeField]
	private bool IsAffectedByDirectionVectorLength;

	public override void DoAction(BasicNode invokingNode, Vector3 direction)
	{
		var desiredRayRotation = Vector3.Angle(Vector3.up, direction);

		if (direction.x > 0)
		{
			desiredRayRotation = -desiredRayRotation;
		}

		float currentRange;

		if (IsAffectedByDirectionVectorLength)
		{
			currentRange = direction.magnitude;
		}
		else
		{
			currentRange = Range;
		}

		var rayObject = Instantiate(RayPrefab, invokingNode.transform.position, Quaternion.Euler(0f, 0f, desiredRayRotation), invokingNode.transform);
		invokingNode.StartCoroutine(RayMovementCoroutine(rayObject, currentRange));

		LightObjectCreated.Action?.Invoke(rayObject);
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
			var newScale = new Vector3(rayObject.transform.localScale.x, rayObject.transform.localScale.y + Speed * Time.deltaTime, rayObject.transform.localScale.z);
			rayObject.transform.localScale = newScale;
			rayObject.transform.Translate(0f, -Speed * Time.deltaTime * 0.5f, 0f, Space.Self);
			yield return null;
		}

		LightObjectDestroyed.Action?.Invoke(rayObject);
		Destroy(rayObject);
	}
}
