using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "CircleWavesAction", menuName = "CircleWavesAction", order = 0)]
public class CircleWavesAction : BasicAction
{

	[SerializeField]
	private GameObject CirclePrefab;
	
	
	public override void DoAction(BasicNode invokingNode, Vector3 direction)
	{
		float currentRange = Mathf.Clamp(direction.magnitude, 0.0f, Range);

		var rayObject = Instantiate(CirclePrefab, invokingNode.transform.position, Quaternion.identity);
		invokingNode.StartCoroutine(MovementCoroutine(rayObject, currentRange));
	}

	private IEnumerator MovementCoroutine(GameObject movingObject, float maxRadius)
	{
		var circleCollider = movingObject.GetComponent<CircleCollider2D>();

		if (circleCollider == null)
		{
			Debug.LogError("ADD SOME CIRCLE COLLIDER TO PREFAB PLEASE.");
			yield break;
		}

		while (movingObject.transform.localScale.y < maxRadius)
		{
			var newScale = new Vector3(movingObject.transform.localScale.x + Speed * Time.deltaTime, movingObject.transform.localScale.y + Speed * Time.deltaTime, movingObject.transform.localScale.z);
			movingObject.transform.localScale = newScale;
			yield return null;
		}

		Destroy(movingObject);
	}
}