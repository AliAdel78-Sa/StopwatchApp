namespace Stopwatcher.models {
  public class StopWatchModel {
    public bool Status { get; set; }
    public long StartedTime { get; set; }
    public long ElapsedTime { get; set; }
    public long LastPausedTime { get; set; }
    public long TotalPausedTime { get; set; }
  }
}