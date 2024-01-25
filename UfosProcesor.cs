using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MisUfosApiConsumer;

public class UfosProcesor : IUfosData
{

    public async Task ProcesandoUfos()
    {

        using (var client = new HttpClient())
        {

            try
            {

                var ufos = await GetUfos(client);

                var ufosModel = TransformarAViewModel(ufos);

                Console.WriteLine("Iniciando el procesamiento de Ufos");

                ImprimirUfos(ufosModel);


                Console.WriteLine("Terminando el procesamiento de ufos");


            }
            catch (HttpRequestException ex)
            {
                // Maneja excepciones relacionadas con problemas de comunicación HTTP
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Maneja excepciones relacionadas con problemas de deserialización JSON
                Console.WriteLine("Error al deserializar JSON: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones no previstas
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                // throw; // Puedes optar por relanzar la excepción si es necesario
            }
        }
    }




    public async Task<IEnumerable<UfosModel>> GetUfos(HttpClient client)
    {
        var urlUfos = "https://apiufosnet8.azurewebsites.net/api/ufo/";

        var ufosContent = await client.GetAsync(urlUfos);

        ufosContent.EnsureSuccessStatusCode();

        var ufosResponse = await ufosContent.Content.ReadAsStringAsync();

        var ufos = JsonConvert.DeserializeObject<UfosModel[]>(ufosResponse);

        return ufos ?? new UfosModel[0];
    }

    public IEnumerable<UfosViewModel> TransformarAViewModel(IEnumerable<UfosModel> ufosModel)
    {
        List<UfosViewModel> listaDeUfos = new List<UfosViewModel>();

        if (ufosModel != null)
        {
            foreach (var ufo in ufosModel)
            {

                var singleUfo = new UfosViewModel()
                {

                    id = ufo.id ,
                    nombre = ufo.nombre ?? "Desconocido",
                    entity = ufo.entity ?? "Desconocido",
                    descripcion = ufo.descripcion ?? "Desconocido",
                    detallesAvistamiento = ufo.detallesAvistamiento ?? "Desconocido",
                    lugarDeOrigen = ufo.lugarDeOrigen ?? "Desconocido",
                    explicacionesAlternativas = ufo.explicacionesAlternativas ?? "Desconocido",
                    fechaAvistamiento = ufo.fechaAvistamiento,
                    urlImagen = ufo.urlImagen ?? "Sin Imagen"
                };
                listaDeUfos.Add(singleUfo);
            }


        }



        return listaDeUfos;
    }


    public void ImprimirUfos(IEnumerable<UfosViewModel> ufosViewModel)
    {
        var archivoRuta = @"C:\Users\practicas\Desktop\ConsoleApp1\MisUfosApiConsumer\UfosText";

        var carpetaUfos = Path.Combine(archivoRuta, "Ufos.txt");



        using (var writter = new StreamWriter(carpetaUfos, true))
        {
            foreach (var ufo in ufosViewModel)
            {

                var linea = $" Id: {ufo.id.ToString().PadRight(20)}, | Nombre: {ufo.nombre.PadRight(20)}, | Descripcion: {ufo.descripcion.PadRight(20)}, | DetallesAvistamiento: {ufo.detallesAvistamiento.PadRight(20)}, | Lugar de Origen {ufo.lugarDeOrigen.PadRight(20)}, | Explicaciones Alternativas: {ufo.explicacionesAlternativas.PadRight(20)}, | Fecha del Avistamiento: {ufo.fechaAvistamiento.AddDays(1)}, | url Imagen: {ufo.urlImagen.PadRight(20)}, | Entity {ufo.entity.PadRight(20)}";

                writter.Write(linea);

                //  string linea = String.Format("Id: {0,-10} | Nombre: {1,-20} | Descripción: {2,-30} | Detalles: {3,-20} | DetallesAvistamiento {4,-20} | Lugar de Origen {5,-20} | Explicaciones Alternativas {5,-20} | Fecha del Avistamiento {6,-20} | url Imagen{7,-20} | Entity{8,-20}  ", 
                //                      ufo.id, ufo.nombre, ufo.descripcion, ufo.detallesAvistamiento, ufo.detallesAvistamiento, ufo.explicacionesAlternativas, ufo.fechaAvistamiento, ufo.urlImagen, ufo.entity );

                Console.WriteLine();

                Console.WriteLine(linea);

            }
        }


    }


}


