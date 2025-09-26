using MqttManager.UI.ViewModels;

namespace MqttManager
{
    public partial class MainPage : ContentPage
    {
		//------------------------------------------------------------------------------
		/// \brief Constructor injecting the ViewModel
		//------------------------------------------------------------------------------
		public MainPage(MainViewModel vm)
        {
			InitializeComponent();
			BindingContext = vm;
		}

    }

}
