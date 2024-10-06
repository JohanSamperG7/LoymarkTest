using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Loymark.Back.WebApi.Tests.CollectionBehaviour
{
    [CollectionDefinition("TestCollection", DisableParallelization = false)]
    public class TestCollectionBehaviour : IClassFixture<IntegrationTestBuilder<Program>>
    {
    }
}
