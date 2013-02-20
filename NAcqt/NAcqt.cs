using System.Collections.Generic;

namespace NAcqt
{
    public static class NAcqt
    {
        public static SourceReader Analyze(string project)
        {
            return new SourceReader(project);
        }
    }
}
