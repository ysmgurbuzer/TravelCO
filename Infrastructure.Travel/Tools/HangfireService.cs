using Hangfire;
using Hangfire.Console;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Travel.Tools
{
    public static class HangfireService
    {
        //public static void ConfigureHangfireService(this WebApplicationBuilder builder)
        //{
        //    var mongoUrlBuilder = new MongoUrlBuilder(builder.Configuration.GetValue<string>("ConnectionStrings:MongoDb"));
        //    var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
        //    string databaseName = "hangfire_database";

        //    builder.Services.AddHangfire(configuration => configuration
        //        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        //        .UseSimpleAssemblyNameTypeSerializer()
        //        .UseRecommendedSerializerSettings()
        //        .UseConsole()
        //        .UseMongoStorage(mongoClient, databaseName, new MongoStorageOptions
        //        {
        //            MigrationOptions = new MongoMigrationOptions
        //            {
        //                MigrationStrategy = new MigrateMongoMigrationStrategy(),
        //                BackupStrategy = new CollectionMongoBackupStrategy()
        //            },
        //            Prefix = "todo.hangfire",
                  
        //            CheckConnection = false
        //        })
        //    );

        //    builder.Services.AddHangfireServer(serverOptions =>
        //    {
        //        serverOptions.ServerName = "ToDo.Hangfire";
        //    });
        //}
    }
}
