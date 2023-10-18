using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.YalfaroCineContext context = new DL.YalfaroCineContext())
                {
                    var filasAf = context.Zonas.FromSqlRaw("GetAllZonas").ToList();
                    result.Objects = new List<object>();
                    if (filasAf != null)
                    {
                        foreach (var registro in filasAf)
                        {
                            ML.Zona zona = new ML.Zona();
                            zona.IdZona = registro.IdZona;
                            zona.Nombre = registro.Nombre;

                            result.Objects.Add(zona);



                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error";
                    }
                }


            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                result.Ex = ex;
            }



            return result;
        }
       
    }
}

