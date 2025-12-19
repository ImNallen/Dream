using System.Reflection;

namespace Architecture.Tests;

/// <summary>
/// Base class providing assembly references for architecture tests.
/// </summary>
public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Domain.Abstractions.Entity<>).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Application.DependencyInjection).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Api.Program).Assembly;

    protected const string DomainNamespace = "Domain";
    protected const string ApplicationNamespace = "Application";
    protected const string InfrastructureNamespace = "Infrastructure";
    protected const string ApiNamespace = "Api";
}

