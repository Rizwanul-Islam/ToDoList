namespace ToDoService.Application.Contracts.Persistence
{
    /// <summary>
    /// Represents a generic repository interface for CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity.</returns>
        Task<T> Get(int id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A read-only list of entities.</returns>
        Task<IReadOnlyList<T>> GetAll();

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        Task<T> Add(T entity);

        /// <summary>
        /// Checks if an entity exists by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        Task<bool> Exists(int id);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        Task Update(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        Task Delete(T entity);

        /// <summary>
        /// Saves changes made to the repository.
        /// </summary>
        Task Save();
    }
}
