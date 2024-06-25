// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using VidSphere.Core.Api.Brokers.Blobs;
using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;
using VidSphere.Core.Api.Brokers.Storages;
using VidSphere.Core.Api.Services.Foundations.Photos;
using VidSphere.Core.Api.Services.Foundations.VideoMetadatas;
using VidSphere.Core.Api.Services.Foundations.Videos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<IDateTimebroker, DateTimeBroker>();
builder.Services.AddTransient<IVideoMetadataService, VideoMetadataService>();
builder.Services.AddTransient<IBlobBroker, BlobBroker>();
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<IPhotoService, PhotoService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
