
using Bonsai.Core;
using UnityEditor;
using UnityEngine;

namespace Bonsai.Designer
{
  [CustomEditor(typeof(BehaviourTree))]
  public class BonsaiTreeInspector : Editor
  {
    private string keyNameSelection = "";

    // State of the fold out if it is opened or closed.
    private bool showBlackboardFoldout = true;

    // Cache the label spacing options.
    private static readonly GUILayoutOption[] deleteButtonSize = new GUILayoutOption[] { GUILayout.Width(18f) };

    void OnEnable()
    {
      if (EditorApplication.isPlaying)
      {
        EditorApplication.update += Repaint;
      }
    }

    void OnDisable()
    {
      EditorApplication.update -= Repaint;
    }

    public override void OnInspectorGUI()
    {
      var tree = target as BehaviourTree;

      EditorGUILayout.LabelField("Behaviour Tree", tree.name);
      EditorGUILayout.Space();
      
      if (GUILayout.Button("Open Window"))
      {
        var window = BonsaiWindow.GetNewWindow(tree, Application.isPlaying ? BonsaiEditor.Mode.View : BonsaiEditor.Mode.Edit);
      }

      EditorGUILayout.Space();
      ShowTreeStats(tree);
    }

    private void ShowTreeStats(BehaviourTree tree)
    {
      if (EditorApplication.isPlaying && tree.IsInitialized())
      {
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Total nodes", tree.Nodes.Length.ToString());
        EditorGUILayout.LabelField("Active timers", tree.ActiveTimerCount.ToString());
      }
    }
  }
}