using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AppAC.Domain;
using AppAC.Domain.Contracts;

namespace AppAC.Application.Test.Dobles
{
    class ActividadAsignadaRepositoryFake : IActividadRepository
    {
        public void Add(Actividad entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<Actividad> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Actividad entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<Actividad> entities)
        {
            throw new NotImplementedException();
        }

        public Actividad Find(int id)
        {
            var dpto = new Departamento("AB21", "Departamento de Ingenieria de Sistemas");
            var actividad = new TipoActividad("Asesoria");
            var asignador = new JefeDpto("1223425","Lucas", "Ortiz","example@gmail.com","Masculino",dpto);
            return new Actividad(actividad,asignador);
        }

        public Actividad Find(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Actividad> FindBy(Expression<Func<Actividad, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Actividad> FindBy(Expression<Func<Actividad, bool>> filter = null, Func<IQueryable<Actividad>, IOrderedQueryable<Actividad>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Actividad FindFirstOrDefault(Expression<Func<Actividad, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Actividad> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Actividad entity)
        {
            throw new NotImplementedException();
        }
    }
}
