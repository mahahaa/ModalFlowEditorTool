using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModalFlowRunner : MonoBehaviour
{
    private Stack<string> nodeHistory = new();
    private bool isRunning = false;
    public bool IsRunning => isRunning;

    public async void RunFlow(ModalFlowData flowData, RectTransform ModalContainer)
    {
        if (isRunning || flowData == null)
        {
            Debug.LogWarning("Flow is null or already running.");
            return;
        }

        isRunning = true;
        nodeHistory.Clear();

        string currentId = flowData.entryNodeId;

        while (!string.IsNullOrEmpty(currentId))
        {
            var node = flowData.nodes.FirstOrDefault(n => n.nodeId == currentId);
            if (node == null || node.modalPrefab == null)
            {
                Debug.LogWarning($"Node not found or missing prefab: {currentId}");
                break;
            }

            node.onShow?.Invoke();

            var instance = Instantiate(node.modalPrefab, ModalContainer);
            var modal = instance.GetComponent<IModalController>();
            if (modal == null)
            {
                Debug.LogWarning("Modal prefab missing IModalController");
                break;
            }

            var result = await modal.ShowAsync();
            Destroy(instance.gameObject);

            if (result == ModalResults.ModalResult.Continue)
            {
                node.onContinue?.Invoke();
                nodeHistory.Push(currentId);
                currentId = node.nextNodeIds.FirstOrDefault();
            }
            else if (result == ModalResults.ModalResult.Cancel)
            {
                node.onCancel?.Invoke();
                if (nodeHistory.Count > 0)
                {
                    currentId = nodeHistory.Pop();
                }
                else
                {
                    Debug.Log("No previous modal in history.");
                    break;
                }
            }
        }

        isRunning = false;
    }
}