using Domain.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Tests that enforce domain layer conventions and best practices.
/// </summary>
public class DomainLayerTests : BaseTest
{
    [Fact]
    public void Entities_ShouldInheritFrom_EntityBase()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace("Domain.Entities")
            .Should()
            .Inherit(typeof(Entity<>))
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Entities should inherit from Entity<TId>", result));
    }

    [Fact]
    public void AggregateRoots_ShouldInheritFrom_AggregateRootBase()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .HaveNameEndingWith("Aggregate")
            .Should()
            .Inherit(typeof(AggregateRoot<>))
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Aggregates should inherit from AggregateRoot<TId>", result));
    }

    [Fact]
    public void ValueObjects_ShouldInheritFrom_ValueObjectBase()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace("Domain.ValueObjects")
            .Should()
            .Inherit(typeof(ValueObject))
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Value objects should inherit from ValueObject", result));
    }

    [Fact]
    public void DomainEvents_ShouldImplement_IDomainEvent()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace("Domain.Events")
            .Should()
            .ImplementInterface(typeof(IDomainEvent))
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain events should implement IDomainEvent", result));
    }

    [Fact]
    public void DomainEvents_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain events should be sealed", result));
    }

    [Fact]
    public void ValueObjects_ShouldBeSealed()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(ValueObject))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Value objects should be sealed", result));
    }

    [Fact]
    public void Domain_ShouldNotReference_EntityFramework()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain should not reference Entity Framework", result));
    }

    [Fact]
    public void Domain_ShouldNotReference_AspNetCore()
    {
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.AspNetCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Domain should not reference ASP.NET Core", result));
    }

    private static string GetFailureMessage(string rule, TestResult result)
    {
        if (result.IsSuccessful)
            return string.Empty;

        var failingTypes = result.FailingTypeNames ?? [];
        return $"{rule}. Violating types: {string.Join(", ", failingTypes)}";
    }
}

