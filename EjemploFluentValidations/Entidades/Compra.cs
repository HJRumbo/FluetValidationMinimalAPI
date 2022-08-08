using FluentValidation;

namespace EjemploFluentValidations.Entidades
{
    public class Compra
    {
        public int Id { get; set; }
        public string? FechaCompra { get; set; }
        public decimal TotalCompra { get; set; }
    }

    public class CompraValidator : AbstractValidator<Compra>
    {
        public CompraValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("El id es requerido");
            RuleFor(x => x.FechaCompra).NotNull().WithMessage("La fecha de compra es requerida");
            RuleFor(x => x.TotalCompra).NotNull().WithMessage("El total de la compra es requerido");
        }
    }
}
