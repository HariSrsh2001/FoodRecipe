using Food_Recipe.Domain.Models;
//using Food_Recipe.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Food_Recipe.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // ✅ Check if user already exists
        public async Task<bool> UserExistsAsync(string username, string email)
        {
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT COUNT(*) FROM Users WHERE Username = @u OR Email = @e";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@e", email);

            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        // ✅ Register with password validation
        public async Task<bool> RegisterAsync(User user)
        {
            if (await UserExistsAsync(user.Username, user.Email))
                return false;

            //if (!IsPasswordValid(user.Password))
            //    return false;

            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = "INSERT INTO Users (Username, Email, Password) VALUES (@u, @e, @p)";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@u", user.Username);
            cmd.Parameters.AddWithValue("@e", user.Email);
            cmd.Parameters.AddWithValue("@p", user.Password);

            await cmd.ExecuteNonQueryAsync();
            return true;
        }


        private static bool IsPasswordValid(string password)
        {
            if (password.Length < 8) return false;
            if (Regex.IsMatch(password, @"(.)\1{2,}")) return false;

            bool hasSpecial = Regex.IsMatch(password, @"[\W_]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasUpper = Regex.IsMatch(password, @"[A-Z]");
            bool hasLower = Regex.IsMatch(password, @"[a-z]");

            return hasSpecial && hasDigit && hasUpper && hasLower;
        }

        // ✅ Login
        public async Task<User?> LoginAsync(string input, string password)
        {
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT * FROM Users WHERE (Username = @input OR Email = @input) AND Password = @pass";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.Parameters.AddWithValue("@pass", password);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }

            return null;
        }

        // ✅ Forgot Password - check if email exists
        public async Task<bool> EmailExistsAsync(string email)
        {
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT COUNT(*) FROM Users WHERE Email = @e";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@e", email);

            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        // ✅ Reset password with validation
        //public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        //{
        //    if (!IsPasswordValid(newPassword))
        //        return false;

        //    using SqlConnection conn = new(_connectionString);
        //    await conn.OpenAsync();

        //    string query = "UPDATE Users SET Password = @p WHERE Email = @e";
        //    using SqlCommand cmd = new(query, conn);
        //    cmd.Parameters.AddWithValue("@p", newPassword);
        //    cmd.Parameters.AddWithValue("@e", email);

        //    int rows = await cmd.ExecuteNonQueryAsync();
        //    return rows > 0;
        //}

        // ✅ Optional: Get user by email (to use in forgot password)
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT * FROM Users WHERE Email = @e";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@e", email);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }

            return null;
        }
    }
}
