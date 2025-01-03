﻿using BoardGame.VIewModels;
using System.Windows.Controls;


namespace SudokuGame.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    /// 
   // [AddINotifyPropertyChangedInterface]
    public partial class GamePage : Page
    {

        //private static GamePage instance;
        //private string currentdiff = ConfigurationManager.AppSettings["Level"];
        //private Dictionary<string, int> difficultyLevels;
        //private string WindowTitle => $"Sudoku Gold - {currentdiff}";
        //private Button previousButton;
        //UserControlBoardGame user;
        //private DispatcherTimer timer;
        //private DateTime startTime;
        //private TimeSpan elapsedTime;

        //public BoardViewModel BoradVM { get; set; }


        //public ObservableCollection<bool> Stars { get; set; }
        //public GameBoardProvider GameBoardProvider { get; set; }

        //public string PopupTitle { get; set; }
        //public string PopupContent { get; set; }

        //public static GamePage Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new GamePage();
        //        }
        //        else
        //        {

        //            //  instance.user.RefocusCell();
        //            instance.startTime = DateTime.Now - instance.elapsedTime;
        //            instance.timer.Start();
        //            ((MainWindow)Application.Current.MainWindow).Title = instance.WindowTitle;
        //        }

        //        return instance;
        //    }
        //}


        public GamePage(/*GamePageVM pagevm*/)
        {
            InitializeComponent();
            //DataContext = pagevm;

           // DataContext = GamePageVM.Instance;


            //Action actionAfterEndGame = () =>
            //{
            //    PopupTitle = "Congratulations!";
            //    difficultyLevels.TryGetValue(currentdiff, out var level);
            //    PopupContent = $"You managed to solve the puzzle on difficulty level {currentdiff}" + Environment.NewLine + "" +
            //    $"in time: {timerTextBlock.Text}";
            //    PopupHost.IsOpen = true;
            //    timer.Stop();
            //};

            //DifficultyLevel difficultyLevel = DifficultyLevel.Medium;
            //Enum.TryParse(currentdiff, true, out difficultyLevel);

            //GameBoardProvider = new GameBoardProvider()
            //{
            //    Level = difficultyLevel
            //};
            //GameBoardProvider.InitializeBoard();
            //BoradVM = new BoardViewModel(GameBoardProvider, actionAfterEndGame);

            //user = new UserControlBoardGame(BoradVM);


            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += Timer_Tick;
            //startTime = DateTime.Now;
            //timer.Start();

            //previousButton = stPanelDiff.Children.OfType<Button>().Where(but =>
            //but.Content.ToString() == currentdiff).FirstOrDefault();
            //previousButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0072e3"));
            //CreateButtonsNumbers();
            //FillDictionaryDiffLevels();

            //user.Margin = new Thickness(15, 60, 0, 0);
            //GridSecond.Children.Add(user);


            //((MainWindow)Application.Current.MainWindow).Title = WindowTitle;

            //DataContext = this;

        }


        //private void PopupOK_Click(object sender, RoutedEventArgs e)
        //{
        //    PopupHost.IsOpen = false;
        //    BoradVM.RestOrNewGame(false);
        //    //user.HandlerButtonOutsideGame(GameAction.New_Game, currentdiff);
        //    ResetTimer();
        //}


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

        //    Enum.TryParse(currentdiff, true, out DifficultyLevel level);
        //    GameBoardProvider.Level = level;

        //    //new game is defualt false
        //    BoradVM.RestOrNewGame(false);

        //    UpdateStars();
        //    ((MainWindow)Application.Current.MainWindow).Title = WindowTitle;
        //    ResetTimer();

        //}

        //private void CreateButtonsNumbers()
        //{
        //    StackPanel st = new StackPanel();

        //    for (int i = 1; i <= 9; i++)
        //    {
        //        Button button = new Button();
        //        button.Content = i.ToString();
        //        button.PreviewMouseDown += Button_PreviewMouseDown_Number;
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
        //    BoradVM.OptionalEraseOrReplaceNum(buttonContent);
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

        //private void Button_click_Option(object sender, MouseButtonEventArgs e)
        //{

        //    switch (((Button)sender).Name)
        //    {
              
        //        case "buUndo":
        //            break;
        //        case "buMenu":
        //            {
        //                timer.Stop();
        //                PopupHostMenu.IsOpen = true;
        //                break;
        //            }
        //        default:
        //            break;
        //    }
        //    e.Handled = true;

        //}


        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    elapsedTime = DateTime.Now - startTime;
        //    timerTextBlock.Text = "Time: " + elapsedTime.ToString(@"hh\:mm\:ss");
        //}

        //private void ResetTimer()
        //{
        //    timerTextBlock.Text = "Time: " + "00:00:00";
        //    startTime = DateTime.Now;
        //    timer.Start();
        //}

        //private void Menu_Button(object sender, MouseButtonEventArgs e)
        //{
        //    switch (((Button)sender).Name)
        //    {

        //        case "buNext":
        //            {
        //                PopupHostMenu.IsOpen = false;
        //                startTime = DateTime.Now - elapsedTime;
        //                timer.Start();
        //                break;
        //            }
        //        case "buSettings":
        //            break;

        //        case "buBack":
        //            {
        //                PopupHostMenu.IsOpen = false;
        //                //  user.LostFocusCell();
        //                ((MainWindow)Application.Current.MainWindow).BackMainWinodw();
        //                break;
        //            }
        //        case "buExit":
        //            Application.Current.Shutdown();
        //            break;

        //    }

        //}
    }
}
