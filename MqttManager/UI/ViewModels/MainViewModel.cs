using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MqttManager.Core;
using System.Collections.ObjectModel;

namespace MqttManager.UI.ViewModels
{

	//------------------------------------------------------------------------------
	/// \class MainViewModel
	/// \brief Main ViewModel handling the broker control (start/stop)
	//------------------------------------------------------------------------------
	public partial class MainViewModel : ObservableObject
	{
		private readonly IMqttBrokerService _brokerService;

		public ObservableCollection<string> BrokerMessages { get; } = new();
		public ObservableCollection<string> BrokerEvents { get; } = new();

		[ObservableProperty]
		private int brokerPort = 1884;

		[ObservableProperty]
		private string? brokerUsername;

		[ObservableProperty]
		private string? brokerPassword;

		[ObservableProperty]
		private string brokerStatus = "Stopped";

		//------------------------------------------------------------------------------
		/// \brief Constructor injecting the broker service
		//------------------------------------------------------------------------------
		public MainViewModel(IMqttBrokerService brokerService)
		{
			_brokerService = brokerService;

			_brokerService.MessageIntercepted += (s, msg) =>
			{
				MainThread.BeginInvokeOnMainThread(() => BrokerMessages.Add(msg));
			};

			_brokerService.BrokerEvent += (s, evt) =>
			{
				MainThread.BeginInvokeOnMainThread(() => BrokerEvents.Add(evt));
			};
		}

		//------------------------------------------------------------------------------
		/// \brief Start the broker and update status
		//------------------------------------------------------------------------------
		[RelayCommand]
		private async Task StartBroker()
		{
			await _brokerService.StartAsync(BrokerPort, BrokerUsername, BrokerPassword);
			BrokerStatus = _brokerService.IsRunning ? "Running" : "Failed";
		}

		//------------------------------------------------------------------------------
		/// \brief Stop the broker and update status
		//------------------------------------------------------------------------------
		[RelayCommand]
		private async Task StopBroker()
		{
			await _brokerService.StopAsync();
			BrokerStatus = "Stopped";
		}




	}
}
