#nullable enable
using Bob.Core.Domain;
using Bob.Core.Repositories;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Sytems
{
    public static class LoginSystem
    {
        private static Customer? m_CurrentCustomer;
        private static bool IsLoggedIn => m_CurrentCustomer != null;
        private static bool IsAdmin => m_CurrentCustomer?.IsAdmin ?? false;

        public static async Task<bool> ValidateLoginAsync(string email, string password)
        {
            var customer = await CustomerRepository.GetByEmailAsync(email);
            if (customer == null)
                return false;

            if (!VerifyPassword(password, customer.PasswordHash))
                return false;

            m_CurrentCustomer = customer;

            return true;
        }

        public static async Task<bool> RegisterCustomerAsync(string name, string surname, string email, int addressId, string phoneNumber, string password, bool isAdmin = false)
        {
            var existingCustomer = await CustomerRepository.GetByEmailAsync(email);
            if (existingCustomer != null)
                return false;

            var passHash = HashPassword(password);

            int highestId = await CustomerRepository.GetHighestId();

            var customer = new Customer(
                id: highestId > 0 ? highestId + 1 : 1,
                name: name,
                surname: surname,
                email: email,
                signupDate: DateTime.UtcNow,
                addressId: addressId,
                phoneNumber: phoneNumber,
                passwordHash: passHash,
                isAdmin: isAdmin
            );

            await CustomerRepository.AddAsync(customer);
            return true;
        }

        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);

            var builder = new StringBuilder();
            foreach (byte b in hash)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool VerifyPassword(string password, string hash) => HashPassword(password) == hash;

        public static Customer? GetCurrentCustomer() => m_CurrentCustomer;

        public static void Logout() => m_CurrentCustomer = null;

        public static bool CurrentUserIsAdmin() => IsAdmin;

        public static bool CurrentUserIsLoggedIn() => IsLoggedIn;
    }
}
