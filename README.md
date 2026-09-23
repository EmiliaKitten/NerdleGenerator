# Nerdle puzzle generator

A small C# class that generates valid Nerdle puzzles for use in a Nerdle-style game.

## Asset attribution

The game asset used with this project was created by me and is available on BOOTH:

[https://msemilia.booth.pm/items/8874868](https://msemilia.booth.pm/items/8874868)

The asset page is also the official source for the asset and its usage terms:

## Requirements

- .NET 8.0 SDK

## How to use

Create a `NerdleGenerator`. A puzzle is generated automatically by the constructor. Call `getPuzzle()` to retrieve its tokens:

```csharp
using System.Collections;
using Nerdle;

NerdleGenerator generator = new NerdleGenerator();
ArrayList puzzle = generator.getPuzzle();

string expression = string.Join("", puzzle.ToArray());
Console.WriteLine(expression);
```

A generated puzzle is returned as an `ArrayList` containing the numbers, operators, equals sign, and result. The ArrayList is always of length 8. For example:

```text
12 + 3 = 15
```

The actual puzzle is random and contains no spaces when joined directly. Add spaces when displaying it in the game if desired:

```csharp
string displayExpression = string.Join(" ", puzzle.ToArray());
Console.WriteLine(displayExpression);
```

To generate a new puzzle with the same generator instance, call `Next()` and read the result again:

```csharp
generator.Next();
ArrayList nextPuzzle = generator.getPuzzle();
```

The generator creates eight-character equations using `+`, `-`, `*`, and `/`. Division puzzles only use whole-number results, and generated results are positive.
