using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ModalFlowEditorWindow : EditorWindow
{
    private ModalFlowData currentFlow;

    [MenuItem("Tools/Modal Flow Editor")]
    public static void ShowWindow()
    {
        GetWindow<ModalFlowEditorWindow>("Modal Flow Editor");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        currentFlow = (ModalFlowData)EditorGUILayout.ObjectField("Flow Asset", currentFlow, typeof(ModalFlowData), false);

        if (currentFlow == null) return;

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Entry Node ID", EditorStyles.boldLabel);
        currentFlow.entryNodeId = EditorGUILayout.TextField(currentFlow.entryNodeId);

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Modal Nodes", EditorStyles.boldLabel);

        for (int i = 0; i < currentFlow.nodes.Count; i++)
        {
            var node = currentFlow.nodes[i];

            EditorGUILayout.BeginVertical("box");
            node.nodeId = EditorGUILayout.TextField("Node ID", node.nodeId);
            node.modalPrefab = (GameObject)EditorGUILayout.ObjectField("Modal Prefab", node.modalPrefab, typeof(GameObject), false);

            EditorGUILayout.LabelField("Next Node IDs");
            for (int j = 0; j < node.nextNodeIds.Count; j++)
            {
                node.nextNodeIds[j] = EditorGUILayout.TextField($"Next [{j}]", node.nextNodeIds[j]);
            }

            if (GUILayout.Button("Add Next Node ID"))
            {
                node.nextNodeIds.Add("");
            }

            if (GUILayout.Button("Remove Node"))
            {
                currentFlow.nodes.RemoveAt(i);
                break;
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
        }

        if (GUILayout.Button("Add New Node"))
        {
            var newNode = ScriptableObject.CreateInstance<ModalNodeData>();
            newNode.name = "NewModalNode";
            AssetDatabase.AddObjectToAsset(newNode, currentFlow);
            AssetDatabase.SaveAssets();
            currentFlow.nodes.Add(newNode);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(currentFlow);
        }
    }
}
