using UnityEngine;
using UnityEngine.Events;

public class BasicNode : MonoBehaviour
{
	[SerializeField]
	private NodeEvent NodeActivatedEvent;
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
				}
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
