using BoardGame.Utilities;
using BoardGame.VIewModels;
using PropertyChanged;
using SudokuGame.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SudokuGame
{
    [AddINotifyPropertyChangedInterface]
    public class GamePageVM
    {
        private static GamePageVM instance;
        private string currentDiff = ConfigurationManager.AppSettings["Level"];
        private DispatcherTimer timer;
        private DateTime startTime;
        private TimeSpan elapsedTime;
        private string windowTitle;



        private GameBoardProvider gameBoardProvider;

        private Dictionary<string, int> difficultyLevels;


        public ICommand MenuGameCommand { get; }

        public ICommand RestOrNewGameCommand { get; }

        public ICommand ButtonAddNumToBoardCommand { get; }

        public ICommand DifficultyOfGameCommand { get; }

        public ObservableCollection<bool> Stars { get; set; }

        public BoardViewModel BoradVM { get; set; }


        public DifficultyLevel Level { get; set; }


        public UserControlBoardGame UserBoardGame { get; set; }

       
        public string PopupTitle { get; set; }
        public string PopupContent { get; set; }
        public string TimerText { get; private set; }

        public static GamePageVM Instance
        {
            get
            {
                if (instance == null)
                    instance = new GamePageVM();
                return instance;
            }
        }

        public GamePageVM()
        {
            InitializeGame();
            MenuGameCommand = new RelayCommand(MenuGame);
            RestOrNewGameCommand = new RelayCommand(RestOrNewGame);
            ButtonAddNumToBoardCommand = new RelayCommand((object num) => { BoradVM.OptionalActions(GameAction.Number_Optional_Or_Erase, num.ToString()); });
            DifficultyOfGameCommand = new RelayCommand((object obj) => {
                Enum.TryParse(obj.ToString(), true, out DifficultyLevel difficulty);
                gameBoardProvider.Level = Level = difficulty;
                BoradVM.ClearOrNewGame(false);
                currentDiff = difficulty.ToString();
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                config.AppSettings.Settings["Level"].Value = currentDiff;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                UpdateStars();
            });


        }

        private void MenuGame(object obj)
        {
            if(obj == null)
            {

                return;
            }
            Enum.TryParse(obj.ToString(), true, out GameAction action);
            BoradVM.OptionalActions(action);
        }




        private void InitializeGame()
        {
            Action actionAfterEndGame = () =>
            {
                PopupTitle = "Congratulations!";
                difficultyLevels.TryGetValue(currentDiff, out var level);
               // PopupContent = $"You managed to solve the puzzle on difficulty level {currentDiff}" + Environment.NewLine + "" +
                //$"in time: {timerTextBlock.Text}";
               // PopupHost.IsOpen = true;
                timer.Stop();
            };

            DifficultyLevel difficultyLevel = DifficultyLevel.Medium;
            Enum.TryParse(currentDiff, true, out difficultyLevel);
            Level = difficultyLevel;
            gameBoardProvider = new GameBoardProvider()
            {
                Level = difficultyLevel
            };
            BoradVM = new BoardViewModel(gameBoardProvider, actionAfterEndGame);

            UserBoardGame = new UserControlBoardGame(BoradVM);

           

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => UpdateElapsedTime();
            startTime = DateTime.Now;
            timer.Start();

            FillDictionaryDiffLevels();

            UpdateStars();
        }



        public void RestOrNewGame(object obj)
        {
            try
            {
                BoradVM.ClearOrNewGame(bool.Parse(obj.ToString()));
            }
            catch { }
        }


        private void InitializeTimer()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, e) => UpdateElapsedTime();
            startTime = DateTime.Now;
            timer.Start();
        }

        private void UpdateElapsedTime()
        {
            elapsedTime = DateTime.Now - startTime;
            TimerText = $"Time: {elapsedTime:hh\\:mm\\:ss}";
           // OnPropertyChanged(nameof(TimerText));
        }

        private void FillDictionaryDiffLevels()
        {
            difficultyLevels = new Dictionary<string, int>
            {
                { "Impossible", 6 },
                { "Extreme", 5 },
                { "Hard", 4 },
                { "Medium", 3 },
                { "Easy", 2 },
                { "VeryEasy", 1 }
            };

            difficultyLevels.TryGetValue(currentDiff, out var level);
            int filledStars = level;
            Stars = new ObservableCollection<bool>();
            for (int i = 0; i < difficultyLevels.Keys.Count; i++)
            {
                Stars.Add(i < filledStars);
            }

        }

        private void UpdateStars()
        {
            difficultyLevels.TryGetValue(currentDiff, out var level);
            int filledStars = level;
            for (int i = 0; i < Stars.Count; i++)
            {
                Stars[i] = (i < filledStars);
            }
        }

        private void UpdateWindowTitle()
        {
            windowTitle = $"Sudoku Gold - {currentDiff}";
            ((MainWindow)Application.Current.MainWindow).Title = windowTitle;
        }



    }
}
