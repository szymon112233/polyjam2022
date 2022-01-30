using System.Collections.Generic;
using UnityEngine;

public class AnimatorBetter : MonoBehaviour
{
	public List<Sprite> FrameSprites = new List<Sprite>();
	public float FrameTime;

	private SpriteRenderer SpriteRenderer;

	private int frameIndex;
	private float timer;

	[SerializeField]
	private float maxRandomOffset = 0.5f;

	private void Awake()
	{
		frameIndex = 0;
		timer = Random.Range(0f, maxRandomOffset);
		SpriteRenderer = GetComponent<SpriteRenderer>();
		SpriteRenderer.sprite = FrameSprites[frameIndex];
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer >= FrameTime)
		{
			frameIndex++;
			if (frameIndex >= FrameSprites.Count)
			{
				frameIndex = 0;
			}
			timer = 0;
			SpriteRenderer.sprite = FrameSprites[frameIndex];
		}
	}
}
