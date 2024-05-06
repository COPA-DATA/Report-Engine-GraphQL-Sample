namespace Contract.Data
{
  /// <summary>
  /// Output of [dbo].[zrsQueryLotFunction]
  /// </summary>
  public class Lot
  {
    /// <summary>
    /// Column [LOT]
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Column [START]
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Column [END]
    /// </summary>
    public DateTime End { get; set; }
  }
}
