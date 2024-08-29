using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Critaria { get ; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } =  new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDescening { get ; set; }
        public int Take { get ; set; }
        public int Skip { get ; set ; }
        public bool IsPaginatedEnable { get; set ; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeStrings { get; set ; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        public BaseSpecification()
        {
                
        }
        public BaseSpecification(Expression<Func<T, bool>> Critaria )
        {
            this.Critaria = Critaria;
        }

        // AddInclude for ThenIncludes
        protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            IncludeStrings.Add(includeExpression);
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        public void AddOrderByDescening(Expression<Func<T, object>> OrderByDescening)
        {
            this.OrderByDescening = OrderByDescening;
        }
        public void AddPagination(int skip , int take)
        {
            IsPaginatedEnable=true;
            this.Skip = skip;
            this.Take = take;
        }
    }
}
