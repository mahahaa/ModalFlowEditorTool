using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ModalNode", menuName = "Modal Flow/Node")]
public class ModalNodeData : ScriptableObject
{
    public string nodeId;
    public GameObject modalPrefab;
    public List<string> nextNodeIds = new();

    public UnityEvent onShow;
    public UnityEvent onContinue;
    public UnityEvent onCancel;
}
