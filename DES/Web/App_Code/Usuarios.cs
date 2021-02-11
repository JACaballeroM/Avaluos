using System;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Claims;
using SIGAPred.Seguridad.Utilidades.ClaimTypes;


/// <summary>
/// Clase para la gestión de usuarios en la aplicación.
/// </summary>
public class Usuarios
{
    /// <summary>
    /// Error de usuario loggeado, se produce cuando el usuario no tiene idpersona asociado.
    /// </summary>
    public Exception IdPersonaException;

    /// <summary>
    /// Método que obtiene el idpersona del usuario logeado en la aplicación.
    /// </summary>
    /// <exception cref="UserFailedException">Se lanza cuando se produce un error de tipo UserFailedException.</exception>
    /// <returns>
    /// Idpersona del usuario logeado en la aplicación.
    /// </returns>
    public static string IdPersona()
    {
        string idPersona;

        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            idPersona = Constantes.VALUE_NO_SELECT;
        }
        else
        {
            IClaimsIdentity identity = (IClaimsIdentity)HttpContext.Current.User.Identity;

            if ((from c in identity.Claims where c.ClaimType == PromocaClaims.IdPersona select c).Any())
                idPersona = (from c in identity.Claims where c.ClaimType == PromocaClaims.IdPersona select c).First().Value;
            else
                throw new UserFailedException();
        }
        return idPersona;
    }

    public static bool tienePerfil(string perfil)
    {
        bool ret = false;

        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            ret = false;
        }
        else
        {
            IClaimsIdentity identity = (IClaimsIdentity)HttpContext.Current.User.Identity;

            ret = ((from c in identity.Claims where c.Value.ToString().Equals(perfil) select c).Any());
                
        }

        return ret;
    }
}
