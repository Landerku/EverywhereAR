#if (UNITY_EDITOR) 
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;

namespace GraphProcessor
{
    public class GraphProcessor : BaseGraphProcessor
    {
        private List<BaseNode> _processList;
        public float Result { get; private set; }

        public GraphProcessor(BaseGraph graph) : base(graph)
        {
        }

        public override void UpdateComputeOrder()
        {
            _processList = graph.nodes.OrderBy(n => n.computeOrder).ToList();
        }

        public override void Run()
        {
            var count = _processList.Count;

            // すべてのノードを順番に処理する
            for (var i = 0; i < count; i++)
            {
                _processList[i].OnProcess();
            }

            JobHandle.ScheduleBatchedJobs();

            // Resultノードを取得する
        }
    }
}
#endif