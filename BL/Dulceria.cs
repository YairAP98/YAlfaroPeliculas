using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dulceria
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.YalfaroCineContext context = new DL.YalfaroCineContext())
                {
                    var query = context.Dulceria.FromSqlRaw("DulceriaGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Dulceria dulceria = new ML.Dulceria();
                            dulceria.IdDulce = row.IdDulce;
                            dulceria.Nombre = row.Nombre;
                            dulceria.Precio = (decimal)row.Precio;
                            dulceria.Imagen = row.Imagen;
                          
                            result.Objects.Add(dulceria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de dulces";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdDulce)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.YalfaroCineContext context = new DL.YalfaroCineContext())
                {
                    var query = context.Dulceria.FromSqlRaw($"DulceriaGetById '{IdDulce}'").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Dulceria dulceria = new ML.Dulceria();
                        dulceria.IdDulce = query.IdDulce;
                        dulceria.Nombre = query.Nombre;
                        dulceria.Precio =(decimal) query.Precio;
                        dulceria.Imagen = query.Imagen;
                       
                        result.Object = dulceria;
                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }
    }

}
