using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;


namespace LockScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            SetLockScreenDetails("Welcome to the Custom Lock Screen", "Press OK to unlock", "C:\\Users\\s3d\\Desktop\\LockScreenV2\\LockScreen\\LockScreen\\Reference\\logo.png");
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            PromptForShellChanges();
        }

        public void SetLockScreenDetails(string title, string instructions, string logoPath)
        {
            TitleLabel.Content = title;
            InstructionText.Text = instructions;

            if (!string.IsNullOrEmpty(logoPath))
            {
                LogoImage.Source = new BitmapImage(new Uri(logoPath, UriKind.RelativeOrAbsolute));
            }
        }


        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape || e.Key == Key.System)
        //    {
        //        e.Handled = false; // Block system key handling
        //    }
        //}


        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl + U is pressed
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.U)
            {
                // Logic to unlock the screen
                UnlockScreen();
            }
        }
        private void UnlockScreen()
        {
            MessageBox.Show("Screen Unlocked!");
            // Add code to close the lock screen or perform any unlock actions
            Application.Current.Shutdown();  // Example to close the lock screen
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.U))
            {
                // Allow closing the window
                e.Cancel = false;
            }
            else
            {
                // Prevent closing with other actions (e.g., Alt+F4)
                e.Cancel = true;
            }
        }

        private void PromptForShellChanges()
        {
            var result = MessageBox.Show("Would you like to set the custom Shell V2 for the Lock Screen? The system needs to restart for changes to take effect.",
                                         "Set Custom Shell V2", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Get the path where the current executable is stored
                    string exePath = AppDomain.CurrentDomain.BaseDirectory;

                    // Create the path for the PowerShell script
                    string scriptPath = System.IO.Path.Combine(exePath, "Lockscreen.ps1");

                    // Check if the script file exists
                    if (System.IO.File.Exists(scriptPath))
                    {
                        // Start the PowerShell script to set Shell V2
                        Process.Start("powershell.exe", $"-ExecutionPolicy Bypass -File \"{scriptPath}\"");

                        // Prompt the user to restart the computer
                        MessageBox.Show("The system needs to restart for changes to take effect.", "Restart Required", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Restart the system
                        Process.Start("shutdown", "/r /t 0");
                    }
                    else
                    {
                        MessageBox.Show("The specified script file does not exist. Please check the file path and try again.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to set Shell V2: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



    }
}
