using MqttManager.Core;
using MQTTnet;
using MQTTnet.Diagnostics.Logger;
using MQTTnet.Server;
using MQTTnet.Server.Internal.Adapter;

namespace MqttManager.Infrastructure
{

	//------------------------------------------------------------------------------
	/// \class MqttNetBrokerService
	/// \brief MQTT broker implementation using MQTTnet
	//------------------------------------------------------------------------------
	public class MqttNetBrokerService : IMqttBrokerService
	{
		private MqttServer? _server;
		public bool IsRunning { get; private set; } = false;
		
		/// <summary>
		/// Event raised whenever a message is intercepted by the broker
		/// </summary>
		public event EventHandler<string>? MessageIntercepted;

		//------------------------------------------------------------------------------
		/// \brief Start the broker on the given port (default 1884)
		//------------------------------------------------------------------------------
		public async Task StartAsync(int port = 1884, string? username = null, string? password = null)
		{
			if (IsRunning)
				return;

			var options = new MqttServerOptionsBuilder()
				.WithDefaultEndpoint()
				.WithDefaultEndpointPort(port)
				.Build();

			// Create adapters (needed in v5)
			new MqttTcpServerAdapter(); // basic TCP adapter

			// Remove the incorrect constructor usage and use the parameterless constructor instead
			var adapters = new List<IMqttServerAdapter>
			{
				new MqttTcpServerAdapter() // basic TCP adapter
            };

			// Logger (can be null, here we use a minimal one)
			var logger = new MqttNetEventLogger();

			_server = new MqttServer(options, adapters, logger);

			// Intercept messages
			_server.InterceptingPublishAsync += args =>
			{
				var topic = args.ApplicationMessage.Topic;
				var payload = args.ApplicationMessage.ConvertPayloadToString();
				var message = $"[{topic}] {payload}";

				// Fire event for ViewModel
				MessageIntercepted?.Invoke(this, message);

				return Task.CompletedTask;
			};


			await _server.StartAsync();
			IsRunning = true;
		}


		//------------------------------------------------------------------------------
		/// \brief Stop the broker if it is running
		//------------------------------------------------------------------------------
		public async Task StopAsync()
		{
			if (_server != null && IsRunning)
			{
				await _server.StopAsync();
				_server.Dispose();
				_server = null;
				IsRunning = false;
			}
		}
	}
}
