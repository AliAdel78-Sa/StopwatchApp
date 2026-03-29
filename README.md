# Stopwatcher ⏱️

A lightweight command-line stopwatch built in C# with persistent state — pick up right where you left off, even after closing the app.

## Features

- Start, pause, and resume with accurate elapsed time tracking
- Persists state to a local JSON file on exit
- Restores your session automatically on next launch
- Minimal flicker-free terminal UI

## Controls

| Key | Action         |
| --- | -------------- |
| `S` | Start / Resume |
| `P` | Pause          |
| `R` | Reset          |
| `Q` | Save & Quit    |

## Requirements

- [.NET 10.0](https://dotnet.microsoft.com/download)

## Running

```bash
dotnet run
```

## Project Structure

```
Stopwatcher/
  models/     → Data models and JSON config
  services/   → Stopwatch logic and threading
  utils/      → Helpers and constants
  Program.cs  → Entry point
```

## Built With

- C# / .NET 10
- `System.Text.Json` for persistence
- Multi-threading with `volatile` shared state
