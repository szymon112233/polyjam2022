using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	public Camera camera;
	
	public InputActionAsset InputActionAsset;
	
	public InputActionReference ShootAction;
	
	public InputActionReference PointerDelta;

	public Vector3Event ShootInputFinished;
	public Vector3Event DuringShootInput;

	private Vector2 UnusedinputDelta;
	private bool isMouseButtonDown;

	private void Awake()
	{
		InputActionAsset.FindActionMap("main").Enable();
		
		ShootAction.action.started += ActionOnstarted;
		ShootAction.action.performed += ActionOnperformed;
		ShootAction.action.canceled +=ActionOncanceled;
	}

	private void OnDestroy()
	{
		ShootAction.action.started -= ActionOnstarted;
		ShootAction.action.performed -= ActionOnperformed;
		ShootAction.action.canceled -= ActionOncanceled;
	}

	private void Update()
	{
		if (isMouseButtonDown)
		{
			Vector2 screenPointerPos = PointerDelta.action.ReadValue<Vector2>();
			Vector3 worldPointerPos = camera.ScreenToWorldPoint(new Vector3(screenPointerPos.x, screenPointerPos.y, -camera.transform.position.z), Camera.MonoOrStereoscopicEye.Mono);
			
			Debug.Log($"screenPointerPos: {screenPointerPos}");
			Debug.Log($"worldPointerPos: {worldPointerPos}");
			
			DuringShootInput.Action?.Invoke(worldPointerPos);
		}
	}

	private void ActionOnstarted(InputAction.CallbackContext obj)
	{
		Debug.Log("Action started");
	}

	private void ActionOnperformed(InputAction.CallbackContext obj)
	{
		isMouseButtonDown = true;
		Debug.Log("Action performed");
	}

	private void ActionOncanceled(InputAction.CallbackContext obj)
	{
		isMouseButtonDown = false;
		Debug.Log($"Action canceled. Cumulated input: {UnusedinputDelta}");
		ShootInputFinished.Action?.Invoke(UnusedinputDelta);
		UnusedinputDelta = Vector2.zero;
	}
}
