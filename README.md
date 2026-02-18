# MqttManager

> A cross-platform MQTT broker management application demonstrating modern embedded architecture patterns

## Overview

**MqttManager** is a showcase application that demonstrates professional embedded system architecture through a cross-platform MQTT broker implementation. Built with .NET MAUI, this project exemplifies clean architecture principles, dependency injection, and the MVVM pattern in a real-world IoT communication context.

This application serves as a reference implementation for developers looking to understand how to structure cross-platform embedded applications with proper separation of concerns and testable code.

## 🎯 Key Features

- **Embedded MQTT Broker**: Run a full-featured MQTT broker directly on mobile and desktop platforms
- **Cross-Platform Support**: Single codebase targeting Android, iOS, macOS, and Windows
- **Real-Time Message Interception**: Monitor all MQTT messages passing through the broker
- **Client Connection Tracking**: Track client connect/disconnect events in real-time
- **Configurable Broker Settings**: Customize port, authentication, and other broker parameters
- **Clean Architecture**: Demonstrates proper layering and separation of concerns
- **Dependency Injection**: Fully leverages .NET's built-in DI container
- **MVVM Pattern**: Uses CommunityToolkit.Mvvm for reactive UI bindings

## 🏗️ Architecture

This project demonstrates **Clean Architecture** principles with clear separation of concerns:

```
MqttManager/
├── Core/                          # Domain layer - Business logic and interfaces
│   └── IMqttBrokerService.cs     # Abstraction for MQTT broker operations
│
├── Infrastructure/                # Infrastructure layer - External dependencies
│   └── MqttNetBrokerService.cs   # Concrete implementation using MQTTnet
│
├── UI/                           # Presentation layer - User interface
│   ├── ViewModels/               # MVVM ViewModels
│   │   └── MainViewModel.cs      # Main page business logic
│   ├── MainPage.xaml             # View definition
│   └── MainPage.xaml.cs          # View code-behind
│
└── MauiProgram.cs                # DI configuration and app bootstrap
```

### Design Patterns

- **Repository Pattern**: Abstraction of data access through interfaces
- **Dependency Injection**: Constructor injection for loose coupling
- **MVVM (Model-View-ViewModel)**: Clean separation between UI and business logic
- **Observer Pattern**: Event-driven architecture for broker notifications
- **Factory Pattern**: MQTTnet server instantiation with custom adapters

## 🛠️ Technology Stack

### Core Technologies
- **.NET 8.0**: Latest .NET platform for cross-platform development
- **.NET MAUI**: Multi-platform App UI framework
- **C# 12**: Modern C# with nullable reference types

### Key Libraries
- **MQTTnet 5.0**: Production-grade MQTT broker and client library
- **CommunityToolkit.Mvvm 8.4**: Modern MVVM helpers and source generators
- **Microsoft.Extensions.DependencyInjection**: Built-in DI container

### Supported Platforms
- **Android** (API 21+)
- **iOS** (11.0+)
- **macOS** (Catalyst 13.1+)
- **Windows** (10.0.17763.0+)

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (17.8+) or Visual Studio for Mac
- Platform-specific SDKs:
  - Android: Android SDK (API 21+)
  - iOS/macOS: Xcode 14+
  - Windows: Windows 10 SDK

### Building the Project

1. Clone the repository:
```bash
git clone https://github.com/Aurel2607/MqttManager.git
cd MqttManager
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build for your target platform:
```bash
# Android
dotnet build -f net8.0-android

# iOS
dotnet build -f net8.0-ios

# macOS
dotnet build -f net8.0-maccatalyst

# Windows
dotnet build -f net8.0-windows10.0.19041.0
```

### Running the Application

#### Visual Studio
1. Open `MqttManager.sln`
2. Select your target platform from the debug dropdown
3. Press F5 to build and run

#### Command Line
```bash
# Run on Android
dotnet build -t:Run -f net8.0-android

# Run on Windows
dotnet build -t:Run -f net8.0-windows10.0.19041.0
```

## 💡 Usage

### Starting the MQTT Broker

1. Launch the application
2. Configure the broker port (default: 1883)
3. Click **Start Broker** to launch the embedded MQTT broker
4. The broker status will update to "Running"

### Monitoring Messages

Once the broker is running:
- **Broker Intercepted Messages**: Displays all MQTT messages published through the broker
- **Broker Events**: Shows client connection/disconnection events with timestamps

### Testing the Broker

You can test the broker using any MQTT client:

```bash
# Using mosquitto_pub (install mosquitto-clients)
mosquitto_pub -h localhost -p 1883 -t "test/topic" -m "Hello MQTT!"

# Using MQTT.fx or any other MQTT client
# Connect to: localhost:1883
# Publish to any topic to see messages intercepted
```

## 📁 Project Structure

```
MqttManager/
├── MqttManager/                  # Main application project
│   ├── Core/                     # Business logic interfaces
│   ├── Infrastructure/           # External service implementations
│   ├── UI/                       # User interface layer
│   │   ├── ViewModels/           # MVVM ViewModels
│   │   ├── MainPage.xaml         # Main UI definition
│   │   └── MainPage.xaml.cs      # UI code-behind
│   ├── Platforms/                # Platform-specific code
│   │   ├── Android/
│   │   ├── iOS/
│   │   ├── MacCatalyst/
│   │   └── Windows/
│   ├── Resources/                # App resources (fonts, images, styles)
│   ├── App.xaml                  # Application definition
│   ├── AppShell.xaml             # Shell navigation structure
│   ├── MauiProgram.cs            # DI and app configuration
│   └── MqttManager.csproj        # Project file
└── MqttManager.sln               # Solution file
```

## 🧩 Architecture Highlights

### Dependency Injection Configuration

The application demonstrates proper DI setup in `MauiProgram.cs`:

```csharp
builder.Services.AddSingleton<IMqttBrokerService, MqttNetBrokerService>();
builder.Services.AddSingleton<MainViewModel>();
builder.Services.AddSingleton<MainPage>();
```

### Interface-Based Design

The `IMqttBrokerService` interface provides a clean contract:

```csharp
public interface IMqttBrokerService
{
    Task StartAsync(int port = 1883, string? username = null, string? password = null);
    Task StopAsync();
    bool IsRunning { get; }
    event EventHandler<string>? MessageIntercepted;
    event EventHandler<string>? BrokerEvent;
}
```

### MVVM Implementation

ViewModels use `CommunityToolkit.Mvvm` for clean, boilerplate-free reactive properties:

```csharp
[ObservableProperty]
private int brokerPort = 1883;

[RelayCommand]
private async Task StartBroker() { /* ... */ }
```

## 🎓 Learning Points

This project demonstrates:

1. **Clean Architecture**: Proper separation of concerns with Core, Infrastructure, and UI layers
2. **SOLID Principles**: Single Responsibility, Dependency Inversion, and Interface Segregation
3. **Cross-Platform Development**: Single codebase for multiple platforms
4. **Event-Driven Architecture**: Decoupled communication via events
5. **Modern C# Features**: Nullable reference types, async/await, source generators
6. **Production-Ready Patterns**: Logging, error handling, and resource management

## 🤝 Contributing

This is a showcase project, but contributions that improve the architecture demonstration are welcome:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/improvement`)
3. Commit your changes (`git commit -am 'Add architecture improvement'`)
4. Push to the branch (`git push origin feature/improvement`)
5. Open a Pull Request

## 📄 License

This project is open source and available for educational purposes.

## 🔗 Related Technologies

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [MQTTnet Library](https://github.com/dotnet/MQTTnet)
- [MQTT Protocol](https://mqtt.org/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/windows/communitytoolkit/mvvm/introduction)

## 📧 Contact

For questions about this architecture showcase, please open an issue on the repository.

---

**Note**: This project serves as an educational reference for embedded architecture patterns in cross-platform applications. It demonstrates professional software engineering practices applicable to IoT, mobile, and desktop development.
