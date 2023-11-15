namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private string? translatedNumber = string.Empty;

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (await DisplayAlert("Dial a Number", "Would you like to call " + translatedNumber + "?", "Yes", "No"))
            {
                try
                {
                    if (translatedNumber != null)
                    {
                        if (PhoneDialer.Default.IsSupported) PhoneDialer.Default.Open(translatedNumber);
                    }
                    else
                    {
                        CallButton.IsEnabled = false;
                    }
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
    }
}