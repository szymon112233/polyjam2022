using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBetter : MonoBehaviour
{

	public List<Sprite> FrameSprites = new List<Sprite>();
	public float FrameTime;

	public SpriteRenderer renderer;

	private int frameIndex;
	private float timer;


	private void Awake()
	{
		frameIndex = 0;
		timer = 0;
		renderer.sprite = FrameSprites[frameIndex];

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
			renderer.sprite = FrameSprites[frameIndex];
		}
	}
}
