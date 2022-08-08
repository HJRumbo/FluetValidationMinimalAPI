using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDapper.Entidades
{
    public class Empleado
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int DepartamentoId { get; set; }
    }
}
