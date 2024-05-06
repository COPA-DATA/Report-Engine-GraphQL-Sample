using Contract.Enums;

namespace Contract.MetaData
{
  /// <summary>
  /// Table [dbo].[VARIABLE]
  /// </summary>
  public class Variable
  {
    ///// <summary>
    ///// Column [REFERENCE]
    ///// </summary>
    public string? VariableName { get; set; } = "";

    /// <summary>
    /// Column [VISUALNAME] or [VISUALNAME_TRANSLATABLE] - depending on language setting in query
    /// </summary>
    public string? DisplayName { get; set; } = "";

    /// <summary>
    /// Column [DESCRIPTION] or [DESCRIPTION_TRANSLATABLE] - depending on language setting in query
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Column [UNIT] or [UNIT_TRANSLATABLE] - depending on language setting in query
    /// </summary>
    public string? MeasuringUnit { get; set; } = "";

    /// <summary>
    /// Column [RESOURCES_LABEL] - depending on language setting in query
    /// </summary>
    public string? ResourcesLabel { get; set; } = "";

    /// <summary>
    /// Column [IDENTIFICATION] - depending on language setting in query
    /// </summary>
    public string? Identification { get; set; } = "";

    /// <summary>
    /// Column [DATATYPE]
    /// </summary>
    public VariableDataType DataType { get; set; } = VariableDataType.Numeric;

    /// <summary>
    /// Column [MEANING] and table [dbo].[VARIABLEMEANING]
    /// </summary>
    public List<string> Meanings { get; set; } = new List<string>();
  }
}
