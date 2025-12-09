using AGS_Models.DTO;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface IEmailService
    {
        Task<bool> EnviarCorreoContacto(ContactoDTO contacto);
    }
}