using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Tests that enforce application layer conventions and best practices.
/// </summary>
public class ApplicationLayerTests : BaseTest
{
    [Fact]
    public void Commands_ShouldHaveNameEndingWith_Command()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .And()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .And()
            .DoNotHaveNameEndingWith("Handler")
            .And()
            .DoNotHaveNameEndingWith("Validator")
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Commands should have names ending with 'Command'", result));
    }

    [Fact]
    public void Queries_ShouldHaveNameEndingWith_Query()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Queries")
            .And()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .And()
            .DoNotHaveNameEndingWith("Handler")
            .And()
            .DoNotHaveNameEndingWith("Validator")
            .Should()
            .HaveNameEndingWith("Query")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Queries should have names ending with 'Query'", result));
    }

    [Fact]
    public void CommandHandlers_ShouldHaveNameEndingWith_CommandHandler()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .And()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Command handlers should have names ending with 'CommandHandler'", result));
    }

    [Fact]
    public void QueryHandlers_ShouldHaveNameEndingWith_QueryHandler()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Queries")
            .And()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Query handlers should have names ending with 'QueryHandler'", result));
    }

    [Fact]
    public void Validators_ShouldHaveNameEndingWith_Validator()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .Or()
            .ResideInNamespaceContaining("Queries")
            .And()
            .HaveNameEndingWith("Validator")
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Validators should have names ending with 'Validator'", result));
    }

    [Fact]
    public void Handlers_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Handlers should be sealed", result));
    }

    [Fact]
    public void Application_ShouldNotReference_EntityFramework()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Application should not reference Entity Framework", result));
    }

    [Fact]
    public void Application_ShouldNotReference_AspNetCore()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.AspNetCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Application should not reference ASP.NET Core", result));
    }

    private static string GetFailureMessage(string rule, TestResult result)
    {
        if (result.IsSuccessful)
            return string.Empty;

        var failingTypes = result.FailingTypeNames ?? [];
        return $"{rule}. Violating types: {string.Join(", ", failingTypes)}";
    }
}

