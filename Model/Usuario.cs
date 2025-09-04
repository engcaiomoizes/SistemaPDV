using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Usuario
    {
        public int id {  get; set; }
        public int funcionario {  get; set; }
        public string login {  get; set; }
        public string senha {  get; set; }
        public bool ativo {  get; set; }
    }
}
