using Testcontainers.PostgreSql;

namespace ProjectService.Tests.Fixtures;

public class PostgresFixture : IAsyncLifetime
{
  private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:16")
    .WithDatabase("testdb")
    .WithUsername("testuser")
    .WithPassword("testpassword")
    .Build();

  public string ConnectionString => _container.GetConnectionString();

  public Task InitializeAsync() => _container.StartAsync();

  public async Task DisposeAsync() => await _container.DisposeAsync();
}

[CollectionDefinition("PostgresCollection")]
public class PostgresCollection : ICollectionFixture<PostgresFixture> { }
