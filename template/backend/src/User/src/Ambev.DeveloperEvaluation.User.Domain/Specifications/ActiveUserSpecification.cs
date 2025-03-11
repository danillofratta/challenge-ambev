using Ambev.Base.Domain.Specification;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class ActiveUserSpecification : BaseSpecification<User>
{
    public override bool IsSatisfiedBy(User entity)
    {
        return entity.Status == UserStatus.Active;
    }
}
