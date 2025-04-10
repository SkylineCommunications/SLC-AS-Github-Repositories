// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Components
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class LabelWithValue<T> : Label
	{
		private Option<T> value;

		public LabelWithValue(Option<T> option) : base(option.Name)
		{
			value = option;
		}

		public new string Text { get => value.Name; }

		public T Value { get => value.Value; }

		public void Update(Option<T> option)
		{
			value = option;
			base.Text = value.Name;
		}
	}
}
