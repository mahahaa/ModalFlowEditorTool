using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModalFlow", menuName = "Modal Flow/Flow")]
public class ModalFlowData : ScriptableObject
{
    public string entryNodeId;
    public List<ModalNodeData> nodes = new();
}
