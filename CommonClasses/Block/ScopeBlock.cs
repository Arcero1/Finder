using System.Text;

namespace NCommon
{
    public class ScopeBlock : Block
    {
        public Scope scope { get; set; }

        public enum Scope
        {
            Global,
            Class,
            Other
        }

        public override bool Equals(object obj)
        {
            var toCompareWith = obj as ScopeBlock;
            if (toCompareWith == null)
            {
                return false;
            }

            return this.scope == toCompareWith.scope && base.Equals(obj);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (scope)
            {
                case (Scope.Global):
                    stringBuilder.Append("Global");
                    break;
                case (Scope.Class):
                    stringBuilder.Append("Class");
                    break;
                default:
                    stringBuilder.Append("Undetermined (usually local)");
                    break;
            }
            stringBuilder.AppendLine(" Scope");
            if (scope != Scope.Global)
            {
                stringBuilder.Append("[start] ").AppendLine(start.ToString());
                stringBuilder.Append("[end]   ").AppendLine(end.ToString());
            }

            return stringBuilder.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
