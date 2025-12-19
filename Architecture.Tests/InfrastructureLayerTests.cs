using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Tests that enforce infrastructure layer conventions and best practices.
/// </summary>
public class InfrastructureLayerTests : BaseTest
{
    [Fact]
    public void Repositories_ShouldHaveNameEndingWith_Repository()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceContaining("Repositories")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Repositories should have names ending with 'Repository'", result));
    }

    [Fact]
    public void Repositories_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .HaveNameEndingWith("Repository")
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Repositories should be sealed", result));
    }

    [Fact]
    public void EntityConfigurations_ShouldHaveNameEndingWith_Configuration()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceContaining("Configurations")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Configuration")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Entity configurations should have names ending with 'Configuration'", result));
    }

    [Fact]
    public void EntityConfigurations_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .HaveNameEndingWith("Configuration")
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Entity configurations should be sealed", result));
    }

    [Fact]
    public void Services_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceContaining("Services")
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Infrastructure services should be sealed", result));
    }

    private static string GetFailureMessage(string rule, TestResult result)
    {
        if (result.IsSuccessful)
            return string.Empty;

        var failingTypes = result.FailingTypeNames ?? [];
        return $"{rule}. Violating types: {string.Join(", ", failingTypes)}";
    }
}

