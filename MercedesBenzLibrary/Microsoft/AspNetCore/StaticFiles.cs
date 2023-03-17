using System;

namespace Microsoft.AspNetCore
{
    internal class StaticFiles
    {
        internal class FileExtensionContentTypeProvider
        {
            public FileExtensionContentTypeProvider()
            {
            }

            internal bool TryGetContentType(string fileName, out string contentType)
            {
                throw new NotImplementedException();
            }
        }
    }
}