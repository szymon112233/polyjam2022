using System;
using Unity.Mathematics;
using UnityEngine;


public class Billboard : MonoBehaviour
{
	void Update()
	{
		var lookPos = Camera.main.transform.position - transform.position;
		lookPos.x = 0;
		transform.rotation = Quaternion.LookRotation(lookPos);
	}
}
