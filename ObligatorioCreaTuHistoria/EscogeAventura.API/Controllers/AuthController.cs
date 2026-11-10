using DTOs.DTO.DTOsAuth;
using DTOs.DTO.DTOsUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.CustomExceptions.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EscogeAventura.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private ICUVerifExistencia _CuVerifExistencia;

        public AuthController(ICUVerifExistencia CuVerificarExistencia)
        {
            _CuVerifExistencia = CuVerificarExistencia;
        }

       

        [HttpPost("login")]
        public IActionResult Login(DTOLogin dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                return BadRequest("Debe ingresar el email y la contraseña");
            }
            try
            {

                DTOUsuario u = _CuVerifExistencia.Ejecutar(dto.Email, dto.Contrasena);

                if (u == null)
                {
                    return Unauthorized("Email o contraseña incorrectos");
                }

                var clave = "UTzl^7yPl$5xrT6&{o89$CW1XXRrN(>XqIp{W4s2S5$>KT$6CG!2M]'ZlrqH-t%eI4.X9W~u#qO+oX£+[?7QDAa";
                var claveCodificada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.Nombre),
                    new Claim(ClaimTypes.Email, u.Email),
                    new Claim(ClaimTypes.Role, u.Rol.Nombre),
                    new Claim(ClaimTypes.Sid, u.Id.ToString())
                };

                var credenciales = new SigningCredentials(claveCodificada, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(3), signingCredentials: credenciales);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                DTOLoginRespuesta respuesta = new DTOLoginRespuesta
                {
                    Nombre = u.Nombre,
                    Rol = u.Rol.Nombre,
                    Token = jwt,
                    IdUsuario = u.Id
                };


                return Ok(respuesta);
            }
            catch (UsuarioException ex)   // tu excepción de dominio, si la tenés
            {
                // Mensajes específicos: "El usuario no existe", "Contraseña incorrecta", etc.
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                // No expongas ex.Message de errores inesperados (puede traer datos internos)
                return StatusCode(500, "Ocurrió un error inesperado en el servidor");
            }

        }
    }
}
