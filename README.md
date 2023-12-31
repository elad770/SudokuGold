# SudokuGold

Project SudokuGold is a Sudoku game with 6 levels of difficulty, from the easiest level to an impossible level.
The game project is built as a WPF GUI, the project itself is actually divided into 3 parts.

- The sudoku board itself is a separate UserControl project intended for reuse.
- Main project, which contains a main window and a page on which the board of the game sits as a component.
- Special interfaces that require an implementation that will provide board values according to difficulty level and receiving board values of a two-dimensional array of size 9x9, and in addition to implement validation functions for the board itself.

The sudoku board itself is a UserControl project that dynamically builds the board of the game with code from behind, in addition this project requires a uniform interface to provide the values of the board by level of difficulty and realization of legality of the board.

The main project contains a main window as a menu for the game and a page containing the board itself and more buttons and controls intended for the game itself and the board.
The main project contains a class that implements the interfaces and also contains an ordered json file with a number of 9x9 panel values according to the six levels that are defined in the game itself.

To add the board of the game, you must implement a special interface, the interface itself requires you to create 3 functions in total, one must return a valid two-dimensional array of numbers according to difficulty level, the other two functions require you to perform validations according to the legality of the game, one must return If the value entered into any cell in the table is correct, that means checking according to the legality of the game, according to row/column and submatrix in which the cell is located.
The second function requires returning a list of row and column indexes, if values in a row or column or in a sub-matrix are colored red due to a mistake made by the user during the game, after the function returns such a list the board itself updates the colors of the values from red to blue.

The project uses the Material Design library for rich design, in addition it uses the Fody library which facilitates the definition of class properties and variables in the context of the NotifyChanged event.
The sounds, icons and images in the game itself were downloaded from free websites.


![image](https://github.com/elad770/SudokuGold/assets/73057751/8f103553-4b14-4774-a643-44270a02d4b6)
![image](https://github.com/elad770/SudokuGold/assets/73057751/7dcef590-e0ad-4cf0-af75-24ac7a48f2eb)
![image](https://github.com/elad770/SudokuGold/assets/73057751/c1732fe2-7219-4218-bf32-6f2a78f356f8)
![image](https://github.com/elad770/SudokuGold/assets/73057751/9f8cf214-deb7-49ed-a026-d61ea49d9481)
![WhatsApp Image 2023-08-28 at 12 57 09](https://github.com/elad770/SudokuGold/assets/73057751/8e1a050f-70ff-4ea2-a64d-0881897e3d03)
![WhatsApp Image 2023-08-28 at 00 27 04](https://github.com/elad770/SudokuGold/assets/73057751/6d0e0a01-2425-46a8-a962-7d20b7915042)

[![Video](https://github.com/elad770/SudokuGold/assets/73057751/6d56562f-337c-4f12-a2d9-56ec3ed88724.png)](https://github.com/elad770/SudokuGold/assets/73057751/6d56562f-337c-4f12-a2d9-56ec3ed88724)



