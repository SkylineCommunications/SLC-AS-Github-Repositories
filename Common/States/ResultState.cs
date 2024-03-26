namespace Common.States
{
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class ResultState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public ResultState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.Result; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			return this;
		}
	}
}
