using UnityEngine;

namespace ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "DebugAction", menuName = "DebugAction", order = 0)]
    public class DebugAction : BasicAction
    {
        public override void DoAction(BasicNode invokingNode, Vector3 direction)
        {
            Debug.Log($"{invokingNode.name} do action with direction {direction}");
        }
    }
}