namespace Common.States
{
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class ResultState : IAddWorkflowState
	{
		public ResultState(DomHelper helper, AddWorkflowInstance instance)
		{
		}

		public Statuses Status { get => Statuses.Result; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			return this;
		}
	}
}
