using MercedesBenzModel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Helpers
{
    public class LocalStorage
    {
        private readonly ProtectedLocalStorage _localStorage;
        private string _purpose = "IdB_MB_LogIn";

        public LocalStorage(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SetUserAsync(string Key, UsuarioLogin Value)
        {
            await _localStorage.SetAsync(_purpose, Key, Value);
        }

        public async Task SetCredentialAsync(string Key, UserCredential Value)
        {
            await _localStorage.SetAsync(_purpose, Key, Value);
        }

        public async Task<UsuarioLogin> GetUserAsync(string Key)
        {
            ProtectedBrowserStorageResult<UsuarioLogin> result = new();
            UsuarioLogin localValue = null;
            try
            {
                result = await _localStorage.GetAsync<UsuarioLogin>(_purpose, Key);
            }
            catch { }
            finally
            {
                localValue = result.Success ? result.Value : null;
            }
            return localValue;
        }

        public async Task<UserCredential> GetCredentialAsync(string Key)
        {
            ProtectedBrowserStorageResult<UserCredential> result = new();
            UserCredential localValue = null;
            try
            {
                result = await _localStorage.GetAsync<UserCredential>(_purpose, Key);
            }
            catch { }
            finally
            {
                localValue = result.Success ? result.Value : null;
            }
            return localValue;
        }

        public async Task DeleteLocalStorageAsync(string Key)
        {
            await _localStorage.DeleteAsync(Key);
        }

    }
}
