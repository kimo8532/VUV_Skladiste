using System;
namespace VUV_skladiste
{
    internal class Iznimka : Exception
    {
        public Iznimka(string poruka) : base(poruka)
        {
        }
    }
}
