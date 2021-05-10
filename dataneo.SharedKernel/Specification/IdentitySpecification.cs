﻿using System;
using System.Linq.Expressions;

namespace dataneo.SharedKernel
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}