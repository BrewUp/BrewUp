using System.Linq.Expressions;

namespace BrewUp.Production.ReadModel.Abstracts;

public interface IPersister
{
    Task<T> GetByIdAsync<T>(string id) where T : DtoBase;
    Task InsertAsync<T>(T dtoToInsert) where T : DtoBase;
    Task ReplaceAsync<T>(T dtoToUpdate) where T : DtoBase;
    Task UpdateOneAsync<T>(string id, Dictionary<string, object> propertiesToUpdate) where T : DtoBase;

    Task DeleteAsync<T>(string id) where T : DtoBase;
    Task DeleteManyAsync<T>(Expression<Func<T, bool>> filter) where T : DtoBase;
    Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filter = null) where T : DtoBase;
}