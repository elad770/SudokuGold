# 🧩 SudokuGold

## Project Overview
**SudokuGold** is a Sudoku game application with six levels of difficulty, from "Easy" to "Impossible". This project, built in WPF with the MVVM pattern, showcases a way to implement a custom control as a UserControl, making it both reusable and adaptable within a larger interface. The project is divided into three main components:

- **Sudoku Board UserControl**: A reusable control that dynamically builds the Sudoku board based on the ViewModel.
- **Main Game Interface**: A primary WPF application that integrates Pages for navigation, each with its own ViewModel, including the game board component.
- **Difficulty and Validation Interfaces**: Special interfaces that generate board values according to difficulty and validate board configurations.

---

## 🛠️ Project Structure and Key Components

### 1. **Sudoku Board UserControl**
   - A standalone UserControl project that constructs a dynamic 9x9 Sudoku board.
   - Managed by a ViewModel that handles the board's logic, making it flexible and easy to integrate into other projects.
   - Requires an interface implementation to supply board values and validate board legality based on difficulty levels.

### 2. **Main Project**
   - Houses the main application window with a menu structure, navigating between Pages that support different functionalities.
   - Includes a dedicated page for the game, where the Sudoku board is integrated as a component.
   - Contains a JSON file with a database of 9x9 board configurations pre-defined for the six difficulty levels.

### 3. **Special Interfaces**
   - These interfaces provide board values dynamically according to the chosen difficulty.
   - Implement validation functions to ensure game legality. These functions:
      - Verify cell values based on Sudoku rules (row, column, and subgrid constraints).
      - Highlight mistakes in the board by flagging incorrect values in red, later resetting to blue when corrections are made.

---

## 🚀 Planned Features and Missing Functionality

This prototype is fully playable, but some features are still under development to enhance the gameplay experience. Here’s what’s coming soon:

- **🔄 Undo Operations**: Allows players to revert to previous moves to correct mistakes without starting over.
- **✏️ Pencil Mode**: Enables players to make temporary "pencil" notes for potential numbers in cells without committing them.
  
### Additional Pages:
   - **⚙️ Settings Page**: A customizable page for adjusting difficulty, themes, and other preferences.
   - **🏆 Hall of Fame**: A leaderboard tracking players who’ve completed numerous boards at various difficulty levels.
   - **ℹ️ About Page**: Game information, including version details, credits, and a brief gameplay description.

These features are prioritized for future updates to create a richer and more engaging game experience.

---

## 🎯 Purpose of the Project
Beyond creating a fun and challenging Sudoku game, this project serves as a demonstration of using **MVVM architecture** and **Pages** in a WPF application. It illustrates how to build a **custom UserControl** for a Sudoku board that is efficient, reusable, and easily integrated within a complex interface.

---

![image](https://github.com/elad770/SudokuGold/assets/73057751/8f103553-4b14-4774-a643-44270a02d4b6)
![image](https://github.com/elad770/SudokuGold/assets/73057751/7dcef590-e0ad-4cf0-af75-24ac7a48f2eb)
![image](https://github.com/elad770/SudokuGold/assets/73057751/c1732fe2-7219-4218-bf32-6f2a78f356f8)
![image](https://github.com/elad770/SudokuGold/assets/73057751/9f8cf214-deb7-49ed-a026-d61ea49d9481)
![WhatsApp Image 2023-08-28 at 12 57 09](https://github.com/elad770/SudokuGold/assets/73057751/8e1a050f-70ff-4ea2-a64d-0881897e3d03)
![WhatsApp Image 2023-08-28 at 00 27 04](https://github.com/elad770/SudokuGold/assets/73057751/6d0e0a01-2425-46a8-a962-7d20b7915042)

[![Video](https://github.com/elad770/SudokuGold/assets/73057751/6d56562f-337c-4f12-a2d9-56ec3ed88724.png)](https://github.com/elad770/SudokuGold/assets/73057751/6d56562f-337c-4f12-a2d9-56ec3ed88724)

https://github.com/user-attachments/assets/5248cafc-d668-458d-adc3-389739801b12
