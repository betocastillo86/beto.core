//-----------------------------------------------------------------------
// <copyright file="BaseFilterModalValidatorFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Tests.Fakes
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Base FIlter Model Validator Fake
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Beto.Core.Web.Tests.Fakes.BaseFilterModelFake}" />
    public class BaseFilterModalValidatorFake : AbstractValidator<BaseFilterModelFake>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilterModalValidatorFake"/> class.
        /// </summary>
        public BaseFilterModalValidatorFake()
        {
            this.AddBaseFilterValidations();
        }
    }
}