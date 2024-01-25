using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisUfosApiConsumer;

public class UfosModel
{
    public int id {get; set;}

    public string nombre {get; set;}

    public string entity {get; set;}

    public int? edad {get; set;} //este campo es null

    public string descripcion {get; set;}

    public string lugarDeOrigen {get; set;}

    public DateTime fechaAvistamiento {get; set;}

    public string detallesAvistamiento {get; set;}

    public string urlImagen {get; set;}

    public int? nivelCredibilidad {get; set;} //campo sin datos

    public string? testigos {get; set;} //campo nulo

    public string explicacionesAlternativas {get; set;}

    public byte? investigado {get; set;}

    public string? resultadosInvestigacion {get;set;} //nulo



}
