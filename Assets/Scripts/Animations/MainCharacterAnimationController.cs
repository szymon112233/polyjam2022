using UnityEngine;
using System.Collections;

public class MainCharacterAnimationController : MonoBehaviour
{
	[SerializeField]
	private Vector3Event DuringShootInput;
	[SerializeField]
	private Vector3Event ShootInputFinished;

	[SerializeField]
	private GameObject IdleSprite;
	[SerializeField]
	private GameObject FocusSprite;
	[SerializeField]
	private GameObject ActionSprite;

	[SerializeField]
	private float ActionSpriteShowLength;

	private void Awake()
	{
		DuringShootInput.Action += OnDuringShootInput;
		ShootInputFinished.Action += OnShootInputFinished;
	}

	private void OnDestroy()
	{
		DuringShootInput.Action -= OnDuringShootInput;
		ShootInputFinished.Action -= OnShootInputFinished;
	}

	private void OnDuringShootInput(Vector3 _)
	{
		IdleSprite.SetActive(false);
		FocusSprite.SetActive(true);
	}

	private void OnShootInputFinished(Vector3 _)
	{
		FocusSprite.SetActive(false);
		ActionSprite.SetActive(true);

		StartCoroutine(RestoreIdleSpriteAfterDelay());
	}

	private IEnumerator RestoreIdleSpriteAfterDelay()
	{
		yield return new WaitForSeconds(ActionSpriteShowLength);

		ActionSprite.SetActive(false);
		IdleSprite.SetActive(true);
	}
}
