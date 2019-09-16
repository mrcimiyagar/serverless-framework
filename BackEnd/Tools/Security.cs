using System;
using System.Text;
using BackEnd.DbContexts;
using BackEnd.Models;
using BackEnd.Models.Database;

namespace BackEnd.Tools
{
    public class Security
    {
        public const string Authorization = "Authorization";
        private const string KeySource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string MakeKey64() => MakeKey(64);

        public static string MakeKey8() => MakeKey(8);

        public static bool AuthenticateUser(string authStr, DatabaseContext context, out Session result)
        {
            result = null;
            var auth = Extract(authStr);
            if (auth == null) return false;
            var session = context.Sessions.Find(auth.SessionId);
            bool returnFlag;
            (result, returnFlag) = session == null || session.Token != auth.Token ? (null, false) : (session, true);
            return returnFlag;
        }
        
        private static ReqAuth Extract(string header)
        {
            if (header == null) return null;
            var parts = header.Split(" ");
            if (parts.Length == 2)
            {
                return new ReqAuth()
                {
                    SessionId = long.Parse(parts[0]),
                    Token = parts[1]
                };
            }
            return null;
        }
        
        private static string MakeKey(int length)
        {
            var result = new StringBuilder();
            var rnd = new Random();
            for (var counter = 0; counter < length; counter++)
                result.Append(KeySource[rnd.Next(KeySource.Length - 1)]);
            return result.ToString();
        }
    }
}