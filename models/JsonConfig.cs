using System.Text.Json;

namespace Stopwatcher.models {

  public static class JsonConfig {
    // Reusable JsonSerializerOptions
    public static readonly JsonSerializerOptions Options = new() {
      WriteIndented = true,
      PropertyNameCaseInsensitive = true
    };
  }

}