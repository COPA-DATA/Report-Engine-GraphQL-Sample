# Introduction

This is a small sample that shows how to query data from the Report Engine GraphQL interface.

# Projects

The sample contains 2 projects:

`Contract` defines some constants for running this sample and the required input/output data classes for accessing the GraphQL interface.
**IMPORTANT:** This project contains a file `Constants.cs` where you have to enter information about your environment. Otherwise, the sample will not be executable.

`GraphQL_CLI` contains the worker class for doing GraphQL requests via the NuGet package `GraphQL.Client` and a program routine that acquires the required access token and triggers the GraphQL queries.

the projects have been developed using .NET 6 and Microsoft Visual Studio 2022. You may either have to install this IDE and SDK or change the project so that you can open it in an IDE of your choice or use a different .NET version.

# Prerequisites for Running the Sample

These components will be needed from the zenon Software platform, at least version 12:

- Engineering Studio
- Service Engine
- Report Engine including GraphQL Interface
- Reporting Studio
- IIoT Services

# Preparations for Running the Sample

The steps below assume that everything is installed properly, the IIoT Services have been configured after installation and at least one Report Engine database is available. Further more it is assumed, that all parts of the zenon Software Platform are running on the same computer and also this sample is executed on this computer.

These things need to be prepared:

1. Connect the Report Engine to the IIoT Services by running through the IIoT Services Connection Wizard from within the Reporting Studio (ribbon "Options", button "Service Node" -> "Interface").
2. Create a project in Engineering Studio.
3. Connect the project to the IIoT Services by running through the IIoT Services Connection Wizard from within Engineering Studio (project properties node "Network", first enable "Activate IIoT Services", then click "Connection Settings" or "IIoT Services connection settings"). In the wizard, select the Report Engine from step 1 and a fitting Report Engine database.
4. Create at least one lot archive in the project and ensure data is being generated.
5. Create at least one variable in the project and ensure the value can be changed or is changing automatically.
6. Execute Metadata Synchronizer in Engineering Studio.
7. Start the Service Engine. Ensure that the Service Engine Connector is running (can be started via Startup Tool).
8. Open IIoT Services Configuration Studio.
9. Navigate to "Identity Management" -> "Clients".
10. Click on "Add" in the right top corner.
11. Click "Custom OAuth 2.0 client".
12. Enter client ID and client name.
13. Add "graphQLInterface" as allowed scope.
14. Store the client ID and client secret for later use.
15. Confirm the popup dialog by clicking "Add".

If you already have a Service Engine project with variables and lot archives, you can simply perform step 1 from above, apply the steps 3, 6 and 7 from above to the existing project and then run through the steps 8 - 15 from above.

# Configuring and Running the Sample

To configure the sample for running it in your environment, you need to exchange things marked with `<<` and `>>` in the `Constants.cs` file of the `Contract` project.

This file initially looks like this:

```c#
namespace Contract
{
  /// <summary>
  /// This settings must be modified for your environment
  /// </summary>
  public static class Constants
  {
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
```

Things to replace are shown in the following table. The contents have been generated in the preparation steps above.

| Placeholder | Description |
|-------------|-------------|
| `<<your computername>>` | The fully qualified domain name of your computer. |
| `<<your IDS client ID>>` | The ID of the Identity Service client. |
| `<<your IDS client secret>>` | The client secret of the Identity Service client. |
| `<<your Report Engine database>>` | The Report Engine database where the Metadata Synchronizer is writing project data to. |
| `<<your Service Engine project>>` | The name of the project generating the lots and variable values. |
| `<<your lot archive>>` | The short name of the lot archive. |
| `<<UTC start timestamp for reading lots>>` | A timestamp where lots are available and that is earlier than the end time stamp. |
| `<<UTC end timestamp for reading lots>>` | A timestamp where lots are available and that is later than the start time stamp. |
| `<<your variables names separated by comma>>` | One or more variables in the Service Engine project. |

The modified configuration file may look like this:

```c#
namespace Contract
{
  /// <summary>
  /// This settings must be modified for your environment
  /// </summary>
  public static class Constants
  {
    // computer data - EXCHANGE IT!
    private const string FQDN = "MyComputer.MyDomain.com";

    // IDS client data - EXCHANGE IT!
    public const string ClientId = "MyGraphQlClient";
    public const string Secret = "SomeBase64EncodedIdsClientSecret";

    // project data - EXCHANGE IT!
    public const string DatabaseName = "TEST_DB";
    public const string ProjectName = "TEST_PROJECT";
    public const string LotArchiveShortName = "L1";
    public static readonly DateTime LotArchiveStart = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
    public static readonly DateTime LotArchiveEnd = new DateTime(2024, 1, 1, 11, 0, 0, DateTimeKind.Utc);
    public static readonly string[] Variables =
    {
      "Variable1",
      "Variable2",
      "Variable3"
    };

    // constant data - no need to change
    public const string ApiScopeQuery = "graphQLInterface";
    public const string IdsUrl = "https://" + FQDN + ":9443/identity-service";
    public const string GraphQLInterfaceUrl = "https://" + FQDN + ":50793/graphql";
  }
}
```

Now just build the whole solution and run or debug the `GraphQL_CLI` project.