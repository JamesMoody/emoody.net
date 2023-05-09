using System;
using System.Collections.Generic;
using System.Text;

namespace eMoody.Infrastructure.DataModels
{
    public class BibleVersion
    {
        public int    Id            { get; set; }
        public string Table         { get; set; }
        public string Abbrevation   { get; set; }
        public string Language      { get; set; }
        public string Version       { get; set; }
        public string InfoText      { get; set; }
        public string InfoURL       { get; set; }
        public string Publisher     { get; set; }
        public string Copyright     { get; set; }
        public string CopyrightInfo { get; set; }
        
    }
}
