
namespace Ambev.Base.Domain.Specification;

public abstract class BaseSpecification<T>
{
    public abstract bool IsSatisfiedBy(T entity);
}

