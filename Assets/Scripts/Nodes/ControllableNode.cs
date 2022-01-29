using UnityEngine;

public class ControllableNode : InteractiveNode
{
	[SerializeField]
	private Vector3Event ShootInputFinished;
	[SerializeField]
	private Vector3Event DuringShootInput;
	[SerializeField]
	private LineRenderer LineRenderer;

	private Vector3 ShootVector;

	private void Awake()
	{
		ShootInputFinished.Action += OnShootInputFinished;
		DuringShootInput.Action += OnDuringShootInput;
	}

	private void OnDestroy()
	{
		ShootInputFinished.Action -= OnShootInputFinished;
		DuringShootInput.Action -= OnDuringShootInput;
	}

	private void OnShootInputFinished(Vector3 inputDelta)
	{
		LineRenderer.enabled = false;
		Fire(ShootVector);
	}

	private void OnDuringShootInput(Vector3 pointerPos)
	{
		AimArrow(pointerPos);
	}

	private void AimArrow(Vector3 worldPointerPos)
	{
		LineRenderer.enabled = true;
		var localPos = transform.InverseTransformPoint(worldPointerPos);
		localPos.z = 0.0f;
		ShootVector = localPos;
		LineRenderer.SetPosition(0, localPos);
	}
}
