using Eto.Parse;
using Eto.Parse.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF.Internals
{
    public class CBFFGrammar : Grammar
    {
        public CBFFGrammar()
            : base("CBFF Definition")
        {
           

            EnableMatchEvents = false;
            CaseSensitive = true;

            // terminals
            var jstring = new StringParser { AllowEscapeCharacters = true,AllowDoubleQuote = false,AllowNonQuoted = true, Name = "string", QuoteCharacters = null };
            var jnumber = new NumberParser { AllowExponent = true, AllowSign = true, AllowDecimal = true, Name = "number" };
            var jboolean = new BooleanTerminal { Name = "bool", TrueValues = new string[] { "true" }, FalseValues = new string[] { "false" }, CaseSensitive = false };
            var jname = new StringParser { AllowDoubleQuote = false, AllowEscapeCharacters = true, AllowNonQuoted = true, Name = "name", QuoteCharacters = null };
        
            var jnull = new LiteralTerminal { Value = "null", Name = "null", CaseSensitive = false };
            var ws = new RepeatCharTerminal(char.IsWhiteSpace);
            var commaDelimiter = new RepeatCharTerminal(new RepeatCharItem(char.IsWhiteSpace), ';', new RepeatCharItem(char.IsWhiteSpace));

            // nonterminals (things we're interested in getting back)
            var jobject = new SequenceParser { Name = "object" };
            var jarray = new SequenceParser { Name = "array" };
            var jprop = new SequenceParser { Name = "property" };

            // rules
            var jvalue = jstring | jnumber | jobject | jarray | jboolean | jnull;
            jobject.Add((+Terminals.LetterOrDigit).Named("NodeName") & ws & "{", ws & -((jprop & -Terminals.Set(';')) | jobject | jarray) & ws ,"}");
            jprop.Add(jname, ":", jvalue);
            jarray.Add((+Terminals.LetterOrDigit).Named("NodeName") & ws & "[", ws & -((jprop & -Terminals.Set(';')) | jobject | jarray) & ws, "]");

            // separate sequence and repeating parsers by whitespace
            jvalue.SeparateChildrenBy(ws, false);

            // allow whitespace before and after the initial object or array
            this.Inner = ws & +(jobject | jarray) & ws;
        }
    }
}