using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Tests that enforce API layer conventions and best practices.
/// </summary>
public class ApiLayerTests : BaseTest
{
    [Fact]
    public void Controllers_ShouldHaveNameEndingWith_Controller()
    {
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .ResideInNamespaceContaining("Controllers")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Controllers should have names ending with 'Controller'", result));
    }

    [Fact]
    public void Controllers_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Controllers should be sealed", result));
    }

    [Fact]
    public void Api_ShouldNotDependOn_DomainRepositories()
    {
        // API should use MediatR to send commands/queries, not call repositories directly
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .ResideInNamespaceContaining("Controllers")
            .ShouldNot()
            .HaveDependencyOnAny("Domain.Repositories", "Infrastructure.Repositories")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("API controllers should not depend on repositories directly", result));
    }

    [Fact]
    public void Middleware_ShouldHaveNameEndingWith_Middleware()
    {
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .ResideInNamespaceContaining("Middleware")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Middleware")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Middleware classes should have names ending with 'Middleware'", result));
    }

    [Fact]
    public void Filters_ShouldHaveNameEndingWith_Filter()
    {
        var result = Types
            .InAssembly(ApiAssembly)
            .That()
            .ResideInNamespaceContaining("Filters")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Filter")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Filter classes should have names ending with 'Filter'", result));
    }

    private static string GetFailureMessage(string rule, TestResult result)
    {
        if (result.IsSuccessful)
            return string.Empty;

        var failingTypes = result.FailingTypeNames ?? [];
        return $"{rule}. Violating types: {string.Join(", ", failingTypes)}";
    }
}

