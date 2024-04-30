using System;

namespace Task2
{
    public static class CurrentTimeUser
    {
        public static string SetCurrentTimeUser(string username)
        {
            return $"{DateTime.Now:HH:mm:ss} Hello, {username}!";
        }
    }
}
