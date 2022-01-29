using UnityEngine;


[CreateAssetMenu(fileName = "Action", menuName = "Action", order = 0)]
public class BasicAction : ScriptableObject
{
	public float Range;
	
	public  virtual void DoAction(Vector2 direction)
	{
		
	}
}
