using Pokemon.Business.Interfaces;
using System;

namespace Pokemon.Business.Services
{
    public class EscapeService : IEscapeService
    {
        public string EscapeString(string text)
        {
            return Uri.EscapeUriString(text.Replace("\n", " "));
        }
    }
}
