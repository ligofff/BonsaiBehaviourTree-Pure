﻿using UnityEngine;
using Bonsai.Core;

namespace Tests
{
    public class TestNode : Task
    {
        public float? Utility { get; set; } = null;
        public Status ReturnStatus { get; set; }

        public const string kHistoryKey = "TraverseHistory";

        public override void OnStart()
        {
        }

        public override Status Run()
        {
            return ReturnStatus;
        }

        public override float UtilityValue()
        {
            return Utility.GetValueOrDefault(base.UtilityValue());
        }

        public override void OnEnter()
        {
        }

        public TestNode WithUtility(float utility)
        {
            Utility = utility;
            return this;
        }
    }

    public static class Helper
    {
        public static void StartBehaviourTree(BehaviourTree tree)
        {
            tree.Start();
            tree.BeginTraversal();
        }

        public static BehaviourNode.Status StepBehaviourTree(BehaviourTree tree)
        {
            if (tree.IsRunning())
            {
                tree.Update();
            }

            return tree.LastStatus();
        }

        public static BehaviourNode.Status RunBehaviourTree(BehaviourTree tree)
        {
            StartBehaviourTree(tree);
            while (tree.IsRunning())
            {
                tree.Update();
            }

            return tree.LastStatus();
        }

        static public BehaviourTree CreateTree()
        {
            return new BehaviourTree();
        }

        static public T CreateNode<T>() where T : BehaviourNode, new()
        {
            return new T();
        }

        static public TestNode PassNode()
        {
            var node = CreateNode<TestNode>();
            node.ReturnStatus = BehaviourNode.Status.Success;
            return node;
        }

        static public TestNode FailNode()
        {
            var node = CreateNode<TestNode>();
            node.ReturnStatus = BehaviourNode.Status.Failure;
            return node;
        }
    }
}