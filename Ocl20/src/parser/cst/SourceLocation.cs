using System;
using Ocl20.parser.controller;

namespace Ocl20.parser.cst
{
    public class SourceLocation : IComparable {
        private string sourceName;
        private int line;
        private int column;

        public SourceLocation(string sourceName, int line, int column) {
            this.sourceName = sourceName;
            this.line = line;
            this.column = column;
        }

        public SourceLocation(OCLWorkbenchToken token) {
            this.sourceName = token != null ? token.getSrcName() : "";
            this.line = token.getLine();
            this.column = token.getColumn();
        }

        public override string ToString() {
            if ((line > 0) && (column > 0)) {
                return sourceName + " [line:" + line + " col:" + column + "]";
            } else {
                return sourceName;
            }
        }

        /**
     * @return
     */
        public int getColumn() {
            return column;
        }

        /**
     * @return
     */
        public int getLine() {
            return line;
        }

        /**
     * @return
     */
        public String getSourceName() {
            return sourceName;
        }

        /* (non-Javadoc)
     * @see java.lang.Comparable#compareTo(java.lang.Object)
     */
        public int CompareTo(object arg0) {
            SourceLocation other = (SourceLocation) arg0;

            if ((sourceName == other.sourceName) ||
                ((sourceName != null) && sourceName.Equals(other.sourceName))) {
                    if (line == other.line) {
                        return column - other.column;
                    } else {
                        return line - other.line;
                    }
                } else {
                    return sourceName.CompareTo(other.sourceName);
                }
        }

        /* (non-Javadoc)
     * @see java.lang.Object#equals(java.lang.Object)
     */
        public override bool Equals(object arg0) {
            SourceLocation other = (SourceLocation) arg0;

            return ((sourceName == other.sourceName) ||
                    ((sourceName != null) && sourceName.Equals(other.sourceName))) &&
                   (line == other.line) && (column == other.column);
        }

        /* (non-Javadoc)
     * @see java.lang.Object#hashCode()
     */
        public override int GetHashCode() {
            return sourceName.GetHashCode();
        }
    }
}
