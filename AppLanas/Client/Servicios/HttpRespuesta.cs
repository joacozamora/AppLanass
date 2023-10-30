namespace AppLanas.Client.Servicios
{
    public class HttpRespuesta<T>
    {
        public T Respuesta { get; set; }

        public bool Error { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public HttpRespuesta(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            this.Respuesta = response;
            this.Error = error;
            this.HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string> ObtenerError()
        {
            if (!Error)
            {
                return "";
            }

            var statuscode = HttpResponseMessage.StatusCode;

            switch (statuscode)
            {
                
                case System.Net.HttpStatusCode.BadRequest:
                    return "Error, no se puede procesar la informacion";
                    
                case System.Net.HttpStatusCode.Unauthorized:
                     return "Error, no esta logueado";
                
                case System.Net.HttpStatusCode.Forbidden:
                    return "Error, no tiene autorizacion a ejecutar este proceso";
                
                case System.Net.HttpStatusCode.NotFound:
                    return "Error, direccion no encontrada";
                default:
                    return HttpResponseMessage.Content.ReadAsStringAsync().ToString();

            }
        }
    }

}
