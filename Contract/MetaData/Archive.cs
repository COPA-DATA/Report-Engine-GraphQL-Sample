namespace Contract.MetaData
{
  /// <summary>
  /// Table [dbo].[ARCHIV]
  /// </summary>
  public class Archive
  {
    ///// <summary>
    ///// Column [PARENT_ID]
    ///// </summary>
    //public int? Parent_Id { get; set; }

    /// <summary>
    /// Column [REFERENCE]
    /// </summary>
    public string ShortName { get; set; } = "";

    /// <summary>
    /// Column [VISUALNAME]
    /// </summary>
    public string LongName { get; set; } = "";

    /// <summary>
    /// Column [DESCRIPTION] or [DESCRIPTION_TRANSLATABLE] - depending on language setting in query
    /// </summary>
    public string Description { get; set; } = "";

    ///// <summary>
    ///// Column [CYCLETIME]
    ///// </summary>
    //public int CycleTime { get; set; } = 0;

    ///// <summary>
    ///// Column [LOTARCHIV]
    ///// </summary>
    //public bool IsLotArchive { get; set; } = false;

    ///// <summary>
    ///// Link based on column [PARENT_ID] of other archives (points to column [ID] of this archive)
    ///// </summary>
    //public List<Archive> AggregatedArchives { get; set; } = new List<Archive>();

    ///// <summary>
    ///// Link based on table [dbo].[VARIABLEARCHIV] (column [ARCHIV_ID] points to column [ID] of this archive) - aggregation type considered in contents
    ///// </summary>
    //public List<ArchiveVariable> Variables { get; set; } = new List<ArchiveVariable>();
  }
}
