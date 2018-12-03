using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgAndStructs_RGZ_SuffixTree
{
    class SimpleSuffixTree
    {
        private readonly SimpleSuffixTreeEdge _root;
        private readonly List<char> _string;

        private void SplitTreeRoot(int position, int charIndex)
        {

        }

        /*private (SimpleSuffixTreeEdge edge, int position) FastScan(SimpleSuffixTreeEdge edge, int offset)
        {
            if (offset > edge.To - edge.From)
        }*/
    }

    class SimpleSuffixTreeEdge
    {
        public SimpleSuffixTreeEdge()
        {
            Children = new List<SimpleSuffixTreeEdge>();
        }

        internal int From { get; set; }
        internal int? To { get; set; }
        internal List<SimpleSuffixTreeEdge> Children { get; set; }
        internal SimpleSuffixTreeEdge SuffixLink { get; set; }

        public SimpleSuffixTreeEdge Subdivide(int position)
        {
            var tailEdge = new SimpleSuffixTreeEdge()
            {
                From = position,
                To = To,
                Children = Children
            };

            Children = new List<SimpleSuffixTreeEdge>() { tailEdge };

            return tailEdge;
        }
    }
}
