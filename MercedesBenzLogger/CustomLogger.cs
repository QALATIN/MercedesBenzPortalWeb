using MercedesBenzLibrary;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Collections.Generic;

namespace MercedesBenzLogger
{
    public static class CustomLogger
    {
        public static Logger CustomLoggerConfiguration()
        {
            string connectionString = ApplicationSettings.GetConnectionString();

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                { "message", new RenderedMessageColumnWriter() },
                //{ "message_template", new MessageTemplateColumnWriter() },
                { "level", new LevelColumnWriter() },
                { "timestamp", new TimestampColumnWriter() },
                { "exception", new ExceptionColumnWriter() },
                { "log_event", new LogEventSerializedColumnWriter() }
            };

            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.PostgreSQL(connectionString: connectionString, tableName: "Logs", 
                    needAutoCreateTable: true, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning, 
                    columnOptions: columnWriters)
                .Enrich.With(new CustomLogEventEnricher())
                .Enrich.WithProperty("NombreAplicacion", "Mercedes Benz")
                .CreateLogger();

            return logger;
        }

    }
}
