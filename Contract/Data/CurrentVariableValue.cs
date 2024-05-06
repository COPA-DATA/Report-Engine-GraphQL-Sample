using Contract.MetaData;

namespace Contract.Data
{
  /// <summary>
  /// Output of [dbo].[zrsQueryCurrentValueFunction]
  /// </summary>
  public class CurrentVariableValue
  {
    /// <summary>
    /// Link based on column [VARIABLE_ID]
    /// </summary>
    public Variable Variable { get; set; } = new Variable();

    /// <summary>
    /// Column [VALUE] (when variable is not string)
    /// </summary>
    public double NumericValue { get; set; } = 0;

    /// <summary>
    /// Column [STRVALUE] (when variable is string)
    /// </summary>
    public string? StringValue { get; set; } = "";

    /// <summary>
    /// Column [STATUSFLAGS]
    /// </summary>
    public long? StatusFlags { get; set; } = 0;

    /// <summary>
    /// Column [TIMESTAMP]
    /// </summary>
    public DateTime? Timestamp { get; set; } = DateTime.MinValue;
  }
}
