//------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation
// </copyright>
//------------------------------------------------------------------------------
namespace Microsoft.WindowsAzure.Storage.DataMovement
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Constants for use with the transfer classes.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Stores the max block size, 4MB.
        /// </summary>
        public const int MaxBlockSize = 4 * 1024 * 1024;

        /// <summary>
        /// Default block size, 4MB.
        /// </summary>
        public const int DefaultBlockSize = 4 * 1024 * 1024;

        /// <summary>
        /// Define cache size for one parallel operation.
        /// </summary>
        internal const long CacheSizeMultiplierInByte = 12 * 1024 * 1024;
        
        /// <summary>
        /// Default to root container name if none is specified.
        /// </summary>
        internal const string DefaultContainerName = "$root";

        /// <summary>
        /// Minimum block size, 256KB.
        /// </summary>
        internal const int MinBlockSize = 256 * 1024;

        /// <summary>
        /// Stores the max page blob file size, 1TB.
        /// </summary>
        internal const long MaxPageBlobFileSize = (long)1024 * 1024 * 1024 * 1024;

        /// <summary>
        /// Stores the max block blob file size, 50000 * 4M.
        /// </summary>
        internal const long MaxBlockBlobFileSize = (long)50000 * 4 * 1024 * 1024;

        /// <summary>
        /// Stores the max cloud file size, 1TB.
        /// </summary>
        internal const long MaxCloudFileSize = (long)1024 * 1024 * 1024 * 1024;

        /// <summary>
        /// Max transfer window size. 
        /// There can be multiple threads to transfer a file, 
        /// and we need to record transfer window 
        /// and have constant length for a transfer entry record in restart journal,
        /// so set a limitation for transfer window here.
        /// </summary>
        internal const int MaxCountInTransferWindow = 128;

        /// <summary>
        /// Length to get page ranges in one request. 
        /// In blog <c>http://blogs.msdn.com/b/windowsazurestorage/archive/2012/03/26/getting-the-page-ranges-of-a-large-page-blob-in-segments.aspx</c>,
        /// it says that it's safe to get page ranges of 150M in one request.
        /// We use 148MB which is multiples of 4MB.
        /// </summary>
        internal const long PageRangesSpanSize = 148 * 1024 * 1024;

        /// <summary>
        /// Length to get file ranges in one request.
        /// Use the same number as page blob for now because cloud file leverages page blob in implementation.
        /// TODO: update this number when doc for cloud file is available.
        /// </summary>
        internal const long FileRangeSpanSize = 148 * 1024 * 1024;

        /// <summary>
        /// Percentage of available we'll try to use for our memory cache.
        /// </summary>
        internal const double MemoryCacheMultiplier = 0.5;

        /// <summary>
        /// Maximum amount of memory to use for our memory cache.
        /// </summary>
        internal static readonly long MemoryCacheMaximum = GetMemoryCacheMaximum();

        /// <summary>
        /// Maximum amount of cells in memory manager.
        /// </summary>
        internal const int MemoryManagerCellsMaximum = 8 * 1024;

        /// <summary>
        /// The life time in minutes of SAS auto generated for asynchronous copy.
        /// </summary>
        internal const int CopySASLifeTimeInMinutes = 7 * 24 * 60;

        /// <summary>
        /// The time in milliseconds to wait to refresh copy status for asynchronous copy.
        /// In order to avoid refreshing the status too aggressively for large copy job and
        /// meanwhile provide a timely update for small copy job, the wait time increases
        /// from 0.1 second to 5 seconds gradually and remains 5 seconds afterwards.
        /// </summary>
        internal const long CopyStatusRefreshMinWaitTimeInMilliseconds = 100;

        internal const long CopyStatusRefreshMaxWaitTimeInMilliseconds = 5 * 1000;

        internal const long CopyStatusRefreshWaitTimeMaxRequestCount = 100;

        /// <summary>
        /// Asynchronous copy decreases status refresh wait time to 0.1s if there's less than 500 MB
        /// data to copy in order to detect the job completion in time.
        /// </summary>
        internal const long CopyApproachingFinishThresholdInBytes = 500 * 1024 * 1024;

        /// <summary>
        /// Multiplier to calculate number of entries listed in one segment.
        /// Formula is: Concurrency * <c>ListSegmentLengthMultiplier</c>.
        /// </summary>
        internal const int ListSegmentLengthMultiplier = 8;

        internal const string BlobTypeMismatch = "Blob type of the blob reference doesn't match blob type of the blob.";

        /// <summary>
        /// The product name used in UserAgent header.
        /// </summary>
        internal const string UserAgentProductName = "DataMovement";

        /// <summary>
        /// UserAgent header.
        /// </summary>
        internal static readonly string UserAgent = GetUserAgent();

        internal static readonly string FormatVersion = GetFormatVersion();

        /// <summary>
        /// Gets the UserAgent string.
        /// </summary>
        /// <returns>UserAgent string.</returns>
        private static string GetUserAgent()
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            return UserAgentProductName + "/" + assemblyName.Version.ToString();
        }

        private static string GetFormatVersion()
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            return assemblyName.Name + "/" + assemblyName.Version.ToString();
        }

        private static long GetMemoryCacheMaximum()
        {
            return Environment.Is64BitProcess ? (long)2 * 1024 * 1024 * 1024 : (long)512 * 1024 * 1024;
        }
    }
}
