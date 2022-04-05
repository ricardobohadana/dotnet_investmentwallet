using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Entities
{
    public class PerfilInvestidor
    {
        public PerfilInvestidor()
        {
            IdPerfilInvestidor = Guid.Parse("7dc669ba-3cef-4f27-9ef1-d573ae6b0a2f");
            Tipo = "Conservador";
            Descricao = "É aquele que tem forte repulsa ao risco e prefere aplicações seguras. Ou seja, não está disposto a perder mesmo diante da forte possibilidade de altos ganhos.";

            Usuarios = new List<Usuario>();
        }

        #region Propriedades

        public Guid IdPerfilInvestidor { get; set; }

        public string Tipo { get; set; }

        public string Descricao { get; set; }

        #endregion

        #region Relacionamento

        public List<Usuario> Usuarios { get; set; }

        #endregion
    }
}

//INSERT INTO PERFILINVESTIDOR (ID, TIPO, DESCRICAO)
//VALUES(
//    '7dc669ba-3cef-4f27-9ef1-d573ae6b0a2f',
//    'Conservador',
//    'É aquele que tem forte repulsa ao risco e prefere aplicações seguras. Ou seja, não está disposto a perder mesmo diante da forte possibilidade de altos ganhos.'
//)

//INSERT INTO PERFILINVESTIDOR (ID, TIPO, DESCRICAO)
//VALUES(
//    '30816e8c-46b0-49fd-a31f-4b019a61e3f6',
//    'Moderado',
//    'Trata-se do investidor que opta por arriscar mais que o conservador em busca de mais rentabilidade. Porém, ele ainda não está disposto a assumir grandes riscos que resultem em uma perda significativa na carteira'
//)

//INSERT INTO PERFILINVESTIDOR (ID, TIPO, DESCRICAO)
//VALUES(
//    '829821a5-c3fc-4ba3-ac2d-a8cb9d198671',
//    'Agressivo',
//    'A pessoa de perfil agressivo ou arrojado está em busca de rendimentos maiores e disposta a correr riscos para que isso aconteça. Conta-se, portanto, com a imprevisibilidade e as perdas em curto prazo para que se tenha altos ganhos em um tempo maior.'
//)