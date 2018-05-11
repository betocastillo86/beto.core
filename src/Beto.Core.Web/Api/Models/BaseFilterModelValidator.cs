//-----------------------------------------------------------------------
// <copyright file="BaseFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api.Models
{
    using System.Linq;
    using FluentValidation;

    /// <summary>
    /// Base Filter Model Validator
    /// </summary>
    public static class BaseFilterModelValidator
    {
        /// <summary>
        /// Adds the base filter validations.
        /// </summary>
        /// <typeparam name="T">a Base filter model</typeparam>
        /// <param name="baseFilter">The base filter.</param>
        public static void AddBaseFilterValidations<T>(this AbstractValidator<T> baseFilter) where T : BaseFilterModel
        {
            baseFilter.RuleFor(c => c.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(c => c.MaxPageSize)
                .WithMessage(c => $"Tamaño máximo de paginación excedido. El máximo es {c.MaxPageSize}");

            baseFilter.RuleFor(c => c.Page)
                .GreaterThanOrEqualTo(0)
                .WithMessage("La pagina debe ser mayor a 0");

            baseFilter.RuleFor(c => c)
                .Must(c => string.IsNullOrEmpty(c.OrderBy) || (!string.IsNullOrEmpty(c.OrderBy) && c.ValidOrdersBy.Contains(c.OrderBy)))
                .WithMessage(c => $"El parametro orderBy no es valido. Las opciones son: {string.Join(",", c.ValidOrdersBy)}");
        }
    }
}