# ConsoleProjectTemplate

A reusable .NET 10 console application template with a menu/submenu navigation system and a `ConsoleHelpers` library for formatted output and validated user input.

---

## Solution Structure

```
ConsoleAppProject/      # Runnable console application
ConsoleHelpers/         # Reusable helpers library
TestConsoleHelpers/     # xUnit tests for ConsoleHelpers
Directory.Build.props   # Centralized NuGet package versions
```

---

## ConsoleAppProject

### Startup (`Program.cs`)

The entry point builds a .NET Generic Host with:

- **Serilog** for structured logging (opt-in via environment variables)
- **`appsettings.json`** + environment-specific overrides + user secrets + environment variables for configuration
- **Dependency Injection** registering `Application` as a transient service

Logging is controlled by two environment variables before the host is built:

| Variable | Values | Effect |
|---|---|---|
| `LOG_TO_CONSOLE` | `true` / `false` | Adds a Serilog console sink |
| `LOG_TO_FILE` | `true` / `false` | Writes logs to `C:\Logs\` |

The active environment is read from `DOTNET_ENVIRONMENT`. A startup banner is printed showing the current environment, logging state, and log file path before the application loop starts.

### Application (`Application.cs`)

`Application` is the top-level orchestrator. It is constructed with `IConfiguration` and `ILogger<Application>` via DI. Its `DoWork()` method launches the `MainMenu` and returns control when the user exits.

`Application.LINE_LENGTH` (default `40`) is the shared line-width constant used by menus and output formatting throughout the project.

---

## Menu System

### How It Works

The menu system is built on three layers:

```
IAsyncDemo (interface)
    └── BaseMenu (abstract class)
            ├── MainMenu
            └── ConsoleHelpersDemonstrationsMenu
```

#### `IAsyncDemo`

Defines the contract every menu must implement:

```csharp
List<string> MenuOptions();           // returns the list of option labels
Task ShowAsync(string title);         // runs the menu loop
Task<bool> HandleMenuChoiceAsync(int choice); // executes the selected option
```

#### `BaseMenu`

The shared base class providing the reusable `ShowAsync` loop. It:

1. Clears the screen
2. Builds the formatted menu string via `MenuGenerator`
3. Calls `InputHelpers.GetInputAsInt` with `min: 1`, `max: MenuOptions().Count`, and `confirm: true` to get a validated, confirmed selection
4. Passes the choice to `HandleMenuChoiceAsync`
5. Repeats until `HandleMenuChoiceAsync` returns `false`

`ShowAsync` is **not** virtual — subclasses cannot override the loop, only the options and logic.

#### `MenuGenerator`

A static helper that builds the formatted menu string:

```
****************************************
Main Menu
****************************************
* Please select an operation
----------------------------------------
1] Console Helpers Demo
2] Exit
****************************************
```

Accepts either a `string[]` or `List<string>` for menu options.

#### Adding a New Menu

1. Create a class that extends `BaseMenu`
2. Override `MenuOptions()` to return the list of option labels as strings
3. Override `HandleMenuChoiceAsync(int choice)` with a `switch` for each option; return `true` to stay in the menu, `false` to exit
4. In the parent menu's `HandleMenuChoiceAsync`, add a new `case` that instantiates and calls `ShowAsync` on the new menu

Example skeleton:

```csharp
public class MyMenu : BaseMenu
{
    public override List<string> MenuOptions() => new List<string>
    {
        "Do Something",
        "Exit"
    };

    public override async Task<bool> HandleMenuChoiceAsync(int choice)
    {
        switch (choice)
        {
            case 1:
                // do work
                break;
            default:
                return false;
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return true;
    }
}
```

### Current Menu Tree

```
MainMenu
└── ConsoleHelpersDemonstrationsMenu
        ├── Show Formatted Messages   → DemonstrateConsoleHelpers.ShowFormattedMessages()
        └── Show Input Helpers        → DemonstrateConsoleHelpers.ShowInputHelpers()
```

---

## ConsoleHelpers Library

A separate class library (`ConsoleHelpers.csproj`) with no external dependencies beyond the .NET runtime. It provides two static helper classes.

### `InputHelpers`

All methods loop until the user provides valid input, printing error guidance on each invalid attempt.

| Method | Description |
|---|---|
| `GetInputAsDouble(prompt, min, max, confirm)` | Reads a `double` within `[min, max]`. Re-prompts on non-numeric or out-of-range input. |
| `GetInputAsInt(prompt, min, max, confirm)` | Reads an `int` within `[min, max]`. Re-prompts on non-integer or out-of-range input. |
| `GetInputAsBool(prompt, confirm)` | Reads a Y/N response (case-insensitive, matches any string starting with `y` or `n`). Returns `true` for Y. |
| `GetInputAsString(prompt, confirm, allowEmpty)` | Reads a string. When `allowEmpty: false`, re-prompts on empty/whitespace. |
| `WaitForUserInput()` | Prints "Press any key to continue..." and calls `Console.ReadKey()`. |

All four reading methods accept an optional `confirm` parameter. When `true`, the user is shown their input and asked to confirm via `GetInputAsBool` before the value is accepted.

### `OutputHelpers`

All methods return a formatted `string` (they do not write to the console directly).

| Method | Description |
|---|---|
| `BoxedMessage(message, borderChar, lineLength)` | Wraps a single message in a box. `*` borders use `*` as side indicators; any other char uses `\|`. |
| `BoxedMessageWithTitle(title, message, lineLength)` | Renders a title box followed by a dashed inner box containing the message. |
| `BoxedArrayWithTitle(title, items[], lineLength)` | Renders a title box followed by each array item in its own row, separated by dashed lines. |
| `BoxedList(items, borderChar, lineLength)` | Renders a list of strings inside a bordered box, each item on its own line. |
| `BoxedListWithTitle(title, items, lineLength)` | Renders a title box followed by each list item separated by dashed lines. |

All methods default to `lineLength: 80`. Example output for `BoxedMessage("Hello", '*')`:

```
********************************************************************************
* Hello                                                                        *
********************************************************************************
```

---

## Tests (`TestConsoleHelpers`)

xUnit tests using **Shouldly** assertions cover both helper classes.

- **`TestInputHelpers`** — uses `Console.SetIn` to inject simulated input sequences, testing valid input, invalid-then-valid retry flows, boundary values, and confirm behavior.
- **`TestOutputHelpers`** — tests all five `OutputHelpers` methods for correct border characters, line lengths, content presence, separator placement, and custom `lineLength` support.

Run all tests:

```bash
dotnet test TestConsoleHelpers
```
