using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BasicNode : MonoBehaviour
{
	[SerializeField]
	private NodeEvent NodeActivatedEvent;

	[SerializeField]
	private List<AudioClip> ActivationSounds;

	private AudioSource AudioSource;

	public bool Activated
	{
		get
		{
			return _Activated;
		}
		set
		{

			if (value)
			{
				if (!_Activated)
				{
					_Activated = value;
					OnActivated.Invoke();
					NodeActivatedEvent.Action?.Invoke(this);
					PlayRandomActivationSound();
				}
			}
			else
			{
				OnDeactivated.Invoke();
			}
		}
	}

	protected virtual void Awake()
	{
		AudioSource = GetComponent<AudioSource>();
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

	private void PlayRandomActivationSound()
	{
		if (ActivationSounds.Count > 0)
		{
			AudioSource.PlayOneShot(ActivationSounds[Random.Range(0, ActivationSounds.Count)]);
		}
	}
}
