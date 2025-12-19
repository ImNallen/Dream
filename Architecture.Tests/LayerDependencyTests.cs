using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Tests that enforce Clean Architecture dependency rules.
/// Dependencies should only flow inward: Api/Infrastructure → Application → Domain
/// </summary>
public class LayerDependencyTests : BaseTest
{
    [Fact]
    public void Domain_ShouldNotDependOn_Application()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApplicationNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain", "Application", result));
    }

    [Fact]
    public void Domain_ShouldNotDependOn_Infrastructure()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain", "Infrastructure", result));
    }

    [Fact]
    public void Domain_ShouldNotDependOn_Api()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain", "Api", result));
    }

    [Fact]
    public void Application_ShouldNotDependOn_Infrastructure()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Application", "Infrastructure", result));
    }

    [Fact]
    public void Application_ShouldNotDependOn_Api()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Application", "Api", result));
    }

    [Fact]
    public void Infrastructure_ShouldNotDependOn_Api()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Infrastructure", "Api", result));
    }

    private static string GetFailureMessage(string layer, string dependencyLayer, TestResult result)
    {
        if (result.IsSuccessful)
            return string.Empty;

        var failingTypes = result.FailingTypeNames ?? [];
        return $"{layer} should not depend on {dependencyLayer}. " +
               $"Violating types: {string.Join(", ", failingTypes)}";
    }
}

