using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class AgendaRepository
    {
        List<Agenda> agendas;

        public AgendaRepository()
        {
            agendas = new List<Agenda>();
        }

        public void Guardar(Agenda agenda)
        {
            agendas.Add(agenda);
        }

        public List<Agenda> Consultar()
        {
            return agendas;
        }

        public Agenda BuscarId(int agendaId)
        {
            foreach (var agenda in agendas)
            {
                if (agenda.AgendaID == agendaId)
                {
                    return agenda;
                }
            }
            return null;
        }

        public void Eliminar(Agenda agenda)
        {
            agendas.Remove(agenda);
        }
    }
}