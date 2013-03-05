using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Wätter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            SettingsPane.GetForCurrentView().CommandsRequested += SettingsCommandsRequested;
            DataContext = new MainViewModel();
            ((MainViewModel)DataContext).LoadData();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).LoadData();
            
        }

        private void PostleitzahlTextbox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var model = ((MainViewModel)DataContext);
            if (PostleitzahlTextbox.Text.Length==4)
            {
                try
                {
                    int newPlz = Int32.Parse(PostleitzahlTextbox.Text);
                        model.Plz = newPlz;
                        model.LoadData();
                }
                catch (Exception)
                {
                    model.SetErrorDescription("Postleitzahl darf nur aus Zahlen bestehen.");
                }
            }
            else
            {
                model.SetErrorDescription("Postleitzahl muss aus vier Zahlen bestehen.");
            }
           
        }

        private void SettingsCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var privacyStatement = new SettingsCommand("privacy", "Privacy Statement", x => Launcher.LaunchUriAsync(
                    new Uri("https://docs.google.com/document/pub?id=1-vYAN6x83MAT1bgbXPS3U2_ATbXCUN5_8QTmHNTyefI")));

            args.Request.ApplicationCommands.Clear();
            args.Request.ApplicationCommands.Add(privacyStatement);
        }
        
    }
}
