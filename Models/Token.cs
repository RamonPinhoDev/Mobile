﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleListUI.Models
{
    internal class Token
    {
        public string? AcessToken { get; set; }
        public string? TokenType { get; set; }
        public int? UsuarioId { get; set; }
        public string? UsuarioNome { get; set; }
    }
}
