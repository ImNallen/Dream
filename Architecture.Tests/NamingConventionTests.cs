using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Tests that enforce naming conventions across all layers.
/// </summary>
public class NamingConventionTests : BaseTest
{
    [Fact]
    public void Interfaces_ShouldStartWith_I()
    {
        var result = Types
            .InAssemblies([DomainAssembly, ApplicationAssembly, InfrastructureAssembly, ApiAssembly])
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Interfaces should start with 'I'", result));
    }

    [Fact]
    public void AbstractClasses_ShouldNotHaveAbstractPrefix()
    {
        // Abstract classes should use meaningful names, not "AbstractXxx"
        var result = Types
            .InAssemblies([DomainAssembly, ApplicationAssembly, InfrastructureAssembly, ApiAssembly])
            .That()
            .AreAbstract()
            .And()
            .AreClasses()
            .ShouldNot()
            .HaveNameStartingWith("Abstract")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Abstract classes should not have 'Abstract' prefix", result));
    }

    [Fact]
    public void Exceptions_ShouldHaveNameEndingWith_Exception()
    {
        var result = Types
            .InAssemblies([DomainAssembly, ApplicationAssembly, InfrastructureAssembly, ApiAssembly])
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .HaveNameEndingWith("Exception")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Exceptions should have names ending with 'Exception'", result));
    }

    [Fact]
    public void DtoClasses_ShouldHaveNameEndingWith_Dto_Or_Request_Or_Response()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Dtos")
            .Or()
            .ResideInNamespaceContaining("Models")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Dto")
            .Or()
            .HaveNameEndingWith("Request")
            .Or()
            .HaveNameEndingWith("Response")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("DTOs should have names ending with 'Dto', 'Request', or 'Response'", result));
    }

    [Fact]
    public void AllClasses_ShouldNotHave_HelperSuffix()
    {
        // "Helper" is often a code smell - prefer more specific names
        var result = Types
            .InAssemblies([DomainAssembly, ApplicationAssembly, InfrastructureAssembly, ApiAssembly])
            .That()
            .AreClasses()
            .ShouldNot()
            .HaveNameEndingWith("Helper")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Classes should not have 'Helper' suffix - use more specific names", result));
    }

    [Fact]
    public void AllClasses_ShouldNotHave_ManagerSuffix()
    {
        // "Manager" is often too vague - prefer more specific names
        var result = Types
            .InAssemblies([DomainAssembly, ApplicationAssembly, InfrastructureAssembly, ApiAssembly])
            .That()
            .AreClasses()
            .ShouldNot()
            .HaveNameEndingWith("Manager")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            GetFailureMessage("Classes should not have 'Manager' suffix - use more specific names", result));
    }

    private static string GetFailureMessage(string rule, TestResult result)
    {
        if (result.IsSuccessful)
            return string.Empty;

        var failingTypes = result.FailingTypeNames ?? [];
        return $"{rule}. Violating types: {string.Join(", ", failingTypes)}";
    }
}

