using MyRestApi.Tests;
using Xunit;

[CollectionDefinition("SharedTestCollection")]
public class TestCollection : ICollectionFixture<TestSetup> { }