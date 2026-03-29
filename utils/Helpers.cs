using System.Text.Json;
using Stopwatcher.models;

namespace Stopwatcher.utils {
  static class Helpers {
    public static string FormatTime(long ms) {
      var duration = TimeSpan.FromMilliseconds(ms);
      if (ms >= Constants.MsInHour) {
        return duration.ToString(Constants.HourFormat);
      }
      else {
        return duration.ToString(Constants.MinuteFormat);
      }
    }

    public static long GetCurrentUnixTime() {
      return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public static StopWatchModel RetrieveStopWatchData() {
      if (!File.Exists(Constants.JsonFile)) {
        SaveStopWatchData(new StopWatchModel());
      }
      string jsonString = File.ReadAllText(Constants.JsonFile);
      StopWatchModel stopWatchModel = JsonSerializer.Deserialize<StopWatchModel>(jsonString)!;
      return stopWatchModel;
    }

    public static void SaveStopWatchData(StopWatchModel stopWatchModel) {
      string jsonString = JsonSerializer.Serialize(stopWatchModel, JsonConfig.Options);
      File.WriteAllText(Constants.JsonFile, jsonString);
    }
  }
}