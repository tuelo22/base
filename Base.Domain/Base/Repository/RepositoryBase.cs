using Base.Domain.Base.Entities;
using Base.Domain.Base.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Base.Domain.Base.Repository
{
    public class RepositoryBase<TEntidade, TId>(DbContext context) : IRepositoryBase<TEntidade, TId>
       where TEntidade : BaseEntity
       where TId : struct
    {
        protected readonly DbContext _context = context;

        public IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return Listar(includeProperties).Where(where);
        }

        public IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return ascendente ? ListarPor(where, includeProperties).OrderBy(ordem) : ListarPor(where, includeProperties).OrderByDescending(ordem);
        }

        public TEntidade ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return Listar(includeProperties).FirstOrDefault(where);
        }

        public TEntidade ObterPorId(TId id, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            if (includeProperties.Length != 0)
            {
                return Listar(includeProperties).FirstOrDefault(x => x.Id.ToString() == id.ToString());
            }

            return _context.Set<TEntidade>().Find(id);
        }

        public IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            return query;
        }

        public IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return ascendente ? Listar(includeProperties).OrderBy(ordem) : Listar(includeProperties).OrderByDescending(ordem);
        }

        public TEntidade Adicionar(TEntidade entidade)
        {
            var entityEntry = _context.Set<TEntidade>().Add(entidade);
            return entityEntry.Entity;
        }

        public TEntidade Editar(TEntidade entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;

            return entidade;
        }

        public void Remover(TEntidade entidade)
        {
            _context.Set<TEntidade>().Remove(entidade);
        }

        public void RemoverLista(IEnumerable<TEntidade> entidade)
        {
            _context.Set<TEntidade>().RemoveRange(entidade);
        }

        /// <summary>
        /// Adicionar um coleção de entidades ao contexto do entity framework
        /// </summary>
        /// <param name="entidades">Lista de entidades que deverão ser persistidas</param>
        /// <returns></returns>
        public IEnumerable<TEntidade> AdicionarLista(IEnumerable<TEntidade> entidades)
        {
            _context.Set<TEntidade>().AddRange(entidades);

            return _context.Set<TEntidade>();
        }

        /// <summary>
        /// Verifica se existe algum objeto com a condição informada
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Existe(Func<TEntidade, bool> where)
        {
            return _context.Set<TEntidade>().Any(where);
        }
    }
}
