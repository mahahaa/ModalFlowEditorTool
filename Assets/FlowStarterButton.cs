using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowStarterButton : MonoBehaviour
{
    [SerializeField] private ModalFlowRunner flowRunner;
    [SerializeField] private ModalFlowData flowData;
    [SerializeField] private RectTransform modalContainer;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (flowRunner == null)
        {
            Debug.LogError("FlowRunner is not assigned on FlowStarterButton.", this);
            button.interactable = false;
            return;
        }

        button.onClick.AddListener(OnClickStartFlow);
    }

    private void OnClickStartFlow()
    {
        if (!flowRunner.IsRunning)
        {
            Debug.Log("Starting modal flow from FlowStarterButton.");
            flowRunner.RunFlow(flowData, modalContainer);
        }
        else
        {
            Debug.Log("Flow already running, ignoring click.");
        }
    }
}
