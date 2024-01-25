using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisUfosApiConsumer;

public class UfosViewModel
{
    public int id { get; set; }

    public string nombre { get; set; }

    public string entity { get; set; }

    public string descripcion { get; set; }

    public string lugarDeOrigen { get; set; }

    public DateTime fechaAvistamiento { get; set; }

    public string detallesAvistamiento { get; set; }

    public string urlImagen { get; set; }

    public string explicacionesAlternativas { get; set; }

}
