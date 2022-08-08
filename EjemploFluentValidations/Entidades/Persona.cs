using FluentValidation;

namespace EjemploFluentValidations.Entidades
{
    public class Persona
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Sexo { get; set; }
        public int? Edad { get; set; }
    }

    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("El id es requerido");
            RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre es requerido");
            RuleFor(x => x.Sexo).NotNull().WithMessage("El sexo es requerido").Must(BeAValidSexo).WithMessage("El sexo debe ser M o F");
            RuleFor(x => x.Edad).NotNull().WithMessage("La edad es requerida");
        }

        private bool BeAValidSexo(string? sexo)
        {
            if (sexo?.ToUpper() == "F" || sexo?.ToUpper() == "M")
            {
                return true;
            }else
            {
                return false;
            }
        } 
    }
}
