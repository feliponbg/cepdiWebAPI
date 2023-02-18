using cepdiWebAPI.Services.Utilerias;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace cepdiWebAPI.Services
{
    public class Sesion
    {
        private readonly ILogger<Sesion> objLogger;
        private readonly IConfiguration objConfiguracion;
        private readonly Excel servExcel;

        private enum TipoDuracion { Segundos = 1, Minutos = 2, Horas = 3, Dias = 4 };
        private TipoDuracion tipoDuracion;

        public Sesion(ILogger<Sesion> logger, IConfiguration configuration, Services.Utilerias.Excel servExcel)
        {
            this.objLogger = logger;
            this.objConfiguracion = configuration;
            this.servExcel = servExcel;
        }

        public async Task<Models.Sesion> InciarSesion(Models.ViewModels.Sesion objJson)
        {

            Models.Sesion objRespuesta = null;

            //Valores por Defecto
            int cantidadTiempo = objJson.MantenerSesionIniciada ? objConfiguracion.GetValue<Int32>("JWT:CantidadTiempoMax") : objConfiguracion.GetValue<Int32>("JWT:CantidadTiempo");
            int tipoValor = objJson.MantenerSesionIniciada ? objConfiguracion.GetValue<Int32>("JWT:TipoValorMax") : objConfiguracion.GetValue<Int32>("JWT:TipoValor");

            //leer el archivo de texto haciendolo pasar por excel
            try
            {
                //Revisar si existe el usuario:
                var dt = servExcel.Leer(objConfiguracion.GetValue<string>("BaseDeDatosSimulacion:TablaUsuarios"));

                var resultado = dt.Select($"usuario = '{objJson.Usuario}' and password = '{objJson.Contraseña}'");

                if (resultado.Length == 0)
                    return objRespuesta;
            }
            catch (Exception error)
            {
                this.objLogger.LogError(error.Message);
                return objRespuesta;
            }

            //Crear Token con Claims
            tipoDuracion = (TipoDuracion)tipoValor;
            return objRespuesta = new Models.Sesion()
            {
                Usuario = objJson.Usuario,
                Token = GenerarToken(objJson.Usuario, cantidadTiempo)
            };
        }

        private string GenerarToken(string usuario, int cantidadDuracion)
        {
            byte[] data = Convert.FromBase64String(objConfiguracion.GetValue<string>("JWT:ClaveSecreta").ToString());
            //string llaveSecreta = Encoding.UTF8.GetString(data);

            DateTime caducidad = DeterminarDuracion(cantidadDuracion);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim("Usuario", usuario),
                new Claim("Caducidad", caducidad.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = caducidad,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(data)
                                                                , SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }

        private DateTime DeterminarDuracion(int cantidadDuracion)
        {
            DateTime duracion;
            duracion = tipoDuracion switch
            {
                TipoDuracion.Segundos => DateTime.UtcNow.AddSeconds(cantidadDuracion),
                TipoDuracion.Minutos => DateTime.UtcNow.AddMinutes(cantidadDuracion),
                TipoDuracion.Horas => DateTime.UtcNow.AddHours(cantidadDuracion),
                TipoDuracion.Dias => DateTime.UtcNow.AddDays(cantidadDuracion),
            };

            return duracion;
        }

    }
}
