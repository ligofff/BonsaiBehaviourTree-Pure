
using System.Collections.Generic;
using System.Linq;
using Bonsai.Core;
using UnityEngine;

namespace Bonsai.Standard
{
  [BonsaiNode("Tasks/", "Interruptor")]
  public class Interruptor : Task
  {
    [Tooltip("If true, then the interruptable node return success else failure.")]
    public bool returnSuccess = false;

    public List<Interruptable> linkedInterruptables = new List<Interruptable>();

    public override Status Run()
    {
      for (int i = 0; i < linkedInterruptables.Count; ++i)
      {
        Status interruptionStatus = returnSuccess ? Status.Success : Status.Failure;
        linkedInterruptables[i].PerformInterruption(interruptionStatus);
      }

      return Status.Success;
    }

    public override BehaviourNode[] GetReferencedNodes()
    {
      return linkedInterruptables.ToArray();
    }
  }
}