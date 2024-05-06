using Contract;
using Contract.Response;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using IdentityModel.Client;

namespace GraphQL_CLI.GraphQL
{
  public class GraphQLAPI
  {
    private readonly GraphQLHttpClient _client;
    public GraphQLAPI()
    {
      _client = new GraphQLHttpClient(Constants.GraphQLInterfaceUrl, new NewtonsoftJsonSerializer());
    }

    public GraphQLAPI(TokenResponse tokenResponse)
    {
      _client = new GraphQLHttpClient(Constants.GraphQLInterfaceUrl, new NewtonsoftJsonSerializer());
      _client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.AccessToken}");
    }
    private async Task ExecuteQueryAsync<Response>(GraphQLHttpClient client, GraphQLRequest request, Action<Response> toSomthing)
    {
      try
      {
        var graphQLResponse = await client.SendQueryAsync<Response>(request).ConfigureAwait(false);

        if (graphQLResponse.Data == null)
        {
          graphQLResponse?.Errors?.ToList().ForEach(error => Console.WriteLine(error.Message));
        }
        else
        {
          toSomthing(graphQLResponse.Data);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public async Task ArchivesQueryAsync()
    {
      await ExecuteQueryAsync<ArchivesResponse>(_client, new GraphQLRequest
      {
        Query = @"
                  query ArchivesQuery($databaseName: String!) {
                   archives(database: $databaseName) {
                    shortName
                    longName
                    description
                   }
                  }",
        Variables = new { databaseName = Constants.DatabaseName }
      },
     (response) =>
     {
       foreach (var archive in response.Archives)
       {
         Console.WriteLine($"{archive.ShortName}, {archive.LongName}, {archive.Description}");
       }
     }).ConfigureAwait(false);
    }

    public async Task VariablesQueryAsync()
    {
      await ExecuteQueryAsync<VariableResponse>(_client, new GraphQLRequest
      {
        Query = @"
                  query VariablesQueryAsync($databaseName: String!) {
                  variables(database: $databaseName) {
		              variableName
		              displayName
		              description
		              measuringUnit
		              resourcesLabel
		              identification
		              dataType
		              meanings
                   }
                  }",
        Variables = new { databaseName = Constants.DatabaseName }
      },
     (response) =>
     {
       foreach (var variable in response.Variables)
       {
         Console.WriteLine($"{variable.DisplayName}, {variable.Description}, {variable.MeasuringUnit}, {variable.DataType}");
       }
     }).ConfigureAwait(false);
    }

    public async Task LotQueryAsync()
    {
      await ExecuteQueryAsync<LotDataResponse>(_client, new GraphQLRequest
      {
        Query = @"
                  query LotQuery(
	                  $databaseName: String!
	                  $project: String!
	                  $archive: String!
	                  $start: DateTime!
	                  $end: DateTime!
                  ) {
	                  lotData(
		                  database: $databaseName
		                  project: $project
		                  archive: $archive
		                  startTime: $start
		                  endTime: $end
	                  ) {
		                  name
		                  start
		                  end
	                  }
                  }"
            ,
        Variables = new
        {
          databaseName = Constants.DatabaseName,
          project = Constants.ProjectName,
          archive = Constants.LotArchiveShortName,
          start = Constants.LotArchiveStart.ToString("O"),
          end = Constants.LotArchiveEnd.ToString("O")
        }
      },
     (response) =>
     {
       if (response.LotData == null)
       {
         throw new ArgumentNullException();
       }
       foreach (var lot in response.LotData)
       {
         Console.WriteLine($"{lot.Name}, {lot.Start}, {lot.End}");
       }
     }).ConfigureAwait(false);
    }

    public async Task CurrentVariableValuesQueryAsync()
    {
      await ExecuteQueryAsync<CurrentVariableValuesResponse>(_client, new GraphQLRequest
      {
        Query = @"
                query CurrentVariableValuesQuery(
                 $databaseName: String!
                 $project: String!
                 $variables: [String]!
                ) {
                 currentVariableValues(
                  database: $databaseName
                  project: $project
                  variables: $variables
                 ) {
                  variable {
                   displayName
                   description
                  }
                  numericValue
                  stringValue
                  timestamp
                 }
                }
              ",
        Variables = new
        {
          databaseName = Constants.DatabaseName,
          project = Constants.ProjectName,
          variables = Constants.Variables
        }
      },
       (response) =>
       {
         if (response.CurrentVariableValues == null)
         {
           throw new ArgumentNullException();
         }
         foreach (var variable in response.CurrentVariableValues)
         {
           Console.WriteLine($"{variable.Variable.DisplayName}, {variable.Variable.Description}, {variable.NumericValue}, {variable.StringValue}, {variable.Timestamp}");
         }
       }).ConfigureAwait(false);
    }
  }
}
