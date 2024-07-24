using Base.Domain.Base.Entities;
using Base.Domain.Base.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Base.Domain.Base.Repository
{
    public class RepositoryBase<TEntidade, TId>(DbContext _context) : IRepositoryBase<TEntidade, TId>
       where TEntidade : BaseEntity
       where TId : struct
    {
        /// <summary>
        /// Busca listagem da entidade de acordo com o filtro.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return Listar(includeProperties).Where(where);
        }

        /// <summary>
        /// Busca a entidade de acordo com o filtro e ordenação.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="ordem"></param>
        /// <param name="ascendente"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return ascendente ? ListarPor(where, includeProperties).OrderBy(ordem) : ListarPor(where, includeProperties).OrderByDescending(ordem);
        }

        /// <summary>
        /// Busca a entidade de acordo com o filtro.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public TEntidade? ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return Listar(includeProperties).FirstOrDefault(where);
        }

        /// <summary>
        /// Busca a entidade pelo identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public TEntidade? ObterPorId(TId id, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            if (includeProperties.Length != 0)
            {
                return Listar(includeProperties).FirstOrDefault(x => x.Id.ToString() == id.ToString());
            }

            return _context.Set<TEntidade>().Find(id);
        }

        /// <summary>
        /// Busca listagem da entidade com todos os registros sem filtro.
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            return query;
        }

        /// <summary>
        /// Lista entidade ordernado por chave informada para acesdente e crescente.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="ordem"></param>
        /// <param name="ascendente"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            return ascendente ? Listar(includeProperties).OrderBy(ordem) : Listar(includeProperties).OrderByDescending(ordem);
        }

        /// <summary>
        /// Adiciona a entidade.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TEntidade Adicionar(TEntidade entidade)
        {
            var entityEntry = _context.Set<TEntidade>().Add(entidade);
            return entityEntry.Entity;
        }

        /// <summary>
        /// Atualiza a entidade.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TEntidade Editar(TEntidade entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;

            return entidade;
        }

        /// <summary>
        ///  Remove a entidade.
        /// </summary>
        /// <param name="entidade"></param>
        public void Remover(TEntidade entidade)
        {
            _context.Set<TEntidade>().Remove(entidade);
        }

        /// <summary>
        /// Remove lista de entidades.
        /// </summary>
        /// <param name="entidade"></param>
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

        /// <summary>
        ///  Converte Func para Expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static Expression<Func<TEntidade, bool>> ToExpression(Func<TEntidade, bool> predicate)
        {
            return entity => predicate(entity);
        }

        /// <summary>
        ///  Busca a entidade de acordo com o filtro assincrono.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<TEntidade?> ObterPorAsync(Func<TEntidade, bool> where, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            var whereExpression = ToExpression(where);

            return await Listar(includeProperties).FirstOrDefaultAsync(whereExpression, cancellationToken);
        }

        /// <summary>
        /// Busca a entidade pelo identificador assincrono.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<TEntidade?> ObterPorIdAsync(TId id, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            var whereExpression = ToExpression(x => x.Id.ToString() == id.ToString());

            if (includeProperties.Length != 0)
            {
                return await Listar(includeProperties).FirstOrDefaultAsync(whereExpression, cancellationToken);
            }

            return await _context.Set<TEntidade>().FindAsync(id);
        }

        /// <summary>
        /// Verifica se existe algum objeto com a condição informada assincrono.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<bool> ExisteAsync(Func<TEntidade, bool> where, CancellationToken cancellationToken = default)
        {
            var whereExpression = ToExpression(where);

            return await _context.Set<TEntidade>().AnyAsync(whereExpression, cancellationToken);
        }

        /// <summary>
        /// Adiciona a entidade assincrono.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public async Task<TEntidade> AdicionarAsync(TEntidade entidade)
        {
            var entityEntry = await _context.Set<TEntidade>().AddAsync(entidade);
            return entityEntry.Entity;
        }
    }
}
