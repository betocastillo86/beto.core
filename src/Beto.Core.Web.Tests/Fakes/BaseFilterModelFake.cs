//-----------------------------------------------------------------------
// <copyright file="BaseFilterModelFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Tests.Fakes
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// Base FIlter Model Fake
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class BaseFilterModelFake : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterModelFake"/> class.
        /// </summary>
        /// <param name="maxPageSize">Maximum size of the page.</param>
        /// <param name="validOrderBy">The valid order by.</param>
        public BaseFilterModelFake(int? maxPageSize = null, string[] validOrderBy = null)
        {
            this.MaxPageSize = maxPageSize ?? 5;
            this.ValidOrdersBy = validOrderBy ?? new string[] { "a", "b", "c" };
        }
    }
}