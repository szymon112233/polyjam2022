using System;
using UnityEngine;
using UnityEngine.Events;

public class BasicNode : MonoBehaviour
{
	public bool Activated
	{
		get
		{
			return _Activated;
		}
		set
		{
			_Activated = value;

			if (value)
			{
				OnActivated.Invoke();
			}
			else
			{
				OnDeactivated.Invoke();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Wave"))
		{
			Activated = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Wave"))
		{
			//Activated = false;
		}
	}

	private bool _Activated;

	public UnityEvent OnActivated;
	public UnityEvent OnDeactivated;
}
