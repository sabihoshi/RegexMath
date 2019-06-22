namespace RegexMath
{
    public class Context
    {
        public string Result { get; set; }
        public string Operation { get; set; }
    }

    public class RegexBuilder
    {
        private readonly Context _context = new Context();

        // language=REGEXP
        private static string Number { get; } =
            @"(?<bracket>[(])*
              (?<x>                         # save 'x' as the full number
                (?<int>[+-]?[0-9,]+)?       # match integer or commas
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |   # make decimal optional if there is an 'int'
                           [.][0-9]+) )     # else make decimal required
                (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)])+) # balance brackets";

        public RegexBuilder WithOperations(string operations)
        {
            // language=REGEXP
            _context.Operation = $@"(?<operation>
                (?(rhs)\k<operation>|{operations})) # back-reference operation or capture it";
            return this;
        }

        public string Build()
        {
            return $@"(?>{Number}) ({_context.Operation} {Number})+";
        }
    }
}
