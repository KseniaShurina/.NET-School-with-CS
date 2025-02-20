﻿namespace Task8.DAL.Interfaces;

public interface IRepository<T>
{
    public Task SaveAsync(IEnumerable<T> items);

    public Task<IEnumerable<T>?> GetAsync();
}