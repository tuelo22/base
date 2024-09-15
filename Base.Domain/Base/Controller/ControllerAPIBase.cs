using Base.Domain.Base.DTO.Arguments;
using Base.Domain.Base.Interfaces.Services;
using Base.Domain.Base.Interfaces.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Base.Domain.Base.Controller
{
    /// <summary>
    /// Classe controller basica, todo controlle deve herdar dela.
    /// </summary>
    public class ControllerAPIBase(IUnitOfWork _unitOfWork) : ControllerBase
    {
        /// <summary>
        /// Método de resposta padrão.
        /// </summary>
        protected IActionResult ResponseAPI(ResponseBaseDTO result, IServiceBase serviceBase)
        {
            result.Mensagens = serviceBase.GetMensagensDTO();

            if (serviceBase.Valido())
            {
                try
                {
                    _unitOfWork.Commit();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return ResponseAPIException(ex);
                }
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Método de resposta padrão assincrono.
        /// </summary>
        protected async Task<IActionResult> ResponseAPIAsync(ResponseBaseDTO result, IServiceBase serviceBase)
        {
            result.Mensagens = serviceBase.GetMensagensDTO();

            if (serviceBase.Valido())
            {
                try
                {
                    await _unitOfWork.CommitAsync();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return ResponseAPIException(ex);
                }
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Response em caso de excessão.
        /// </summary>
        protected IActionResult ResponseAPIException(Exception ex)
        {
            return StatusCode(500, new { errors = $"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}", exception = ex.ToString() });
        }


        /// <summary>
        /// Retorna o Id do usuario autenticado.
        /// </summary>
        /// <returns></returns>
        protected Guid GetIdUsuario()
        {
            // Obtém o token JWT do cabeçalho de autorização
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.StartsWith("Bearer ") ? authHeader.Substring("Bearer ".Length).Trim() : authHeader;

            // Decodifica o token JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Obtém o ID do usuário a partir da claim "sub"
            var userIdClaim = jwtToken.Claims.First(claim => claim.Type == "sub").Value;

            if(userIdClaim == null)
            {
                throw new Exception("Falha ao obter o identifador do usuario registrado no token.");
            }

            // Converte o ID para Guid e retorna
            return Guid.Parse(userIdClaim);
        }
    }
}
