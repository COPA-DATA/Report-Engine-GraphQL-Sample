using Contract;
using GraphQL_CLI.GraphQL;
using IdentityModel.Client;

namespace GraphQL_CLI
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      try
      {
        var client = new HttpClient();

        var discoveryDocument = await client.GetDiscoveryDocumentAsync(Constants.IdsUrl);

        if (discoveryDocument.IsError)
        {
          Console.WriteLine("Getting the discovery document failed.");
          Console.WriteLine($"Error: {discoveryDocument.Error}.");
          return;
        }

        //get the access token
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
          Address = discoveryDocument.TokenEndpoint,
          ClientId = Constants.ClientId,
          ClientSecret = Constants.Secret,
          Scope = Constants.ApiScopeQuery,
        });

        if (tokenResponse.IsError)
        {
          Console.WriteLine("Accessing the access token failed.");
          Console.WriteLine($"Error: {tokenResponse.Error}.");
          return;
        }

        Console.WriteLine("Ready for querying data from GraphQL interface!");

        var graph = new GraphQLAPI(tokenResponse);
        Console.WriteLine("");
        Console.WriteLine("#### Archives Query ####");
        graph.ArchivesQueryAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine("");
        Console.WriteLine("#### Variable Query ####");
        graph.VariablesQueryAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine("");
        Console.WriteLine("#### Lot Query ####");
        graph.LotQueryAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine("");
        Console.WriteLine("#### Current Variable Values Query ####");
        graph.CurrentVariableValuesQueryAsync().ConfigureAwait(false).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }
  }
}