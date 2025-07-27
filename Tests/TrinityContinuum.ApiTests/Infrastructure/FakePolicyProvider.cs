using Microsoft.AspNetCore.Authorization;

namespace TrinityContinuum.ApiTests.Infrastructure;

// This class provides a custom authorization policy that allows all requests.
public class FakePolicyProvider : IAuthorizationPolicyProvider
{
    // This method is called for a simple [Authorize] attribute.
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        var policy = new AuthorizationPolicyBuilder();
        policy.RequireAssertion(context => true); // This assertion always passes.
        return Task.FromResult(policy.Build());
    }

    // Returns the fallback policy, also not needed for this scenario.
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return Task.FromResult<AuthorizationPolicy?>(null);
    }

    // This is the key method. It's called for any [Authorize(Policy = "...")].
    // We override it to return a policy that requires nothing, effectively allowing anonymous access.
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder();
        policy.RequireAssertion(context => true); // This assertion always passes.
        return Task.FromResult<AuthorizationPolicy?>(policy.Build());
    }
}