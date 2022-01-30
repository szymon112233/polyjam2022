using UnityEngine;

public class Billboard : MonoBehaviour
{
	[SerializeField]
	private bool ShouldBeInvert;

	void Update()
	{
		var lookPos = Camera.main.transform.position - transform.position;
		lookPos.x = 0;
		transform.rotation = Quaternion.LookRotation(ShouldBeInvert ? -lookPos : lookPos);
	}
}
