// See https://aka.ms/new-console-template for more information

using Dapper;
using EjemploDapper.Entidades;
using Microsoft.Data.SqlClient;

var connectionString = "Data Source=DESKTOP-KGON68I\\SQLEXPRESS;Initial Catalog=TestJoin;Integrated Security=True;TrustServerCertificate=True";

// Ejemplo 1: Un simple Query

var queryDepartamentos = "Select * From Departamentos";

using (var connection = new SqlConnection(connectionString))
{
    //var departamentosAnonimo = connection.Query(queryDepartamentos).ToList();

    var departamentos = connection.Query<Departamento>(queryDepartamentos).ToList();
}

// Ejemplo 2: Un insert con Dapper

var queryInsertDepartamento = "Insert Into Departamentos (id, nombre) Values (@id, @nombre)";

//using (var connection = new SqlConnection(connectionString))
//{
//    connection.Execute(queryInsertDepartamento, new { id = 36, nombre = "Human Resources"});
//    var departamentos = connection.Query<Departamento>(queryDepartamentos).ToList();
//}

// Ejemplo 3: Queries multiples

var queryMultiple = "Select * From Departamentos Order By Id desc; Select * From Empleados";

using (var connection = new SqlConnection(connectionString))
{
    using (var multi = connection.QueryMultiple(queryMultiple))
    {
        var departamentos = multi.Read<Departamento>().ToList();
        var empleados = multi.Read<Empleado>().ToList();
    }
}

// Ejemplo 4: Inner Join

var queryInnerJoin = @"Select * From Departamentos 
                    Inner Join Empleados 
                    On Departamentos.Id = Empleados.DepartamentoId";

var diccionarioDepartamento = new Dictionary<int, Departamento>();

//using (var connection = new SqlConnection(connectionString))
//{
//    var listado = connection.Query<Departamento, Empleado, Departamento>(queryInnerJoin,
//        (departamento, empleado) =>
//        {
//            Departamento departamentoTemp;

//            if (!diccionarioDepartamento.TryGetValue(departamento.Id, out departamentoTemp!))
//            {
//                departamentoTemp = departamento;

//                departamentoTemp.Empleados = new List<Empleado>();

//                diccionarioDepartamento.Add(departamentoTemp.Id, departamento);

//            }

//            if (empleado != null)
//            {
//                departamentoTemp.Empleados.Add(empleado);
//            }

//            return departamentoTemp;
//        }).Distinct().ToList();
//}

// Ejemplo 5: Un select con parámetros

var queryParametros = "Select * From Departamentos Where Id = @Id";

using (var connection = new SqlConnection(connectionString))
{
    //var departamentosAnonimo = connection.Query(queryDepartamentos).ToList();

    var departamentos = connection.Query<Departamento>(queryParametros, new { Id = 36 }).ToList();
}


Console.WriteLine("Hello, World!");
