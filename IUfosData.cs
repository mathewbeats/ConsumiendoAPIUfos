using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisUfosApiConsumer;

public interface IUfosData
{
    Task<IEnumerable<UfosModel>> GetUfos(HttpClient client);

    IEnumerable<UfosViewModel> TransformarAViewModel(IEnumerable<UfosModel> ufosModel);

    void ImprimirUfos(IEnumerable<UfosViewModel> ufosViewModel);
}
