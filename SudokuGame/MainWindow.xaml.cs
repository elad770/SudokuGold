using BoardGame.Interfaces;
using BoardGame.Resources;
using SudokuGame.BoardProvider;
using SudokuGame.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
using System.Configuration;
using BoardGame.Utilities;
using MaterialDesignThemes.Wpf;
using SudokuGame.Pages;
using System.Media;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;

namespace SudokuGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public bool IsButtonVisible { get; set; } = true;
        SoundPlayer soundPlayer = null;
        private GamePage gamePage;

        public MainWindow()
        {
            InitializeComponent();
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string soundFilePath = System.IO.Path.Combine("..", "..", "Sounds", "background-sound.wav");
            soundPlayer = new SoundPlayer(soundFilePath);
            Task.Run(() => soundPlayer.PlayLooping());

        }


        public void BackMainWinodw()
        {
            Task.Run(() => soundPlayer.PlayLooping());
            spMainButtons.Visibility = Visibility.Visible;
            this.Title = "Sudoku Gold";
            //if (contentFrame.Content is IDisposable disposablePage)
            //{
            //    disposablePage.Dispose();
            //}
            contentFrame.Content = null;
        }


        //private dynamic CreateControlToPopup(Type controlType, Thickness margin, double fontSize = 15, Visibility visible = Visibility.Visible)
        //{
        //    dynamic control = Activator.CreateInstance(controlType);
        //    control.Margin = margin;
        //    control.FontSize = fontSize;
        //    control.Foreground = Brushes.Azure;
        //    control.Visibility = visible;
        //    return control;
        //}


        private void RunGame()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {

                // 

                soundPlayer.Stop();
                contentFrame.Navigate(GamePage.Instance);
                spMainButtons.Visibility = Visibility.Hidden;
                IsButtonVisible = false;
                Mouse.OverrideCursor = Cursors.Wait;
                Task.Delay(200);
                Mouse.OverrideCursor = null;
            });


            //var stackPanelMain = new StackPanel()
            //{
            //    Width = 430,
            //    Height = 140,
            //};

            //var textBlock = (TextBlock)CreateControlToPopup(typeof(TextBlock), new Thickness(0, 10, 0, 10));
            //textBlock.Text = "Account wallet";
            //textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            //var textBox = (TextBox)CreateControlToPopup(typeof(TextBox), new Thickness(10, 0, 10, 20));
            //string key = "Account";
            //textBox.Text = ConfigurationManager.AppSettings[key];

            //stackPanelMain.Children.Add(textBlock);
            //stackPanelMain.Children.Add(textBox);
            //var textBlockError = (TextBlock)CreateControlToPopup(typeof(TextBlock), new Thickness(10, 0, 0, 3), 12, Visibility.Collapsed);

            //stackPanelMain.Children.Add(textBlockError);

            //var stackPanelSecond = new StackPanel()
            //{
            //    Orientation = Orientation.Horizontal,
            //    HorizontalAlignment = HorizontalAlignment.Center,
            //    VerticalAlignment = VerticalAlignment.Center,
            //};


            //Action<string> displayMessageError = (string textMessage) =>
            //{
            //    textBox.Margin = new Thickness(10, 0, 10, 10);
            //    textBlockError.Visibility = Visibility.Visible;
            //    textBlockError.Text = "*" + textMessage;
            //};

            //Action clickOk = async () =>
            //{
            //    if (!string.IsNullOrWhiteSpace(textBox.Text))
            //    {
            //        // Validate the text box value
            //        string walletAddress = textBox.Text.Trim();

            //        string pattern = @"^(0x)?[0-9a-fA-F]{40}$";

            //        if (Regex.IsMatch(walletAddress, pattern))
            //        {


            //            // save account in program
            //            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            //            config.AppSettings.Settings[key].Value = walletAddress;
            //            config.Save(ConfigurationSaveMode.Modified);
            //            ConfigurationManager.RefreshSection("appSettings");
            //            Mouse.OverrideCursor = Cursors.Wait;
            //            stackPanelMain.IsEnabled = false;


            //            var requestData = new
            //            {
            //                account = walletAddress,
            //                amount = config.AppSettings.Settings["AmountStart"].Value
            //            };
            //            string json = System.Text.Json.JsonSerializer.Serialize(requestData, new JsonSerializerOptions
            //            {
            //                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Optional: Use camel case for property names
            //            });

            //            // Send request to the server
            //            HttpClient httpClient = new HttpClient();

            //            HttpResponseMessage response = await httpClient.PostAsync("https://sodukusmartcontractsce.onrender.com/mint", new StringContent(json, Encoding.UTF8, "application/json"));

            //            // Process the response
            //            if (response.IsSuccessStatusCode)
            //            {


            //                Application.Current.Dispatcher.Invoke(() =>
            //                {

            //                    // await Task.Delay(500);
            //                    DialogHost.CloseDialogCommand.Execute(true, null);
            //                    soundPlayer.Stop();
            //                    contentFrame.Navigate(GamePage.Instance);
            //                    IsButtonVisible = false;
            //                    //  Mouse.OverrideCursor = null;
            //                });
            //                stackPanelMain.IsEnabled = true;
            //                spMainButtons.Visibility = Visibility.Collapsed;
            //            }

            //            else if (response.StatusCode == HttpStatusCode.Forbidden)
            //            {
            //                string content = await response.Content.ReadAsStringAsync();
            //                displayMessageError(content);

            //            }
            //            else
            //            {

            //                displayMessageError("Login error try later");
            //                // Handle status code 403 - Only the manager can call this function

            //            }

            //            Mouse.OverrideCursor = null;
            //            stackPanelMain.IsEnabled = true;

            //        }
            //        else
            //        {
            //            displayMessageError("Please enter a valid ETH wallet address.");
            //        }
            //    }

            //    else
            //    {
            //        displayMessageError("Please enter your account.");
            //    }
            //};

            //var okButton = new Button
            //{
            //    Content = "OK",
            //    Command = new RelayCommand(clickOk)
            //};

            //var cancelButton = new Button
            //{
            //    Content = "Cancel",
            //    Command = new RelayCommand(() =>
            //    {
            //        DialogHost.CloseDialogCommand.Execute(true, null);
            //    })
            //};

            //stackPanelSecond.Children.Add(okButton);
            //stackPanelSecond.Children.Add(cancelButton);
            //stackPanelMain.Children.Add(stackPanelSecond);

            //CustomMaterialDesignPopup cusMat = new CustomMaterialDesignPopup(stackPanelMain);
            //await DialogHost.Show(cusMat);

        }


        private void Main_Menu_Options(object sender, RoutedEventArgs e)
        {

            // var viewModel = new CustomMaterialDesignPopupViewModel();

            switch ((sender as Button).Name)
            {
                case "buPlayGame":
                    {
                        RunGame();
                        break;
                    }
                case "buHall":
                    break;
                case "buSettings":
                    break;
                case "buExit":
                    Application.Current.Shutdown();
                    break;
            }

        }


        private void GameWindow_Closed(object sender, EventArgs e)
        {

            //gameWindow.Dispose(); // Dispose the game window to clean up resources
            this.Visibility = Visibility.Visible;
            this.spMainButtons.Visibility = Visibility.Visible;

        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    GamePage.Instance.ExitGame();
        //}
    }



    //public ObservableCollection<bool> Stars { get; set; }
    //private string currentdiff = ConfigurationManager.AppSettings["Level"];
    //private Dictionary<string, int> difficultyLevels;
    //private Button previousButton;
    //UserControlBoardGame user;

    //  public MainWindow()
    //   {

    //   InitializeComponent();

    //user = new UserControlBoardGame(new GameBoardProvider(), currentdiff);
    //previousButton = stPanelDiff.Children.OfType<Button>().Where(but =>
    //but.Content.ToString() == currentdiff).FirstOrDefault();
    //previousButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0072e3"));
    //CreateButtonsNumbers();
    ////currentdiff = boardLevels[new Random().Next(0, boardLevels.Length - 1)];
    ////UpdateStars();
    ////viewModel.Board = currentdiff;

    //// ResourceDictionary externalResources = new ResourceDictionary();
    //FillDictionaryDiffLevels();

    //user.Margin = new Thickness(15, 60, 0, 0);
    //GridSecond.Children.Add(user);
    //DataContext = this;
    //  }

    //private void Click_Change_Difficulty(object sender, RoutedEventArgs e)
    //{
    //    if (previousButton != null)
    //    {
    //        previousButton.Foreground = Brushes.Black; // Set the previous button's color back to black
    //    }

    //    Button butCurrent = (sender as Button);
    //    butCurrent.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0072e3"));
    //    previousButton = butCurrent;
    //    currentdiff = butCurrent.Content.ToString();
    //    Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
    //    config.AppSettings.Settings["Level"].Value = currentdiff;
    //    config.Save(ConfigurationSaveMode.Modified);
    //    ConfigurationManager.RefreshSection("appSettings");
    //    user.HandlerButtonOutsideGame(GameAction.New_Game, currentdiff);
    //    UpdateStars();
    //    //  ((MainWindow)Application.Current.MainWindow).Title = WindowTitle;
    //}


    //private void CreateButtonsNumbers()
    //{
    //    StackPanel st = new StackPanel();

    //    for (int i = 1; i <= 9; i++)
    //    {
    //        Button button = new Button();
    //        button.Content = i.ToString();
    //        //  button.Click += Button_Click_Number;
    //        button.PreviewMouseDown += Button_PreviewMouseDown_Number;
    //        //button.FontFamily = (FontFamily)Application.Current.Resources["CustomFont"];
    //        //button.Name = $"button_{i}";
    //        //  button.FontFamily = new FontFamily("Copperplate Gothic Light");
    //        st.Children.Add(button);
    //        if (i % 3 == 0)
    //        {
    //            stPanelNumbers.Children.Add(st);
    //            st = new StackPanel();
    //        }
    //    }
    //}

    //private void Button_PreviewMouseDown_Number(object sender, MouseButtonEventArgs e)
    //{
    //    string buttonContent = ((Button)sender).Content.ToString();
    //    user.HandlerButtonOutsideGame(GameAction.Number_Optional, buttonContent);
    //    e.Handled = true;
    //}


    //private void FillDictionaryDiffLevels()
    //{
    //    difficultyLevels = new Dictionary<string, int>
    //    {
    //        { "Impossible", 6 },
    //        { "Extreme", 5 },
    //        { "Hard", 4 },
    //        { "Medium", 3},
    //        { "Easy", 2},
    //        { "Very Easy", 1 }
    //    };

    //    difficultyLevels.TryGetValue(currentdiff, out var level);
    //    int filledStars = level;
    //    Stars = new ObservableCollection<bool>();
    //    for (int i = 0; i < difficultyLevels.Keys.Count; i++)
    //    {
    //        Stars.Add(i < filledStars);
    //    }

    //}

    //private void UpdateStars()
    //{
    //    difficultyLevels.TryGetValue(currentdiff, out var level);
    //    int filledStars = level;
    //    for (int i = 0; i < Stars.Count; i++)
    //    {
    //        Stars[i] = (i < filledStars);
    //    }
    //}


    //private void Button_click_Option(object sender, RoutedEventArgs e)
    //{

    //    switch (((Button)sender).Name)
    //    {
    //        case "buNewGame":
    //            user.HandlerButtonOutsideGame(GameAction.New_Game, currentdiff);
    //            break;
    //        case "buResetGame":
    //            user.HandlerButtonOutsideGame(GameAction.Reset_Game);
    //            //    ResetGame();
    //            break;
    //        case "buErase":
    //            break;
    //        case "buUndo":
    //            break;
    //        default:
    //            break;
    //    }


    //}


    // }
}
