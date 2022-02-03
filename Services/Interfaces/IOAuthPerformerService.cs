using System.Threading.Tasks;

namespace GDLevels.Services.Interfaces
{
    public interface IOAuthPerformerService
    {
        /// <summary>
        /// Получает адрес, на который нужно перенаправить пользователя для аутентификации через OAuth
        /// и получения кода для токена доступа
        /// </summary>
        /// <param name="redirectUri">Параметр redirect_uri в адресе запроса на получение кода OAuth.
        /// Туда будет возвращён код</param>
        string GetOAuthUri(string redirectUri);

        /// <summary>
        /// Получает токен доступа по code, полученному при аутентификации пользователя по OAuth
        /// </summary>
        /// <param name="code">Код, полученный при аутентификации</param>
        /// <param name="redirectUri">redirect_uri, использованный при получении кода</param>
        Task<string> GetTokenByCodeAsync(string code, string redirectUri);
    }
}