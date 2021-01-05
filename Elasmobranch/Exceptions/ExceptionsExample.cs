using System;
using System.Net;
using Syroot.Windows.IO;

namespace Elasmobranch.Exceptions
{
    public static class ExceptionsExample
    {
        /// <summary>
        ///     Get the filename from the given URL
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static string Basename(Uri uri)
        {
            return uri.LocalPath;
        }

        /// <summary>
        ///     Download a file from the internet given a URL
        /// </summary>
        /// <param name="url">The web file URL</param>
        public static bool DownloadFile(string url)
        {
            var uri = new Uri(url);

            try
            {
                using var client = new WebClient();
                client.DownloadFile(uri, new KnownFolder(KnownFolderType.Downloads).Path + Basename(uri));
            }
            catch (WebException e)
            {
                Console.WriteLine(e.GetBaseException().Message);

                if (e.Status == WebExceptionStatus.ProtocolError) Console.WriteLine($"Status code: {(int) e.Status}");

                return false;
            }

            return true;
        }
    }
}