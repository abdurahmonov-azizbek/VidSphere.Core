﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.SqlServer;
using System;

namespace VidSphere.Core.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        public async ValueTask<T> InsertAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return @object;
        }

        public async ValueTask<T> SelectAsync<T>(params object[] objectIds) where T : class
        {
            var broker = new StorageBroker(this.configuration);

            return await broker.FindAsync<T>(objectIds);
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            var broker = new StorageBroker(this.configuration);

            return broker.Set<T>();
        }

        public async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Modified;
            await broker.SaveChangesAsync();

            return @object;
        }

        public async ValueTask<T> DeleteAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Deleted;
            await broker.SaveChangesAsync();

            return @object;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose()
        { }
    }
}
