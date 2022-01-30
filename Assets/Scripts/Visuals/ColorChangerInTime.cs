using UnityEngine;

public class ColorChangerInTime : MonoBehaviour
{
	[SerializeField]
	private float ChangeTime = 0.1f;

	private MeshRenderer meshRenderer;
	private float timer;

	private void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer >= ChangeTime)
		{
			timer = 0f;
			var newHue = Random.Range(0f, 1f);
			meshRenderer.material.SetColor("_EmissionColor", Color.HSVToRGB(newHue, 1f, 1f));
		}
	}
}
