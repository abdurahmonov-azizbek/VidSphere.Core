﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;
using VidSphere.Core.Api.Brokers.Storages;
using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimebroker dateTimeBroker;

        public VideoMetadataService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimebroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            TryCatch(async () =>
            {
                ValidateVideoMetadataOnAdd(videoMetadata);
                return await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
            });

        public ValueTask<VideoMetadata> ModifyVideoMetadataAsync(VideoMetadata videoMetadata) =>
            TryCatch(async () =>
            {
                ValidateVideoMetadataOnModify(videoMetadata);

                VideoMetadata maybeVideoMetadata =
                    await this.storageBroker.SelectVideoMetadataByIdAsync(videoMetadata.Id);

                ValidateAgainstStorageOnModify(videoMetadata, maybeVideoMetadata);

                return await this.storageBroker.UpdateVideoMetadataAsync(videoMetadata);
            });

        public IQueryable<VideoMetadata> RetrieveAllVideoMetadatas() =>
            TryCatch(() =>
            {
                return this.storageBroker.SelectAllVideoMetadatas();
            });

        public ValueTask<VideoMetadata> RetrieveVideoMetadataByIdAsync(Guid videoMetadataId) =>
            TryCatch(async () =>
            {
                ValidateVideoMetadataId(videoMetadataId);

                var maybeVideoMetadata =
                    await this.storageBroker.SelectVideoMetadataByIdAsync(videoMetadataId);

                ValidateStorageVideoMetadataExists(maybeVideoMetadata, videoMetadataId);

                return maybeVideoMetadata;
            });

        public ValueTask<VideoMetadata> RemoveVideoMetadataByIdAsync(Guid videoMetadataId) =>
            TryCatch(async () =>
            {
                ValidateVideoMetadataId(videoMetadataId);

                VideoMetadata maybeVideoMetadata =
                    await this.storageBroker.SelectVideoMetadataByIdAsync(videoMetadataId);

                ValidateStorageVideoMetadata(maybeVideoMetadata, videoMetadataId);

                return await this.storageBroker.DeleteVideoMetadataAsync(maybeVideoMetadata);
            });
    }
}
