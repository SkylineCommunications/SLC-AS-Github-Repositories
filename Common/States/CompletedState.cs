namespace Common.States
{
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class CompletedState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public CompletedState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public IAddWorkflowState Transition(Statuses status)
		{
			return this;
		}
	}
}
