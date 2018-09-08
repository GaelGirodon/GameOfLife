# _Conway's Game of Life_

A simple implementation of the cellular automaton _Conway's Game of Life_
made with .NET & [MonoGame](https://github.com/MonoGame/MonoGame).

## QuickStart

- Clone the repository or download it as a ZIP
- Restore NuGet packages
- Build and run the program

## Examples of patterns

Some [presets](GameOfLife/Components/AutomatonPreset.cs) are built-in
to test the cellular automaton with well-known examples.

The initial state can be customized by changing the `preset` argument
of the `GameOfLifeAutomaton` constructor in [`GameOfLifeGame.cs`](GameOfLife/GameOfLifeGame.cs) (line `54`), e.g.:

```csharp
_automaton = new GameOfLifeAutomaton(ScreenSize.Scale(0.5f), 160, 110, 8,
    Color.DodgerBlue, AutomatonPreset.RPentomino, 50, 2500);
```

## Controls

Two keyboard controls are implemented:

- <kbd>+</kbd> (`Add`): speed up the simulation
- <kbd>-</kbd> (`Subtract`): slow down the simulation

## License

This project is licensed under the GNU General Public License.
