using System;
using EnvDTE;
using System.IO;
using EnvDTE80;

namespace SwitchCore
{
    /// <summary>
    /// Helper functions for DoSwitch functionality.
    /// </summary>
    public static class SwitchHelper
    {
        /// <summary>
        /// Tries the open document.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static bool TryOpenDocument(DTE2 application, string path)
        {
            //  Go through each document in the solution.
            foreach(Document document in application.Documents)
            {
                if(String.CompareOrdinal(document.FullName, path) == 0)
                {
                    //  The document is open, we just need to activate it.
                    if (document.Windows.Count > 0)
                    {
                        document.Activate();
                        return true;
                    }
                }
            }

            //  The document isn't open - does it exist?
            if (File.Exists(path))
            {
                try
                {
                    application.Documents.Open(path, "Text", false);
                    return true;
                }
                catch (Exception)
                {
                    //  We can't open the doc.
                    return false;
                }
            }

            //  We couldn't open the document.
            return false;
        }
    }
}
