using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using BoardGame.Utilities;
using System.Media;
using SudokuGame.Pages;

namespace SudokuGame
{
    [AddINotifyPropertyChangedInterface]
    public class MainVM
    {

        private SoundPlayer soundPlayer = null;

        public ICommand MainMenuOptionsGameCommand { get; }
       
        public Page CurrentPage { get; set; }

        public bool IsMainPageVisible => CurrentPage == null;

        private readonly Action comebackPage;


        public MainVM()
        {
            comebackPage = () => 
            {
                CurrentPage = null; 
                soundPlayer.PlayLooping(); 
            };

            MainMenuOptionsGameCommand = new RelayCommand<MainMenuOption>(MainMenuOptionsGame);
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string soundFilePath = System.IO.Path.Combine("..", "..", "Sounds", "background-sound.wav");
            soundPlayer = new SoundPlayer(soundFilePath);
            Task.Run(() => soundPlayer.PlayLooping());
        }

        private void MainMenuOptionsGame(MainMenuOption menu)
        {
            switch (menu)
            {
                case MainMenuOption.StartGame:
                    {
                        soundPlayer.Stop();
                        CurrentPage = new GamePage() { DataContext = GamePageVM.GetInstance(comebackPage) };
                        break;
                    }
                case MainMenuOption.HallOfFame:
                    break;
                case MainMenuOption.Settings:
                    break;
                case MainMenuOption.About:
                    break;
                case MainMenuOption.Exit:
                    Application.Current.Shutdown();
                    break;
            }

        }

    }
}
