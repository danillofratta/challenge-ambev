using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Base.Domain.Specification;

    public abstract class BaseSpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);
    }

