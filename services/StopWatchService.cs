using Stopwatcher.models;
using Stopwatcher.utils;

namespace Stopwatcher.services {
  static class StopWatchService {

    // Fields
    static readonly StopWatchModel stopWatchData = Helpers.RetrieveStopWatchData();
    static volatile string sharedKey = stopWatchData.Status ? "S" : "P";

    // Main
    public static void Run() {
      Thread t1 = new(StopWatchInterfaceThread);
      Thread t2 = new(InputInterfaceThread);
      t1.Start();
      t2.Start();
    }

    // Threads
    static void StopWatchInterfaceThread() {
      Console.Clear();
      Console.CursorVisible = false;
      while (true) {
        switch (sharedKey) {
          case "S":
            StartStopWatch();
            break;
          case "P":
            PauseStopWatch();
            break;
          case "R":
            ResetStopWatch();
            break;
          case "Q":
            Helpers.SaveStopWatchData(stopWatchData);
            goto exitLoop;
        }
        DisplayStopWatchInterface(Helpers.FormatTime(stopWatchData.ElapsedTime), stopWatchData.Status);
      }
    exitLoop:;
    }

    static void InputInterfaceThread() {
      while (true) {
        string userInput = Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper();
        sharedKey = userInput;
        if (sharedKey == "Q") {
          break;
        }
      }
    }


    // Helpers
    static void StartStopWatch() {
      stopWatchData.Status = true;
      long currentTime = Helpers.GetCurrentUnixTime();
      if (stopWatchData.StartedTime == 0) {
        stopWatchData.StartedTime = currentTime;
      }
      if (stopWatchData.LastPausedTime > 0) {
        stopWatchData.TotalPausedTime += currentTime - stopWatchData.LastPausedTime;
        stopWatchData.LastPausedTime = 0;
      }
      UpdateElapsedTime();
    }

    static void PauseStopWatch() {
      stopWatchData.Status = false;
      if (stopWatchData.LastPausedTime == 0 && stopWatchData.StartedTime > 0) {
        stopWatchData.LastPausedTime = Helpers.GetCurrentUnixTime();
      }
    }

    static void ResetStopWatch() {
      stopWatchData.Status = false;
      stopWatchData.ElapsedTime = 0;
      stopWatchData.StartedTime = 0;
      stopWatchData.TotalPausedTime = 0;
      stopWatchData.LastPausedTime = 0;
    }

    static void UpdateElapsedTime() {
      stopWatchData.ElapsedTime = Helpers.GetCurrentUnixTime() - stopWatchData.StartedTime - stopWatchData.TotalPausedTime;
    }

    static void DisplayStopWatchInterface(string formattedTime, bool status) {
      Console.SetCursorPosition(0, 0);
      Console.WriteLine("=== Stopwatch ===");
      Console.WriteLine($"    {formattedTime}\n");
      Console.WriteLine($"    Status: {(status ? "Running" : "Stopped")}\n");
      Console.WriteLine("    [S] Start   [P] Pause   [R] Reset   [Q] Quit\n");
      Thread.Sleep(10);
    }
  }
}

