using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttManager.Core
{
	//------------------------------------------------------------------------------
	/// \interface IMqttBrokerService
	/// \brief Interface to manage a local MQTT broker (start/stop, status)
	//------------------------------------------------------------------------------
	public interface IMqttBrokerService
	{
		/// \brief Start the MQTT broker with optional authentication
		Task StartAsync(int port = 1884, string? username = null, string? password = null);

		/// \brief Stop the MQTT broker
		Task StopAsync();

		/// \brief Indicates if the broker is currently running
		bool IsRunning { get; }

		/// \brief Event triggered when a client publishes a message (intercepted by broker)
		event EventHandler<string>? MessageIntercepted;
	}
}
