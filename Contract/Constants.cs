namespace Contract
{
  /// <summary>
  /// This settings must be modified for your environment
  /// </summary>
  public static class Constants
  {
    // The compiler errors in this file are intended in the repository.
    // See the readme on how to modify this file to solve the compiler errors and get the sample running in your environment.

    // computer data - EXCHANGE IT!
    private const string FQDN = <<your computername>>;
    
    // IDS client data - EXCHANGE IT!
    public const string ClientId = <<your IDS client ID>>;
    public const string Secret = <<your IDS client secret>>;

    // project data - EXCHANGE IT!
    public const string DatabaseName = <<your Report Engine database>>;
    public const string ProjectName = <<your Service Engine project>>;
    public const string LotArchiveShortName = <<your lot archive>>;
    public static readonly DateTime LotArchiveStart = <<UTC start timestamp for reading lots>>;
    public static readonly DateTime LotArchiveEnd = <<UTC end timestamp for reading lots>>;
    public static readonly string[] Variables =
    {
      <<your variables names separated by comma>>
    };

    // constant data - no need to change
    public const string ApiScopeQuery = "graphQLInterface";
    public const string IdsUrl = "https://" + FQDN + ":9443/identity-service";
    public const string GraphQLInterfaceUrl = "https://" + FQDN + ":50793/graphql";
  }
}
